using Client.Configurations;
using Client.Models.Auth;
using Client.Services.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Client.Extensions;
using System.Net;

namespace Client.Services;

public class AuthenthicationService(IIdentityProviderHttpClient httpClient, IOptions<ApiOptions> options) : IAuthenticationService
{
    private readonly API _api = new API(options);
    public async Task<ApiTokenModel> Authenticate(AuthCredentials credentials)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, _api.TokenUrl);

        var values = new Dictionary<string, string>()
        {
            {"login", credentials.Login },
            {"password", credentials.Password },
        };

        request.Content = new StringContent(values.ToJson(), null, "application/json");

        var response = await httpClient.SendAsync(request, CancellationToken.None);

        var tokenString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<ApiTokenModel>(tokenString) ?? throw new JsonSerializationException();
    }

    public async Task RegisterUser(AuthCredentials credentials)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, _api.UserUrl);

        var values = new Dictionary<string, string>()
        {
            {"login", credentials.Login },
            {"password", credentials.Password },
        };

        request.Content = new StringContent(values.ToJson(), null, "application/json");

        var response = await httpClient.SendAsync(request, CancellationToken.None);
    }
}
