using Domotech.iRemote.WebService.GraphApi.OutputTypes;
using HotChocolate;

namespace Domotech.iRemote.WebService.GraphApi.Mutations
{
    public sealed class SocketMutation
    {
        private readonly int _id;

        public SocketMutation(int id)
        {
            _id = id;
        }

        public Socket On([Service] IClient client)
            => Toggle(client, true);

        public Socket Off([Service] IClient client)
            => Toggle(client, false);

        public Socket Toggle([Service] IClient client)
            => Toggle(client, null);

        private Socket Toggle(IClient client, bool? state)
        {
            Items.Socket socket = client.GetSocket(_id);
            bool newState = state ?? !socket.State;
            socket.State = newState;
            return Socket.Create(socket).WithState(newState);
        }
    }
}
