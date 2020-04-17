namespace Domotech.iRemote.WebService.GraphApi.OutputTypes
{
    public sealed class Scene
    {
        private Scene(int id, string name, bool isExecuting)
        {
            Id = id;
            Name = name;
            IsExecuting = isExecuting;
        }

        public int Id { get; }

        public string Name { get; }

        public bool IsExecuting { get; }

        internal static Scene Create(Items.Scenario scenario)
            => new Scene(
                id: scenario.Index,
                name: scenario.Name,
                isExecuting: scenario.Busy);

        internal Scene WithIsExecuting(bool isExecuting)
            => new Scene(
                id: Id,
                name: Name,
                isExecuting: isExecuting);
    }
}
