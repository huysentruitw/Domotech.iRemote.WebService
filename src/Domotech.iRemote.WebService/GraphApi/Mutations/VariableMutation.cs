using Domotech.iRemote.WebService.GraphApi.OutputTypes;
using HotChocolate;

namespace Domotech.iRemote.WebService.GraphApi.Mutations
{
    public sealed class VariableMutation
    {
        private readonly int _id;

        public VariableMutation(int id)
        {
            _id = id;
        }

        public Variable On([Service] IClient client)
            => Toggle(client, true);

        public Variable Off([Service] IClient client)
            => Toggle(client, false);

        public Variable Toggle([Service] IClient client)
            => Toggle(client, null);

        private Variable Toggle(IClient client, bool? state)
        {
            Items.LogVar variable = client.GetLogVar(_id);
            bool newState = state ?? !variable.State;
            variable.State = newState;
            return Variable.Create(variable).WithState(newState);
        }
    }
}
