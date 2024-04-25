using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Application.Common.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using Application.Models.IdentityServer;

namespace Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationServices(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		services.AddHealthChecks();
		
		MapsterConfiguration.ConfigureMappings();

		return services;
	}
}