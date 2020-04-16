using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Domotech.iRemote.WebService.Services
{
    internal sealed class DomotechService : BackgroundService
    {
        private readonly IClient _client;
        private readonly IConnectionStateService _connectionStateService;
        private readonly DomotechServiceOptions _options;

        public DomotechService(
            IClient client,
            IConnectionStateService connectionStateService,
            IOptions<DomotechServiceOptions> options)
        {
            _client = client;
            _connectionStateService = connectionStateService;
            _options = options.Value;

            WireClientEvents();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Workaround: yield needed to unblock host, see https://github.com/dotnet/extensions/issues/2149
            await Task.Yield();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (!_client.Connected)
                {
                    Connect();
                }

                await Task.Delay(1000);
            }

            _client.Disconnect();
        }

        private void Connect()
        {
            _client.Connect(_options.Host, _options.Port);
        }

        private void WireClientEvents()
        {
            _client.ClientStatus += Handle_ClientStatus;
        }

        private void Handle_ClientStatus(object sender, ClientStatusEventArgs e)
        {
            switch (e.Status)
            {
            case ClientStatusValue.Connecting:
                _connectionStateService.UpdateState(ConnectionState.Connecting);
                break;
            case ClientStatusValue.Downloading:
                _connectionStateService.UpdateStateAndDownloadProgress(ConnectionState.Downloading, downloadStateProgressInPercent: e.Percent);
                break;
            case ClientStatusValue.Ready:
                _connectionStateService.UpdateState(ConnectionState.Ready);
                break;
            case ClientStatusValue.ConnectionLost:
                _connectionStateService.UpdateState(ConnectionState.ConnectionLost);
                break;
            case ClientStatusValue.Disconnected:
                _connectionStateService.UpdateState(ConnectionState.Disconnected);
                break;
            default:
                _connectionStateService.UpdateState(ConnectionState.Unknown);
                break;
            }
        }
    }
}
