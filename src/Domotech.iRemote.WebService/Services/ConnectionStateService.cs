using System;
using System.Threading.Tasks;
using Domotech.iRemote.WebService.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Domotech.iRemote.WebService.Services
{
    internal sealed class ConnectionStateService : IConnectionStateService
    {
        private readonly IServiceProvider _serviceProvider;

        private ConnectionState _state = ConnectionState.Unknown;
        private int _downloadProgressInPercent = 0;

        public ConnectionStateService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ConnectionState GetState()
            => _state;

        public int GetDownloadProgressInPercent()
            => _downloadProgressInPercent;

        public async Task UpdateState(ConnectionState state)
        {
            _state = state;
            await NotifyWebClients();
        }

        public async Task UpdateStateAndDownloadProgress(ConnectionState state, int downloadStateProgressInPercent)
        {
            _state = state;
            _downloadProgressInPercent = downloadStateProgressInPercent;
            await NotifyWebClients();
        }

        private async Task NotifyWebClients()
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            IHubContext<ConnectionStateHub> hubContext = scope.ServiceProvider.GetService<IHubContext<ConnectionStateHub>>();
            await hubContext.Clients.All.SendAsync("Changed", new
            {
                State = _state,
                DownloadProgressInPercent = _downloadProgressInPercent,
            });
        }
    }

    public interface IConnectionStateService
    {
        ConnectionState GetState();

        int GetDownloadProgressInPercent();

        Task UpdateState(ConnectionState state);

        Task UpdateStateAndDownloadProgress(ConnectionState state, int downloadStateProgressInPercent);
    }

    public enum ConnectionState
    {
        Unknown,
        Connecting,
        Downloading,
        Ready,
        ConnectionLost,
        Disconnected,
    }
}
