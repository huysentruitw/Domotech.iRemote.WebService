using Domotech.iRemote.WebService.GraphApi.OutputTypes;
using Domotech.iRemote.WebService.Services;
using HotChocolate;

namespace Domotech.iRemote.WebService.GraphApi
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
