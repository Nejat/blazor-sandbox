using System;
using System.Linq;

using Data.Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static System.Linq.Enumerable;

using ILogger = Microsoft.Extensions.Logging.ILogger<Sandbox.Data.Controllers.WeatherForecastController>;
using IWeatherForecasts = System.Collections.Generic.IEnumerable<Data.Model.WeatherForecast>;

namespace Sandbox.Data.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] SUMMARIES =
    {
        "Freezing", "Bracing", "Chilly", "Cool"
        , "Mild", "Warm", "Balmy", "Hot"
        , "Sweltering", "Scorching"
    };

    public WeatherForecastController (ILogger? logger = default) { Logger = logger; }

    private ILogger? Logger { get; }

    [HttpGet(template: "{date}")]
    public IWeatherForecasts Get (DateTime? date = default)
    {
        date??=DateTime.Now;
        
        Logger?.Log(LogLevel.Information, $"Get Weather Forecasts for {date}");

        var rng = new Random();

        return Range(start: 1, count: 5)
            .Select
            (
                index => new WeatherForecast
                {
                    Date         = date.Value.AddDays(index)
                    , TemperatureC = rng.Next(minValue: -20, maxValue: 55)
                    , Summary      = SUMMARIES[rng.Next(SUMMARIES.Length)]
                }
            );
    }
}