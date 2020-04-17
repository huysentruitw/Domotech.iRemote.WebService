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
            => client.Lights().Select(Light.Create).ToArray();

        public DimmableLight[] DimmableLights([Service] IClient client)
            => client.Dimmers().Select(DimmableLight.Create).ToArray();

        public Socket[] Sockets([Service] IClient client)
            => client.Sockets().Select(Socket.Create).ToArray();

        public Shutter[] Shutters([Service] IClient client)
            => client.Shutters().Select(Shutter.Create).ToArray();

        public Scene[] Scenes([Service] IClient client)
            => client.Scenarios().Select(Scene.Create).ToArray();
    }
}
