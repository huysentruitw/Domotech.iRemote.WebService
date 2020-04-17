namespace Domotech.iRemote.WebService.GraphApi.OutputTypes
{
    public sealed class Shutter
    {
        private Shutter(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }

        internal static Shutter Create(Items.Shutter shutter)
            => new Shutter(
                id: shutter.Index,
                name: shutter.Name);
    }
}
