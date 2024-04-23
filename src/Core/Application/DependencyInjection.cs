using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Application.Common.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationServices(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		services.AddHealthChecks();
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters()
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = configuration.GetSection("JwtSettings")["Issuer"],
				ValidAudience = configuration.GetSection("JwtSettings")["Audience"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings")["Key"]!))
			};
		});
		MapsterConfiguration.ConfigureMappings();

		return services;
	}
}