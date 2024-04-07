using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Builder;
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
	}

	public class ApplicationDbContextInitializer
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<ApplicationDbContextInitializer> _logger;

		public ApplicationDbContextInitializer(
			ApplicationDbContext context,
			ILogger<ApplicationDbContextInitializer> logger)
		{
			_context = context;
			_logger = logger;
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
	}
}
