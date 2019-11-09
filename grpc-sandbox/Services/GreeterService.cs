using System.Threading.Tasks;

using Grpc.Core;

using ILogger = Microsoft.Extensions.Logging.ILogger<Sandbox.gRPC.Services.GreeterService>;

namespace Sandbox.gRPC.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GreeterService : Greeter.GreeterBase
    {
        public GreeterService (ILogger logger) { Logger = logger; }

        private ILogger Logger { get; }

        public override Task<HelloReply> SayHello
        (
            HelloRequest      request
          , ServerCallContext context
        )
        {
            return Task.FromResult
            (
                new HelloReply
                {
                    Message = "Hello " + request.Name
                }
            );
        }
    }
}