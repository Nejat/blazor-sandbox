using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger<Sandbox.gRPC.Services.GreeterService>;

namespace Sandbox.gRPC.Services;

// ReSharper disable once ClassNeverInstantiated.Global
public class GreeterService : Greeter.GreeterBase
{
    private ILogger? Logger { get; }

    public GreeterService(ILogger? logger)
    {
        Logger = logger;
    }

    public override Task<HelloReply> SayHello
    (
        HelloRequest request,
        ServerCallContext context
    )
    {
        Logger?.Log(LogLevel.Information, $"Say Hello - Name: {request.Name}");

        return Task.FromResult
        (
            new HelloReply
            {
                Message = "Hello " + request.Name
            }
        );
    }
}