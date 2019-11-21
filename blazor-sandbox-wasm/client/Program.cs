using Microsoft.AspNetCore.Blazor.Hosting;

using Sandbox.Blazor.Client;

using static Microsoft.AspNetCore.Blazor.Hosting.BlazorWebAssemblyHost;

namespace Sandbox.Blazor
{
    public class Program
    {
        public static void Main ()
        {
            var host = CreateHostBuilder().Build();

            host.Run();
        }

        private static IWebAssemblyHostBuilder CreateHostBuilder ()
        {
            return CreateDefaultBuilder()
               .UseBlazorStartup<Startup>();
        }
    }
}