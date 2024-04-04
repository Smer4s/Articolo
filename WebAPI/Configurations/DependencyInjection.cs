using Domain.Configurations;
using Domain.Entities;
using Domain.Entities.Reactions;
using Domain.Enums;
using Domain.Repositories;
using Domain.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using WebAPI.Models.Dto;

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
        services.AddTransient<HashService>();
        services.AddTransient<PublicationService>();
        services.AddTransient<AdminService>();
    }

    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseConfiguration>(configuration.GetSection(nameof(DatabaseConfiguration)));
        services.AddDbContext<PostgreDbContext>();

        services.AddRepositories();
    }

    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.CreateMap<User, User>();
            cfg.CreateMap<User, GetUserDto>()
            .ForMember(p => p.Role, opt => opt.MapFrom(x => x.Role.ToString()));
            cfg.CreateMap<Publication, GetPublicationDto>()
            .ForMember(p => p.Status, opt => opt.MapFrom(x => x.Status.ToString()));
            cfg.CreateMap<Comment, CommentDto>();
            cfg.CreateMap<PublicationReaction, PublicationReactionDto>();
            cfg.CreateMap<Theme, ThemeDto>();
        });
    }

    public static void ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    LifetimeValidator = AuthOptions.LifetimeValidator,
                    IssuerSigningKey = AuthOptions.SymmetricSecurityKey,
                    ValidateIssuerSigningKey = true,
                };
            });
        services.AddAuthorization();
    }


    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IPublicationRepository, PublicationRepository>();
        services.AddTransient<IThemeRepository, ThemeRepository>();
    }
}
