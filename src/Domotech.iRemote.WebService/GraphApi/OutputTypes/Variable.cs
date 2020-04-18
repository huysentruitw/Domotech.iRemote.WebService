namespace Domotech.iRemote.WebService.GraphApi.OutputTypes
{
    public sealed class Variable
    {
        private Variable(int id, string name, bool state)
        {
            Id = id;
            Name = name;
            State = state;
        }

        public int Id { get; }

        public string Name { get; }

        public bool State { get; }

        internal static Variable Create(Items.LogVar variable)
            => new Variable(
                id: variable.Index,
                name: variable.Name,
                state: variable.State);

        internal Variable WithState(bool state)
            => new Variable(id: Id, name: Name, state: state);
    }
}
