using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Application.Common.Mappings;
using Application.Middlewares;

namespace Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddTransient<ApplicationLogger>();
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		services.AddHealthChecks();
		MapsterConfiguration.ConfigureMappings();

		return services;
	}
}