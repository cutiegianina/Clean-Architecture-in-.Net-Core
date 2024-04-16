using Application.Common.Interfaces;
using Application.Common.Interfaces.Data;
using Infrastructure.Data;
using Infrastructure.Data.Context;
using Infrastructure.Data.Interceptors;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
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

		services.AddScoped<ApplicationDbContext>();
		services.AddScoped<AuditableEntityInterceptor>();
		services.AddScoped<ApplicationDbContextInitializer>();
		services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
		services.AddScoped<IArgon2Hasher, Argon2Hasher>();

		services
			.AddIdentityCore<ApplicationUser>()
			.AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();

		services.AddDataProtection();


		return services;
	}
}