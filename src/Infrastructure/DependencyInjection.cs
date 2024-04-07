using Application.Common.Interfaces.Data;
using Infrastructure.Data.Context;
using Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructureServices(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		string? connectionString = configuration.GetConnectionString("ApplicationDbContext");
		services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((sp, optionsBuilder) =>
		{
			var auditableInterceptor = sp.GetRequiredService<AuditableEntityInterceptor>();
			optionsBuilder.UseSqlServer(connectionString,
					o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
				.AddInterceptors(auditableInterceptor);
		});
		services.AddScoped<AuditableEntityInterceptor>();

		return services;
	}
}