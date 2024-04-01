using Client.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;

namespace Client.Services.Auth;

public class IdentityProviderHttpClient : IIdentityProviderHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly string? AccessToken;

    public IdentityProviderHttpClient(
        IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory)
    {
        AccessToken = httpContextAccessor.HttpContext?.Request.Cookies["access_token"];

        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        AddAuthorizationHeader(request);

        var response = await _httpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        return response;
    }

    private void AddAuthorizationHeader(HttpRequestMessage request)
    {
        if (!string.IsNullOrEmpty(AccessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, AccessToken);
        }
    }
}
