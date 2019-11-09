using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

using static System.Guid;
using static System.Threading.Tasks.Task;

namespace Sandbox.SignalR
{
    public class StatusMonitor : BackgroundService
    {
        public StatusMonitor (IHubContext<RealtimeHub> hubContext) { HubContext = hubContext; }

        private IHubContext<RealtimeHub> HubContext { get; }

        #region Overrides of BackgroundService

        protected override async Task ExecuteAsync (CancellationToken cancellation)
        {
            var random = new Random(Seed: 42);

            loop:

            await Delay(random.Next(minValue: 1, maxValue: 5) * 100, cancellation);

            await HubContext.Clients.All.SendAsync(method: "statusUpdate", NewGuid().ToString(), cancellation);

            goto loop;
        }

        #endregion
    }
}
