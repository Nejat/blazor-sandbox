using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using static Microsoft.Extensions.Hosting.Host;

namespace Sandbox.Blazor
{
    internal class Program
    {
        public static void Main (string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder (string[] args)
        {
            return CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        }
    }
}