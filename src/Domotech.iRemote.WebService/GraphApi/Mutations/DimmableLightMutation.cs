using Domotech.iRemote.WebService.GraphApi.OutputTypes;
using HotChocolate;

namespace Domotech.iRemote.WebService.GraphApi.Mutations
{
    public sealed class DimmableLightMutation
    {
        private readonly int _id;

        public DimmableLightMutation(int id)
        {
            _id = id;
        }

        public DimmableLight On([Service] IClient client)
            => Toggle(client, true);

        public DimmableLight Off([Service] IClient client)
            => Toggle(client, false);

        public DimmableLight Toggle([Service] IClient client)
            => Toggle(client, null);

        private DimmableLight Toggle(IClient client, bool? state)
        {
            Items.Dimmer dimmer = client.GetDimmer(_id);
            bool newState = state ?? !dimmer.State;
            dimmer.State = newState;
            return new DimmableLight
            {
                Id = dimmer.Index,
                Name = dimmer.Name,
                State = newState,
                BrightnessInPercent = dimmer.Value,
            };
        }

        public DimmableLight IncreaseBrightness([Service] IClient client)
            => OffsetValue(client, 5);

        public DimmableLight DecreaseBrightness([Service] IClient client)
            => OffsetValue(client, -5);

        private DimmableLight OffsetValue(IClient client, int brightnessOffset)
        {
            Items.Dimmer dimmer = client.GetDimmer(_id);
            int newBrightness = dimmer.Value + brightnessOffset;
            if (newBrightness > 100) newBrightness = 100;
            if (newBrightness < 0) newBrightness = 0;
            dimmer.Value = (byte)newBrightness;
            return new DimmableLight
            {
                Id = dimmer.Index,
                Name = dimmer.Name,
                State = newBrightness > 0,
                BrightnessInPercent = newBrightness,
            };
        }
    }
}
