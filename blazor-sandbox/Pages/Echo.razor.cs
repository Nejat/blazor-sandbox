using System;
using System.Net.Http;
using System.Threading.Tasks;

using Grpc.Net.Client;

using Microsoft.AspNetCore.Components;

using Sandbox.gRPC;

using static System.Net.Http.HttpClientHandler;

using static Grpc.Net.Client.GrpcChannel;

namespace Sandbox.Blazor.Pages
{
    public class EchoBase : ComponentBase
    {
        private Greeter.GreeterClient? Client { get; set; }

        protected string Name { get; set; } = string.Empty;

        protected string Response { get; private set; } = string.Empty;

        protected string Error { get; private set; } = string.Empty;

        protected bool HasError => Error.Length > 0;

        protected bool HasResponse => Response.Length > 0;
        
        protected bool ValidName => Name.Length > 0;

        protected async Task SendMessage ()
        {
            if (Client is null) return;

            try
            {
                Response = 
                    (
                        await Client.SayHelloAsync
                            (
                                new HelloRequest
                                {
                                    Name = Name
                                }
                            )
                    ).Message;
            } 
            catch (Exception exception)
            {
                Error = exception.Message;
            }
        }

        #region Overrides of ComponentBase

        protected override Task OnInitializedAsync ()
        {
            try
            {
#if DEBUG
                var httpClientHandler = new HttpClientHandler
                                        {
                                            // Return `true` to allow certificates that are untrusted/invalid
                                            ServerCertificateCustomValidationCallback =
                                                DangerousAcceptAnyServerCertificateValidator
                                        };

                var httpClient = new HttpClient(httpClientHandler);

                var channel = ForAddress
                (
                    address: "https://grpc-sandbox"
                  , new GrpcChannelOptions
                    {
                        HttpClient = httpClient
                    }
                );
#else
                var channel = GrpcChannel.ForAddress(address: "https://grpc-sandbox");
#endif
                Client = new Greeter.GreeterClient(channel);
            } 
            catch (Exception exception)
            {
                Error = exception.Message;
            }

            return base.OnInitializedAsync();
        }


        #endregion
    }
}