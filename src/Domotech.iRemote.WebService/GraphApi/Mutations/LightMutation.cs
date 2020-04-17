using Domotech.iRemote.WebService.GraphApi.OutputTypes;
using HotChocolate;

namespace Domotech.iRemote.WebService.GraphApi.Mutations
{
    public sealed class LightMutation
    {
        private readonly int _id;

        public LightMutation(int id)
        {
            _id = id;
        }

        public Light On([Service] IClient client)
            => Toggle(client, true);

        public Light Off([Service] IClient client)
            => Toggle(client, false);

        public Light Toggle([Service] IClient client)
            => Toggle(client, null);

        private Light Toggle(IClient client, bool? state)
        {
            Items.Light light = client.GetLight(_id);
            bool newState = state ?? !light.State;
            light.State = newState;
            return Light.Create(light).WithState(newState);
        }
    }
}
