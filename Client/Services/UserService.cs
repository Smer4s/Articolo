using Client.Configurations;
using Client.Extensions;
using Client.Models;
using Client.Models.Auth;
using Client.Models.Publication;
using Client.Services.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace Client.Services;

public class UserService(IIdentityProviderHttpClient httpClient, IOptions<ApiOptions> options) : IUserService
{
    private readonly API _api = new API(options);
    public Task<IEnumerable<Publication>> GetFavorites()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUser()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, _api.UserUrl);

        var response = await httpClient.SendAsync(request, CancellationToken.None);

        var userString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<User>(userString) ?? throw new JsonSerializationException();
    }

    public Task RegisterUser(AuthCredentials credentials)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}
