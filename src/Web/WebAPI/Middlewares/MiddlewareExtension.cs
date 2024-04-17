using Microsoft.AspNetCore.Builder;

namespace WebAPI.Middlewares;

public static class MiddlewareExtension
{
	public static IApplicationBuilder UseApplicationLogger(this IApplicationBuilder app) =>
		app.UseMiddleware<ApplicationLogger>();
}