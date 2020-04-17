using Domotech.iRemote.WebService.GraphApi.Mutations;

namespace Domotech.iRemote.WebService.GraphApi
{
    public sealed class Mutation
    {
        public LightMutation Light(int id) => new LightMutation(id);

        public DimmableLightMutation DimmableLight(int id) => new DimmableLightMutation(id);

        public SocketMutation Socket(int id) => new SocketMutation(id);

        public ShutterMutation Shutter(int id) => new ShutterMutation(id);

        public SceneMutation Scene(int id) => new SceneMutation(id);
    }
}
