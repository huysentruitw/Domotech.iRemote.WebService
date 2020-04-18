using Domotech.iRemote.WebService.GraphApi.Mutations;

namespace Domotech.iRemote.WebService.GraphApi
{
    public sealed class Mutation
    {
        public AudioZoneMutation AudioZone(int id) => new AudioZoneMutation(id);

        public DimmableLightMutation DimmableLight(int id) => new DimmableLightMutation(id);

        public LightMutation Light(int id) => new LightMutation(id);

        public SceneMutation Scene(int id) => new SceneMutation(id);

        public ShutterMutation Shutter(int id) => new ShutterMutation(id);

        public SocketMutation Socket(int id) => new SocketMutation(id);

        public ThermostatMutation Thermostat(int id) => new ThermostatMutation(id);

        public TimerMutation Timer(int id) => new TimerMutation(id);

        public VariableMutation Variable(int id) => new VariableMutation(id);
    }
}
