using Microsoft.AspNetCore.Builder;

namespace Application.Middlewares;

public static class MiddlewareExtension
{
	public static IApplicationBuilder UseApplicationLogger(this IApplicationBuilder app) =>
		app.UseMiddleware<ApplicationLogger>();
}