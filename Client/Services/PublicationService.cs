using Client.Configurations;
using Client.Extensions;
using Client.Models.Auth;
using Client.Models.Publication;
using Client.Services.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace Client.Services;

public class PublicationService(IIdentityProviderHttpClient httpClient, IOptions<ApiOptions> options) : IPublicationService
{
    private readonly API _api = new API(options);
    public async Task AddPublicationToFavorites(int publicationId)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, _api.PublicationUrl + $"/favorites/{publicationId}");
        var response = await httpClient.SendAsync(request, CancellationToken.None);
    }

    public async Task CreatePublication(Publication publication)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, _api.PublicationUrl);
        var values = new Dictionary<string, object>()
        {
            { "title", publication.Title },
            { "xmlDocument", publication.Content },
            { "themeIds", new int[]{ 0 } }
        };

        request.Content = new StringContent(values.ToJson(), null, "application/json");

        await httpClient.SendAsync(request, CancellationToken.None);
    }

    public Task DeletePublication(int publicationId)
    {
        throw new NotImplementedException();
    }

    public async Task<Publication[]> GetPublications()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, _api.PublicationUrl);

        var response = await httpClient.SendAsync(request, CancellationToken.None);

        var content = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<Publication[]>(content) ?? throw new JsonSerializationException();
    }

    public Task UpdatePublication(Publication publication)
    {
        throw new NotImplementedException();
    }
}
