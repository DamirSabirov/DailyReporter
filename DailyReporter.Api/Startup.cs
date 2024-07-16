using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace DailyReporter.Api
{
	public static class Startup
	{
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
