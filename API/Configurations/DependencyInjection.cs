using Domain.Repositories;
using Domain.Services;
using Infrastructure;
using Infrastructure.Repositories;

namespace API.Configurations;

public static class DependencyInjection
{
    public static void AddWebServices(this IServiceCollection services)
    {
        services.AddTransient<AuthService>();

        services.AddTransient<UserService>();

    }

    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseConfiguration>(configuration.GetSection(nameof(DatabaseConfiguration)));
        services.AddDbContext<PostgreDbContext>();

        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
    }
}
