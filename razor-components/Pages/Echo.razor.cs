using System;
using System.Threading.Tasks;

using gRPC.Client;

using Microsoft.AspNetCore.Components;

using Sandbox.gRPC;

namespace Razor.Components.Pages
{
    public class EchoBase : ComponentBase
    {
        [Inject]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private GreeterClient? Client { get; set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        protected string Name { get; set; } = string.Empty;

        protected string Response { get; private set; } = string.Empty;

        protected string Error { get; private set; } = string.Empty;

        protected bool HasError => Error.Length > 0;

        protected bool HasResponse => Response.Length > 0;
        
        protected bool ValidName => Name.Length > 0;

        // ReSharper disable once UnusedMember.Global
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
    }
}