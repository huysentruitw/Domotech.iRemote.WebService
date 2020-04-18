using System;
using Domotech.iRemote.WebService.GraphApi.OutputTypes;
using HotChocolate;

namespace Domotech.iRemote.WebService.GraphApi.Mutations
{
    public sealed class AudioZoneMutation
    {
        private readonly int _id;

        public AudioZoneMutation(int id)
        {
            _id = id;
        }

        public AudioZone On([Service] IClient client)
            => Toggle(client, true);

        public AudioZone Off([Service] IClient client)
            => Toggle(client, false);

        public AudioZone Toggle([Service] IClient client)
            => Toggle(client, null);

        private AudioZone Toggle(IClient client, bool? state)
        {
            Items.AudioZone audioZone = client.GetAudioZone(_id);
            bool newState = state ?? !audioZone.State;
            audioZone.State = newState;
            return AudioZone.Create(audioZone).WithState(newState);
        }

        public AudioZone Mute([Service] IClient client)
            => ToggleMute(client, true);

        public AudioZone Unmute([Service] IClient client)
            => ToggleMute(client, false);

        public AudioZone ToggleMute([Service] IClient client)
            => ToggleMute(client, null);

        private AudioZone ToggleMute(IClient client, bool? mute)
        {
            Items.AudioZone audioZone = client.GetAudioZone(_id);
            bool newMute = mute ?? !audioZone.Mute;
            audioZone.Mute = newMute;
            return AudioZone.Create(audioZone).WithIsMuted(newMute);
        }

        public AudioZone ChangeSource([Service] IClient client, int audioSourceId)
        {
            if (audioSourceId < 0 || audioSourceId >= client.AudioSourceCount)
                throw new ArgumentOutOfRangeException(nameof(audioSourceId), $"Value must be in the range [0, {client.AudioSourceCount}[");

            Items.AudioZone audioZone = client.GetAudioZone(_id);
            audioZone.Source = (byte)audioSourceId;
            return AudioZone.Create(audioZone).WithAudioSourceId(audioSourceId);
        }

        public AudioZone SetVolume([Service] IClient client, int volume)
        {
            if (volume < -80 || volume > 0)
                throw new ArgumentOutOfRangeException(nameof(volume), "Value must be in the range [-80, 0]");

            Items.AudioZone audioZone = client.GetAudioZone(_id);
            audioZone.Volume = volume;
            return AudioZone.Create(audioZone).WithVolume(volume);
        }

        public AudioZone SetTreble([Service] IClient client, int treble)
        {
            if (treble < -12 || treble > 12)
                throw new ArgumentOutOfRangeException(nameof(treble), "Value must be in the range [-12, 12]");

            Items.AudioZone audioZone = client.GetAudioZone(_id);
            audioZone.Treble = treble;
            return AudioZone.Create(audioZone).WithTreble(treble);
        }

        public AudioZone SetBass([Service] IClient client, int bass)
        {
            if (bass < -12 || bass > 12)
                throw new ArgumentOutOfRangeException(nameof(bass), "Value must be in the range [-12, 12]");

            Items.AudioZone audioZone = client.GetAudioZone(_id);
            audioZone.Bass = bass;
            return AudioZone.Create(audioZone).WithBass(bass);
        }
    }
}
