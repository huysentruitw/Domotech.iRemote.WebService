namespace Domotech.iRemote.WebService.GraphApi.OutputTypes
{
    public sealed class Socket
    {
        private Socket(int id, string name, bool state)
        {
            Id = id;
            Name = name;
            State = state;
        }

        public int Id { get; }

        public string Name { get; }

        public bool State { get; }

        internal static Socket Create(Items.Socket socket)
            => new Socket(
                id: socket.Index,
                name: socket.Name,
                state: socket.State);

        internal Socket WithState(bool state)
            => new Socket(
                id: Id,
                name: Name,
                state: state);
    }
}
