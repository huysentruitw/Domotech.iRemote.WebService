using Domotech.iRemote.WebService.GraphApi.OutputTypes;
using HotChocolate;

namespace Domotech.iRemote.WebService.GraphApi.Mutations
{
    public sealed class ShutterMutation
    {
        private readonly int _id;

        public ShutterMutation(int id)
        {
            _id = id;
        }

        public Shutter Open([Service] IClient client)
        {
            Items.Shutter shutter = client.GetShutter(_id);
            shutter.Open();
            return Shutter.Create(shutter);
        }

        public Shutter Close([Service] IClient client)
        {
            Items.Shutter shutter = client.GetShutter(_id);
            shutter.Close();
            return Shutter.Create(shutter);
        }

        public Shutter Stop([Service] IClient client)
        {
            Items.Shutter shutter = client.GetShutter(_id);
            shutter.Stop();
            return Shutter.Create(shutter);
        }
    }
}
