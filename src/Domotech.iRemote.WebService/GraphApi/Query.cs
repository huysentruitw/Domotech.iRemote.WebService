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

        public AudioSource[] AudioSources([Service] IClient client)
            => client.AudioSources().Select(AudioSource.Create).ToArray();

        public AudioZone[] AudioZones([Service] IClient client)
            => client.AudioZones().Select(AudioZone.Create).ToArray();

        public DimmableLight[] DimmableLights([Service] IClient client)
            => client.Dimmers().Select(DimmableLight.Create).ToArray();

        public Light[] Lights([Service] IClient client)
            => client.Lights().Select(Light.Create).ToArray();

        public Scene[] Scenes([Service] IClient client)
            => client.Scenarios().Select(Scene.Create).ToArray();

        public Shutter[] Shutters([Service] IClient client)
            => client.Shutters().Select(Shutter.Create).ToArray();

        public Socket[] Sockets([Service] IClient client)
            => client.Sockets().Select(Socket.Create).ToArray();

        public Thermostat[] Thermostats([Service] IClient client)
            => client.Rooms().Select(Thermostat.Create).ToArray();

        public Timer[] Timers([Service] IClient client)
            => client.Timers().Select(Timer.Create).ToArray();

        public Variable[] Variables([Service] IClient client)
            => client.LogVars().Select(Variable.Create).ToArray();
    }
}
