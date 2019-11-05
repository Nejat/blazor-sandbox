using System;
using System.Threading.Tasks;

using static System.Linq.Enumerable;
using static System.Threading.Tasks.Task;

namespace Sandbox.Blazor.Data
{
    public class WeatherForecastService
    {
        private static readonly string[] SUMMARIES = {
                                                         "Freezing", "Bracing", "Chilly", "Cool"
                                                       , "Mild", "Warm", "Balmy", "Hot"
                                                       , "Sweltering", "Scorching"
                                                     };

        public Task<WeatherForecast[]> GetForecastAsync (DateTime startDate)
        {
            var rng = new Random();

            return FromResult
            (
                Range(start: 1, count: 5)
                   .Select
                    (
                        index => new WeatherForecast
                                 {
                                     Date         = startDate.AddDays(index)
                                   , TemperatureC = rng.Next(minValue: -20, maxValue: 55)
                                   , Summary      = SUMMARIES[rng.Next(SUMMARIES.Length)]
                                 }
                    )
                   .ToArray()
            );
        }
    }
}