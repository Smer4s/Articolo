using Client.Services.Abstractions;
using Client.Services.Auth;

namespace Client.Configurations;

public static class DependencyInjection
{
    public static void AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorComponents()
                .AddInteractiveServerComponents();
        services.AddTransient<IIdentityProviderHttpClient, IdentityProviderHttpClient>();
        services.AddTransient<IAuthenticationService, AuthenthicationService>();
        services.AddHttpContextAccessor();
        services.AddHttpClient();
        services.Configure<ApiOptions>(configuration.GetSection(ApiOptions.Section));
    }
}
