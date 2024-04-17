namespace WebAPI.Middlewares;

public static class DependencyInjection
{
	public static IServiceCollection AddMiddlewareServices(this IServiceCollection services)
	{
		services.AddTransient<ApplicationLogger>();
		return services;
	}
}
