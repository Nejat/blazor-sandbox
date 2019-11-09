using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

using Toolbox;

using static System.String;
using static System.Threading.Tasks.Task;

namespace Sandbox.Blazor.Pages
{
    public class UpdatesBase : ComponentBase
    {
        private readonly HubConnection _connection = new HubConnectionBuilder()
                                                    .WithUrl(url: "http://signalr-sandbox/realtime")
                                                    .WithAutomaticReconnect()
                                                    .Build();

        private bool Connected { get; set; }

        private bool Receive { get; set; }

        protected string ErrorMessage { get; private set; } = Empty;

        protected bool NoErrorMessage => !IsNullOrWhiteSpace(ErrorMessage);

        private protected bool NoConnectionIssues => !Receive || Connected;

        protected RingBuffer<string> Updates { get; } = new RingBuffer<string>(capacity: 10);

        protected string UpdatesButtonLabel => Receive ? "Disable" : "Enable";

        protected async Task UpdatesClicked ()
        {
            ErrorMessage = Empty;

            Receive = !Receive;

            if (Receive)
            {
                try
                {
                    Connected = true;

                    await _connection.StartAsync();
                } 
                catch (Exception exception)
                {
                    ErrorMessage = exception.Message;
                    Connected    = false;
                }
            }
            else
            {
                try
                {
                    await _connection.StopAsync();

                    PrimeUpdates();
                } 
                catch (Exception exception)
                {
                    ErrorMessage = exception.Message;
                }
            }
        }

        #region Overrides of ComponentBase

        protected override void OnInitialized ()
        {
            PrimeUpdates();

            _connection.Closed += async _ =>
                                  {
                                      ErrorMessage = Empty;
                                      Connected    = false;

                                      if (Receive) await _connection.StartAsync();
                                  };

            _connection.Reconnected += _ =>
                                       {
                                           ErrorMessage = Empty;
                                           Connected    = true;

                                           return CompletedTask;
                                       };

            _connection.Reconnecting += _ =>
                                        {
                                            ErrorMessage = Empty;
                                            Connected    = false;

                                            return CompletedTask;
                                        };

            _connection.On<string>
            (
                methodName: "statusUpdate"
              , status =>
                {
                    Updates.Write(status);

                    StateHasChanged();
                }
            );
        }

        #endregion

        private void PrimeUpdates ()
        {
            Updates.Clear();

            for (var idx = 0; idx < Updates.Capacity; idx++) Updates.Write(value: " waiting ... ");

            StateHasChanged();
        }
    }
}