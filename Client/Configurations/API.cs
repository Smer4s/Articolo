using Microsoft.Extensions.Options;

namespace Client.Configurations;

public class API(IOptions<ApiOptions> options)
{
    public string TokenUrl => $"{options.Value.BaseUrl}/auth/token";
    public string RefreshTokenUrl => $"{options.Value.BaseUrl}/auth/refresh-token";
}
