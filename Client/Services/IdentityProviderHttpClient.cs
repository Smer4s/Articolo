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
	ICookie cookieService,
	IHttpClientFactory httpClientFactory,
	IOptions<ApiOptions> options) : IIdentityProviderHttpClient
{
	private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
	private string? accessToken;
	private string? refreshToken;
	private readonly API _api = new API(options);

	public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		await AddAuthorizationHeader(request);

		var response = await _httpClient.SendAsync(request, cancellationToken);

		if (response.StatusCode is HttpStatusCode.Unauthorized && accessToken is not null)
		{
			refreshToken = await cookieService.GetValue("refresh_token");
			
			if (!string.IsNullOrEmpty(refreshToken))
			{
				var tokens = await RefreshTokenRequest();

				accessToken = tokens.AccessToken;
				refreshToken = tokens.RefreshToken;

				var requestCopy = new HttpRequestMessage(request.Method, request.RequestUri)
				{
					Content = request.Content is null ? null : request.Content
				};

				await cookieService.SetValue("refresh_token", refreshToken);
				await cookieService.SetValue("access_token", accessToken);

				await AddAuthorizationHeader(requestCopy);

				response = await _httpClient.SendAsync(requestCopy, cancellationToken);
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
			{"accessToken", accessToken! },
			{"refreshToken", refreshToken! },
		};

		request.Content = new StringContent(values.ToJson(), null, "application/json");

		var response = await _httpClient.SendAsync(request, CancellationToken.None);

		var tokenString = await response.Content.ReadAsStringAsync();

		return JsonConvert.DeserializeObject<ApiTokenModel>(tokenString) ?? throw new JsonSerializationException();
	}

	private async Task AddAuthorizationHeader(HttpRequestMessage request)
	{
		accessToken = await cookieService.GetValue("access_token");

		if (!string.IsNullOrEmpty(accessToken))
		{
			request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, accessToken);
		}
	}
}
