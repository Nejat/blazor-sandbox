using System;
using System.Net.Http;
using System.Threading.Tasks;

using Data.Model;

using static System.Text.Json.JsonSerializer;

namespace Data.Service
{
    public class WeatherForecastService
    {
        public async Task<WeatherForecast[]> GetForecastAsync (DateTime startDate)
        {
            using var client = new HttpClient();

            var request = new HttpRequestMessage
                          {
                              RequestUri = new Uri($"http://data-sandbox/api/weatherforecast/{startDate:yyyy-MM-dd}")
                          };

            var response = await client.SendAsync(request);
            var json     = await response.Content.ReadAsStringAsync();
            var result   = Deserialize<WeatherForecast[]>(json);

            return result;
        }
    }
}