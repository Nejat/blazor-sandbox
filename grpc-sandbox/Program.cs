using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using static Microsoft.Extensions.Hosting.Host;

namespace Sandbox.gRPC;

internal static class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        host.Run();
    }

    // Additional configuration is required to successfully run gRPC on macOS.
    // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}