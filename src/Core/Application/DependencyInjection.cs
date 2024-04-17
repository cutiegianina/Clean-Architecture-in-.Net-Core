using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Application.Common.Mappings;

namespace Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		services.AddHealthChecks();
		MapsterConfiguration.ConfigureMappings();

		return services;
	}
}