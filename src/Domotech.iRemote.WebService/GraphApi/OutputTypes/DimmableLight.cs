namespace Domotech.iRemote.WebService.GraphApi.OutputTypes
{
    public sealed class DimmableLight
    {
        private DimmableLight(int id, string name, bool state, int brightnessInPercent)
        {
            Id = id;
            Name = name;
            State = state;
            BrightnessInPercent = brightnessInPercent;
        }

        public int Id { get; }

        public string Name { get; }

        public bool State { get; }

        public int BrightnessInPercent { get; }

        internal static DimmableLight Create(Items.Dimmer dimmer)
            => new DimmableLight(
                id: dimmer.Index,
                name: dimmer.Name,
                state: dimmer.State,
                brightnessInPercent: dimmer.Value);

        internal DimmableLight WithState(bool state)
            => new DimmableLight(
                id: Id,
                name: Name,
                state: state,
                brightnessInPercent: BrightnessInPercent);

        internal DimmableLight WithBrightnessInPercent(int brightnessInPercent)
            => new DimmableLight(
                id: Id,
                name: Name,
                state: State,
                brightnessInPercent: brightnessInPercent);
    }
}
