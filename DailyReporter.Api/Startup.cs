﻿using Asp.Versioning;
using DailyReporter.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace DailyReporter.Api
{
	public static class Startup
	{
		/// <summary>
		/// Подключение аутентификации и авторизации
		/// </summary>
		/// <param name="services"></param>
		public static void AddAuthentificationAndAutorization(this IServiceCollection services, WebApplicationBuilder builder)
		{
			services.AddAuthorization();
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(o =>
			{
				var options = builder.Configuration.GetSection(JwtSettings.DefaultSection).Get<JwtSettings>();
				var jwtKey = options.JwtKey;
				var issuer = options.Issuer;
				var audience = options.Audience;
				o.Authority = options.Authority;
				o.RequireHttpsMetadata = false;
				o.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidIssuer = issuer,
					ValidAudience = audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
					ValidateAudience = true,
					ValidateIssuer = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true
				};
			});
		}
		public static void AddSwagger(this IServiceCollection services)
		{
			services.AddApiVersioning()
				.AddApiExplorer(options =>
				{
					options.DefaultApiVersion = new ApiVersion(1, 0);
					options.GroupNameFormat = "'v'VVV";
					options.SubstituteApiVersionInUrl = true;
					options.AssumeDefaultVersionWhenUnspecified = true;
				});
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					In = ParameterLocation.Header,
					Description = "Insert token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme()
						{
							Reference = new OpenApiReference()
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
	}
}
