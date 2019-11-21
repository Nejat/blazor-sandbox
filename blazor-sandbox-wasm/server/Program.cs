using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using static Microsoft.AspNetCore.WebHost;

namespace Sandbox.Blazor
{
    public static class Program
    {
        public static void Main (string[] args)
        {
            BuildWebHost(args)
               .Run();
        }

        private static IWebHost BuildWebHost (string[] args)
        {
            var host = CreateDefaultBuilder(args);

            host.UseConfiguration
            (
                new ConfigurationBuilder().AddCommandLine(args)
                                          .Build()
            );

            host.UseStartup<Startup>();

            return host.Build();
        }
    }
}