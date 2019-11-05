using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using static System.Linq.Enumerable;

using ILogger = Microsoft.Extensions.Logging.ILogger<Sandbox.Data.Controllers.WeatherForecastController>;
using IWeatherForecasts = System.Collections.Generic.IEnumerable<Sandbox.Data.WeatherForecast>;

namespace Sandbox.Data.Controllers
{
    [ApiController]
    [Route(template: "[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] SUMMARIES =
        {
            "Freezing", "Bracing", "Chilly", "Cool"
          , "Mild", "Warm", "Balmy", "Hot"
          , "Sweltering", "Scorching"
        };

        public WeatherForecastController (ILogger? logger = default)
        {
            Logger = logger;
        }

        private ILogger? Logger { get; }

        [HttpGet]
        public IWeatherForecasts Get ()
        {
            var rng = new Random();

            return Range(start: 1, count: 5)
                  .Select
                   (
                       index => new WeatherForecast
                                {
                                    Date         = DateTime.Now.AddDays(index)
                                  , TemperatureC = rng.Next(minValue: -20, maxValue: 55)
                                  , Summary      = SUMMARIES[rng.Next(SUMMARIES.Length)]
                                }
                   );
        }
    }
}