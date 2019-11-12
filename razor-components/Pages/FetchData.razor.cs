using System.Threading.Tasks;

using Data.Service;

using Microsoft.AspNetCore.Components;

using static System.DateTime;

using IWeatherForecasts = System.Collections.Generic.IEnumerable<Data.Model.WeatherForecast>;

namespace Razor.Components.Pages
{
    public class FetchDataBase : ComponentBase
    {
        [Inject]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private WeatherForecastService? ForecastService { get; set; }

        protected IWeatherForecasts? Forecasts { get; private set; }

        #region Overrides of ComponentBase

        protected override async Task OnInitializedAsync ()
        {
            if (ForecastService is null) return;

            Forecasts = await ForecastService.GetForecastAsync(Now);
        }

        #endregion
    }
}
