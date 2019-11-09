using System;
using System.Threading.Tasks;

using Data.Model;

using Microsoft.AspNetCore.Components;

using Sandbox.Blazor.Data;

namespace Sandbox.Blazor.Pages
{
    public class FetchDataBase : ComponentBase
    {
        [Inject]
        private WeatherForecastService? ForecastService { get; set; }

        protected WeatherForecast[]? Forecasts { get; private set; }

        #region Overrides of ComponentBase

        protected override async Task OnInitializedAsync ()
        {
            if (ForecastService is null) return;

            Forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
        }

        #endregion
    }
}
