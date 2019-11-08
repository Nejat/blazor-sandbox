using System;

using System.Text.Json.Serialization;

namespace Data.Model
{
    public class WeatherForecast
    {
        [JsonPropertyName(nameof(Date))]
        public DateTime Date { get; set; }

        [JsonPropertyName(nameof(TemperatureC))]
        public int TemperatureC { get; set; }

        [JsonIgnore] 
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [JsonPropertyName(nameof(Summary))]
        public string Summary { get; set; }
    }
}
