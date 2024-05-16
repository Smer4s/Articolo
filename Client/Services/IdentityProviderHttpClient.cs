using Client.Configurations;
using Client.Extensions;
using Client.Models.Auth;
using Client.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Services;

public class IdentityProviderHttpClient(
	IHttpContextAccessor httpContextAccessor,
	IHttpClientFactory httpClientFactory,
	IOptions<ApiOptions> options) : IIdentityProviderHttpClient
{
	private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
	private string? accessToken;
	private string? refreshToken;
	private readonly API _api = new API(options);

	public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		AddAuthorizationHeader(request);

		var response = await _httpClient.SendAsync(request, cancellationToken);

		if (response.StatusCode is HttpStatusCode.Unauthorized && accessToken is not null)
		{
			refreshToken = httpContextAccessor.HttpContext?.Request.Cookies["refresh_token"];
			
			if (!string.IsNullOrEmpty(refreshToken))
			{
				var tokens = await RefreshTokenRequest();

				accessToken = tokens.AccessToken;
				refreshToken = tokens.RefreshToken;

				var requestCopy = new HttpRequestMessage(request.Method, request.RequestUri)
				{
					Content = request.Content is null ? null : request.Content
				};

				AddAuthorizationHeader(requestCopy);

				response = await _httpClient.SendAsync(requestCopy, cancellationToken);

				httpContextAccessor.HttpContext?.Request.Cookies.Append(new KeyValuePair<string, string>("refresh_token", refreshToken));
				httpContextAccessor.HttpContext?.Request.Cookies.Append(new KeyValuePair<string, string>("access_token", accessToken));
			}
		}

		response.EnsureSuccessStatusCode();

		return response;
	}

	private async Task<ApiTokenModel> RefreshTokenRequest()
	{
		using var request = new HttpRequestMessage(HttpMethod.Post, _api.RefreshTokenUrl);

		var values = new Dictionary<string, string>()
		{
			{"access_token", accessToken! },
			{"refresh_token", refreshToken! },
		};

		request.Content = new StringContent(values.ToJson(), null, "application/json");

		var response = await _httpClient.SendAsync(request, CancellationToken.None);

		var tokenString = await response.Content.ReadAsStringAsync();

		return JsonConvert.DeserializeObject<ApiTokenModel>(tokenString) ?? throw new JsonSerializationException();
	}

	private void AddAuthorizationHeader(HttpRequestMessage request)
	{
		accessToken = httpContextAccessor.HttpContext?.Request.Cookies["access_token"];

		if (!string.IsNullOrEmpty(accessToken))
		{
			request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, accessToken);
		}
	}
}
