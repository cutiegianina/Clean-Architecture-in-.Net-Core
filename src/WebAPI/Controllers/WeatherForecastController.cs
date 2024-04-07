using Microsoft.AspNetCore.Mvc;
using Polly;
using System;

namespace Pamakyabe.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
	private static readonly string[] Summaries = new[]
	{
	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	private readonly ILogger<WeatherForecastController> _logger;

	public WeatherForecastController(ILogger<WeatherForecastController> logger)
	{
		_logger = logger;
	}

	[HttpGet(Name = "GetWeatherForecast")]
	public async Task<IActionResult> Get()
	{
		try
		{
			var retryPolicy = Policy
			.Handle<ApplicationException>()
			.RetryAsync(3, (exception, retryCount) =>
			{
				_logger.LogWarning($"Attempt {retryCount}: Retrying due to {exception}");
			});

			var res = await retryPolicy.ExecuteAsync(() =>
			{
				return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
				{
					Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
					TemperatureC = Random.Shared.Next(-20, 55),
					Summary = Summaries[Random.Shared.Next(Summaries.Length)]
				}).ToArray());
			});

			return Ok(res);
		}
		catch (Exception ex)
		{
			_logger.LogError($"Unexpected error caught: {ex.Message}");
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
		
	}
}