﻿using Domain.Constants;
using Infrastructure.Data.Context;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public static class ApplicationDbContextInitializerExtension
{
	public static async Task InitializeDatabaseAsync(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
		
		await initializer.InitializeAsync();

		await initializer.SeedAsync();
	}
}

public class ApplicationDbContextInitializer
{
	private readonly ApplicationDbContext _context;
	private readonly ILogger<ApplicationDbContextInitializer> _logger;
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;

	public ApplicationDbContextInitializer(
		ApplicationDbContext context,
		ILogger<ApplicationDbContextInitializer> logger,
		UserManager<ApplicationUser> userManager,
		RoleManager<IdentityRole> roleManager)
	{
		_context = context;
		_logger = logger;
		_userManager = userManager;
		_roleManager = roleManager;
	}

	public async Task InitializeAsync()
	{
		try
		{
			await _context.Database.MigrateAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occured while initializing the database.");
			throw;
		}
	}

	public async Task SeedAsync()
	{
		try
		{
			await TrySeedAsync();
		} catch (Exception ex)
		{
			_logger.LogError(ex, "An error occured while seeding the database!");
		}
	}

	public async Task TrySeedAsync()
	{
		var administratorRole = new IdentityRole(Roles.Administrator);

		if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
			await _roleManager.CreateAsync(administratorRole);

		var administrator = new ApplicationUser() {
			UserName	= Environment.GetEnvironmentVariable("ADMIN_USERNAME"),
			Email		= Environment.GetEnvironmentVariable("ADMIN_EMAIL")
		};

		if (_userManager.Users.All(u => u.UserName != administrator.UserName))
		{
			await _userManager.CreateAsync(administrator, Environment.GetEnvironmentVariable("ADMIN_PASSWORD")!);
			if (!string.IsNullOrWhiteSpace(administratorRole.Name))
				await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
		}
			



	}
}
