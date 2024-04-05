using Client.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;

namespace Client.Services;

public class IdentityProviderHttpClient : IIdentityProviderHttpClient
{
    private readonly HttpClient _httpClient;
    private string? accessToken;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityProviderHttpClient(
        IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory)
    {
        _httpContextAccessor = httpContextAccessor;
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
        accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["access_token"];

        if (!string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, accessToken);
        }
    }
}
