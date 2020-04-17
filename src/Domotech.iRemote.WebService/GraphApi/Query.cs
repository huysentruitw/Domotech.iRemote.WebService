using System.Linq;
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

        public Light[] Lights([Service] IClient client)
            => client.Lights().Select(x => new Light
            {
                Id = x.Index,
                Name = x.Name,
                State = x.State,
            })
            .ToArray();

        public DimmableLight[] DimmableLights([Service] IClient client)
            => client.Dimmers().Select(x => new DimmableLight
            {
                Id = x.Index,
                Name = x.Name,
                State = x.State,
                BrightnessInPercent = x.Value,
            })
            .ToArray();

        public Socket[] Sockets([Service] IClient client)
            => client.Sockets().Select(x => new Socket
            {
                Id = x.Index,
                Name = x.Name,
                State = x.State,
            })
            .ToArray();

        public Shutter[] Shutters([Service] IClient client)
            => client.Shutters().Select(x => new Shutter
            {
                Id = x.Index,
                Name = x.Name,
            })
            .ToArray();

        public Scene[] Scenes([Service] IClient client)
            => client.Scenarios().Select(x => new Scene
            {
                Id = x.Index,
                Name = x.Name,
                IsExecuting = x.Busy,
            })
            .ToArray();
    }
}
