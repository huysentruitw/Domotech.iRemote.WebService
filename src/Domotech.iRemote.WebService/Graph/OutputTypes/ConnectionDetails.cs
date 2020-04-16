using Domotech.iRemote.WebService.Services;

namespace Domotech.iRemote.WebService.Graph.OutputTypes
{
    public sealed class ConnectionDetails
    {
        public ConnectionState State { get; set; }

        public int DownloadProgressInPercent { get; set; }
    }
}
