using HotChocolate;

namespace Domotech.iRemote.WebService.GraphApi.OutputTypes
{
    public sealed class AudioZone
    {
        private AudioZone(
            int id,
            string name,
            bool state,
            bool isMuted,
            int audioSourceId,
            int volume,
            int treble,
            int bass)
        {
            Id = id;
            Name = name;
            State = state;
            IsMuted = isMuted;
            AudioSourceId = audioSourceId;
            Volume = volume;
            Treble = treble;
            Bass = bass;
        }

        public int Id { get; }

        public string Name { get; }

        public bool State { get; }

        public bool IsMuted { get; }

        public int AudioSourceId { get; }

        public AudioSource AudioSource([Service] IClient client)
            => OutputTypes.AudioSource.Create(client.GetAudioSource(AudioSourceId));

        public int Volume { get; }

        public int Treble { get; }

        public int Bass { get; }

        internal static AudioZone Create(Items.AudioZone audioZone)
            => new AudioZone(
                id: audioZone.Index,
                name: audioZone.Name,
                state: audioZone.State,
                isMuted: audioZone.Mute,
                audioSourceId: audioZone.Source,
                volume: audioZone.Volume,
                treble: audioZone.Treble,
                bass: audioZone.Bass);

        internal AudioZone WithState(bool state)
            => new AudioZone(
                id: Id,
                name: Name,
                state: state,
                isMuted: IsMuted,
                audioSourceId: AudioSourceId,
                volume: Volume,
                treble: Treble,
                bass: Bass);

        internal AudioZone WithIsMuted(bool isMuted)
            => new AudioZone(
                id: Id,
                name: Name,
                state: State,
                isMuted: isMuted,
                audioSourceId: AudioSourceId,
                volume: Volume,
                treble: Treble,
                bass: Bass);

        internal AudioZone WithAudioSourceId(int audioSourceId)
            => new AudioZone(
                id: Id,
                name: Name,
                state: State,
                isMuted: IsMuted,
                audioSourceId: audioSourceId,
                volume: Volume,
                treble: Treble,
                bass: Bass);

        internal AudioZone WithVolume(int volume)
            => new AudioZone(
                id: Id,
                name: Name,
                state: State,
                isMuted: IsMuted,
                audioSourceId: AudioSourceId,
                volume: volume,
                treble: Treble,
                bass: Bass);

        internal AudioZone WithTreble(int treble)
            => new AudioZone(
                id: Id,
                name: Name,
                state: State,
                isMuted: IsMuted,
                audioSourceId: AudioSourceId,
                volume: Volume,
                treble: treble,
                bass: Bass);

        internal AudioZone WithBass(int bass)
            => new AudioZone(
                id: Id,
                name: Name,
                state: State,
                isMuted: IsMuted,
                audioSourceId: AudioSourceId,
                volume: Volume,
                treble: Treble,
                bass: bass);
    }
}
