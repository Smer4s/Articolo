namespace Client.Services.Abstractions;

public interface IIdentityProviderHttpClient
{
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
}
