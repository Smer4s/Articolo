using Microsoft.Extensions.Options;

namespace Client.Configurations;

public class API(IOptions<ApiOptions> options)
{
    public string TokenUrl => $"{options.Value.BaseUrl}/auth/token";
    public string RefreshTokenUrl => $"{options.Value.BaseUrl}/auth/refresh-token";
    public string PublicationUrl => $"{options.Value.BaseUrl}/publication";
    public string ModeratorUrl => $"{options.Value.BaseUrl}/moder";
    public string AdminUrl => $"{options.Value.BaseUrl}/admin";
    public string UserUrl => $"{options.Value.BaseUrl}/user";
}
