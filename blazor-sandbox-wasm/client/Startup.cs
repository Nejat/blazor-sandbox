using Components.UI.Models;

using Data.Service;

using Microsoft.AspNetCore.Components.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

using static Microsoft.AspNetCore.Components.Routing.NavLinkMatch;

using INavItems = System.Collections.Generic.IEnumerable<Components.UI.Models.NavItem>;
using NavItems = System.Collections.Generic.List<Components.UI.Models.NavItem>;

// ReSharper disable once CheckNamespace
namespace Sandbox.Blazor.Client
{
    public class Startup
    {
        public void ConfigureServices (IServiceCollection services)
        {
            services.AddSingleton<WeatherForecastService>();

            services.AddSingleton<INavItems>
            (
                _ => new NavItems
                     {
                         new NavItem
                         {
                             Text           = "Home"
                           , OpenIconicIcon = "oi-home"
                           , Match          = All
                         }
                       , new NavItem
                         {
                             Text           = "Counter"
                           , Route          = "counter"
                           , OpenIconicIcon = "oi-plus"
                         }
                       , new NavItem
                         {
                             Text           = "Fetch Data"
                           , Route          = "fetchdata"
                           , OpenIconicIcon = "oi-list-rich"
                         }
                       , new NavItem
                         {
                             Text           = "Updates"
                           , Route          = "updates"
                           , OpenIconicIcon = "oi-bullhorn"
                         }
                     }
            );

            services.AddTransient
            (
                _ => new HubConnectionBuilder().WithUrl(url: "http://signalr-sandbox/realtime")
                                               .WithAutomaticReconnect()
                                               .Build()
            );
        }

        // ReSharper disable once UnusedMember.Global
        public void Configure (IComponentsApplicationBuilder app) { app.AddComponent<App>(domElementSelector: "app"); }
    }
}