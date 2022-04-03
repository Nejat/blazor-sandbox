using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Toolbox;

using static System.String;
using static System.Threading.Tasks.Task;

namespace Razor.Components.Pages;

public class UpdatesBase : ComponentBase
{
    [Inject]
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    private HubConnection? RealtimeConnection { get; set; }

    [Inject]
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    private IConfiguration? Configuration { get; set; }

    private bool Connected { get; set; }

    private bool Receive { get; set; }

    protected string ErrorMessage { get; private set; } = Empty;

    protected bool NoErrorMessage => !IsNullOrWhiteSpace(ErrorMessage);

    private protected bool NoConnectionIssues => !Receive || Connected;

    protected RingBuffer<string>? Updates { get; private set; }

    protected string UpdatesButtonLabel => Receive ? "Disable" : "Enable";

    // ReSharper disable once UnusedMember.Global
    protected async Task UpdatesClicked ()
    {
        if (RealtimeConnection is null)
        {
            ErrorMessage = "SignalR Connection was not configured";

            return;
        }

        ErrorMessage = Empty;

        Receive = !Receive;

        if (Receive)
        {
            try
            {
                Connected = true;

                await RealtimeConnection.StartAsync();
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
                await RealtimeConnection.StopAsync();

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
        if (RealtimeConnection is null || Configuration is null)
        {
            Updates = new RingBuffer<string>(capacity: 0);

            return;
        }

        Updates = new RingBuffer<string>(Configuration.GetValue(key: "Updates-BufferSize", defaultValue: 10));

        PrimeUpdates();

        RealtimeConnection.Closed += async _ =>
        {
            ErrorMessage = Empty;
            Connected    = false;

            if (Receive) await RealtimeConnection.StartAsync();
        };

        RealtimeConnection.Reconnected += _ =>
        {
            ErrorMessage = Empty;
            Connected    = true;

            return CompletedTask;
        };

        RealtimeConnection.Reconnecting += _ =>
        {
            ErrorMessage = Empty;
            Connected    = false;

            return CompletedTask;
        };

        RealtimeConnection.On<string>
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
        if (Updates is null) return;

        Updates.Clear();

        for (var idx = 0; idx < Updates.Capacity; idx++) Updates.Write(value: " waiting ... ");

        StateHasChanged();
    }
}