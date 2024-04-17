using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Middlewares;

public class ApplicationLogger : IMiddleware
{
	private readonly ILogger<ApplicationLogger> _logger;
	public ApplicationLogger(ILogger<ApplicationLogger> logger) =>
		_logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error has occured.");
		}
	}
}