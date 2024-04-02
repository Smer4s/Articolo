using Domain.Repositories;
using Domain.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Configurations;

public static class DependencyInjection
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
    public static void AddWebServices(this IServiceCollection services)
    {
        services.AddTransient<AuthService>();
        services.AddTransient<UserService>();
        services.AddTransient<TokenService>();
        services.AddTransient<JwtSecurityTokenHandler>();
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
