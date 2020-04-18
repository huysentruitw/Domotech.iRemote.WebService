namespace Domotech.iRemote.WebService.GraphApi.OutputTypes
{
    public sealed class AudioSource
    {
        private AudioSource(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }

        internal static AudioSource Create(Items.AudioSource audioSource)
            => new AudioSource(
                id: audioSource.Index,
                name: audioSource.Name);
    }
}
