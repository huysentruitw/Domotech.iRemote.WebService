namespace Domotech.iRemote.WebService.GraphApi.OutputTypes
{
    public sealed class DimmableLight
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool State { get; set; }

        public int BrightnessInPercent { get; set; }
    }
}
