using Application.Common.Interfaces;
using Application.Common.Interfaces.Data;
using Application.Common.Interfaces.Services;
using Application.Models.IdentityServer;
using Domain.Abstractions;
using Infrastructure.Data;
using Infrastructure.Data.Context;
using Infrastructure.Data.Interceptors;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Providers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
		services.AddScoped<IJwtTokenService, JwtTokenService>();
		//services.AddScoped<IHttpContextAccessor>();
		services.AddHttpContextAccessor();

		services
			.AddIdentityCore<ApplicationUser>()
			.AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();

		services.AddHealthChecks();
		services.AddDataProtection();

		services.AddCors(options =>
		{
			options.AddPolicy("CorsPolicy", builder =>
			{
				builder.WithOrigins("http://localhost:4200") // Specify the allowed origin
					   .AllowAnyMethod()  // Allows all methods
					   .AllowAnyHeader()  // Allows all headers
					   .AllowCredentials(); // Allows credentials
			});
		});

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme	= JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme		= JwtBearerDefaults.AuthenticationScheme;
			options.DefaultScheme				= JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()!;

			options.TokenValidationParameters = new TokenValidationParameters()
			{
				ValidateIssuer				= true,
				ValidateAudience			= true,
				ValidateLifetime			= true,
				ValidateIssuerSigningKey	= true,
				ValidIssuer					= jwtSettings.Issuer,
				ValidAudience				= jwtSettings.Audience,
				ValidAlgorithms				= new string[] { SecurityAlgorithms.HmacSha512Signature }.AsEnumerable(),
				IssuerSigningKey			= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
			};
		});

		services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));


		return services;
	}
}