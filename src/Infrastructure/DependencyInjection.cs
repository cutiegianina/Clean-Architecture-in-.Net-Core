using Application.Data;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		string? connectionString = configuration.GetConnectionString("ApplicationDbContext");
		services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((sp, optionsBuilder) =>
		{
			optionsBuilder.UseSqlServer(connectionString);
		});

		return services;
	}
}
