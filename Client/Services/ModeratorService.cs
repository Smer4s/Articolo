using Client.Configurations;
using Client.Models.Publication;
using Client.Services.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Client.Services;

public class ModeratorService(IIdentityProviderHttpClient httpClient, IOptions<ApiOptions> options) : IModeratorService
{
    readonly API _api = new API(options);
    public async Task ApprovePublication(int publicationId)
    {
        using var request = new HttpRequestMessage(HttpMethod.Put, _api.ModeratorUrl + $"/publication/approve/{publicationId}");

        var response = await httpClient.SendAsync(request, CancellationToken.None);
    }

    public async Task DeclinePublication(int publicationId)
    {
        using var request = new HttpRequestMessage(HttpMethod.Put, _api.ModeratorUrl + $"/publication/decline/{publicationId}");

        var response = await httpClient.SendAsync(request, CancellationToken.None);
    }

    public async Task<Publication[]> GetPublications()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, _api.ModeratorUrl + "/publications");

        var response = await httpClient.SendAsync(request, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<Publication[]>(content) ?? throw new JsonSerializationException();
    }
}
