using Domotech.iRemote.WebService.GraphApi.OutputTypes;
using HotChocolate;

namespace Domotech.iRemote.WebService.GraphApi.Mutations
{
    public sealed class SceneMutation
    {
        private readonly int _id;

        public SceneMutation(int id)
        {
            _id = id;
        }

        public Scene Execute([Service] IClient client)
        {
            Items.Scenario scenario = client.GetScenario(_id);
            scenario.Execute();
            return Scene.Create(scenario).WithIsExecuting(true);
        }
    }
}
