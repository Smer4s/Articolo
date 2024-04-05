using Client.Configurations;
using Client.Models;
using Client.Models.Publication;
using Client.Services.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Client.Services;

public class AdminService(IIdentityProviderHttpClient httpClient, IOptions<ApiOptions> options) : IAdminService
{
    readonly API _api = new API(options);

    public async Task<User[]?> GetModerators()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, _api.AdminUrl + "/moders");

        var response = await httpClient.SendAsync(request, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<User[]>(content);
    }

    public async Task<User[]?> GetUsers()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, _api.AdminUrl + "/users");

        var response = await httpClient.SendAsync(request, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<User[]>(content);
    }

    public async Task AddModerator(int userId)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, _api.AdminUrl + $"/moder/{userId}");

        var response = await httpClient.SendAsync(request, CancellationToken.None);
    }   

    public async Task RemoveModerator(int userId)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, _api.AdminUrl + $"/moder/{userId}");

        var response = await httpClient.SendAsync(request, CancellationToken.None);
    }
}
