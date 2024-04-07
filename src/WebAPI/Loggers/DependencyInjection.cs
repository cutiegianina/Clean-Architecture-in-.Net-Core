using Serilog;
using Serilog.Events;

namespace WebAPI.Loggers;
public static class DependencyInjection
{
	public static IHostBuilder AddApplicationLogs(this IHostBuilder hostBuilder) =>
		 hostBuilder
			.UseSerilog((context, services, configuration) => configuration
			.ReadFrom.Configuration(context.Configuration)
			.ReadFrom.Services(services)
			.Enrich.FromLogContext()
			.WriteTo.Console()
			.WriteTo.File(@"C:\Logs\ApplicationLogs.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Information));
}