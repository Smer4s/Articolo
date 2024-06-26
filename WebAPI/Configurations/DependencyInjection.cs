﻿using Domain.Configurations;
using Domain.Entities;
using Domain.Entities.Reactions;
using Domain.Repositories;
using Domain.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
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
		services.AddTransient<CommentService>();
	}

	public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<DatabaseConfiguration>(configuration.GetSection(nameof(DatabaseConfiguration)));
		services.AddDbContext<PostgreDbContext>();

		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
			.ForMember(p => p.Status, opt => opt.MapFrom(x => x.Status.ToString()))
			.ForMember(p => p.FavouritesCount, opt => opt.MapFrom(x => x.Favourites == null ? 0 : x.Favourites.Count))
			.ForMember(p => p.Favorites, opt => opt.MapFrom(x => x.Favourites == null ? null : x.Favourites.Select(x => x.Id.ToString()).ToList()))
			.ForMember(p => p.Comments, opt => opt.MapFrom(x => x.Comments == null ? null : x.Comments.Select(comment => new CommentDto()
			{
				Id = comment.Id,
				IssuerName = string.IsNullOrEmpty(comment.Issuer.UserName) ? comment.Issuer.Login : comment.Issuer.UserName,
				Posted = comment.Posted,
				Text = comment.Text
			})))
			.ForMember(p => p.Reactions, opt => opt.MapFrom(x => x.Reactions == null ? null : x.Reactions.Select(x => new PublicationReactionDto()
			{
				Id= x.Id,
				ReactionType = x.ReactionType,
				UserId = x.Issuer.Id
			})));
			cfg.CreateMap<Comment, CommentDto>();
			cfg.CreateMap<PublicationReaction, PublicationReactionDto>()
			.ForMember(p=>p.UserId, opt => opt.MapFrom(x=>x.Issuer.Id));
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
		services.AddTransient<ICommentRepository, CommentRepository>();
	}
}
