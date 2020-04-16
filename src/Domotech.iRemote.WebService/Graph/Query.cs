using Domotech.iRemote.WebService.Graph.OutputTypes;
using Domotech.iRemote.WebService.Services;
using HotChocolate;

namespace Domotech.iRemote.WebService.Graph
{
    public sealed class Query
    {
        public ConnectionDetails Connection([Service] IConnectionStateService connectionStateService)
            => new ConnectionDetails
            {
                State = connectionStateService.GetState(),
                DownloadProgressInPercent = connectionStateService.GetDownloadProgressInPercent(),
            };
    }
}
