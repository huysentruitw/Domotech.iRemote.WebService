using Domotech.iRemote.WebService.Services;

namespace Domotech.iRemote.WebService.GraphApi.OutputTypes
{
    public sealed class ConnectionDetails
    {
        public ConnectionState State { get; set; }

        public int DownloadProgressInPercent { get; set; }
    }
}
