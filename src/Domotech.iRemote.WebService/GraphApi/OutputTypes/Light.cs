namespace Domotech.iRemote.WebService.GraphApi.OutputTypes
{
    public sealed class Light
    {
        private Light(int id, string name, bool state)
        {
            Id = id;
            Name = name;
            State = state;
        }

        public int Id { get; }

        public string Name { get; }

        public bool State { get; }

        internal static Light Create(Items.Light light)
            => new Light(
                id: light.Index,
                name: light.Name,
                state: light.State);

        internal Light WithState(bool state)
            => new Light(id: Id, name: Name, state: state);
    }
}
