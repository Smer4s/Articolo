using Client.Services;
using Client.Services.Abstractions;

namespace Client.Configurations;

public static class DependencyInjection
{
    public static void AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorComponents()
            .AddInteractiveServerComponents();

        services.AddTransient<ICookie, Cookie>();
        services.AddTransient<IIdentityProviderHttpClient, IdentityProviderHttpClient>();
        services.AddTransient<IAuthenticationService, AuthenthicationService>();
        services.AddTransient<IPublicationService, PublicationService>();
        services.AddTransient<IModeratorService, ModeratorService>();
        services.AddTransient<IAdminService, AdminService>();
        services.AddTransient<IUserService, UserService>();
        services.AddHttpContextAccessor();
        services.AddHttpClient();
        services.Configure<ApiOptions>(configuration.GetSection(ApiOptions.Section));
    }
}
