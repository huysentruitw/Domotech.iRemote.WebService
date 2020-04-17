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
            return new Shutter
            {
                Id = shutter.Index,
                Name = shutter.Name,
            };
        }

        public Shutter Close([Service] IClient client)
        {
            Items.Shutter shutter = client.GetShutter(_id);
            shutter.Close();
            return new Shutter
            {
                Id = shutter.Index,
                Name = shutter.Name,
            };
        }

        public Shutter Stop([Service] IClient client)
        {
            Items.Shutter shutter = client.GetShutter(_id);
            shutter.Stop();
            return new Shutter
            {
                Id = shutter.Index,
                Name = shutter.Name,
            };
        }
    }
}
