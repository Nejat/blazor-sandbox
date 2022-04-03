using Data.Service;

using gRPC.Client;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using INavItems = System.Collections.Generic.IEnumerable<Components.UI.Models.NavItem>;
using NavItems = System.Collections.Generic.List<Components.UI.Models.NavItem>;

namespace Sandbox.Blazor;

public class Startup
{
    public Startup (IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices (IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddSingleton<WeatherForecastService>();
            
        services.AddSingleton<INavItems>
        (
            _ =>
            {
                var items = new NavItems();

                Configuration.Bind(key: "NavigationItems", items);

                return items;
            }
        );
            
        services.AddTransient
        (
            _ => new HubConnectionBuilder().WithUrl
                (
                    Configuration.GetValue
                    (
                        key: "Services.SignalR-Realtime"
                        , defaultValue: "http://signalr-sandbox/realtime"
                    )
                )
                .WithAutomaticReconnect()
                .Build()
        );

        services.AddTransient
        (
            _ =>
            {
                var address = Configuration.GetValue
                (
                    key: "Services.gRPC-Greeter"
                    , defaultValue: "https://grpc-sandbox"
                );
#if DEBUG
                return new GreeterClient(address, dangerousUntrustedCertificates: true);
#else
                    return new GreeterClient(address);
#endif
            }
        );
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure
    (
        IApplicationBuilder app
        , IWebHostEnvironment env
    )
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler(errorHandlingPath: "/Error");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseEndpoints
        (
            endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage(page: "/_Host");
            }
        );
    }
}