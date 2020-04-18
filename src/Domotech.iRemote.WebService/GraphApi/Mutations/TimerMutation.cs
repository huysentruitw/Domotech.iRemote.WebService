using System;
using Domotech.iRemote.WebService.GraphApi.OutputTypes;
using HotChocolate;

namespace Domotech.iRemote.WebService.GraphApi.Mutations
{
    public sealed class TimerMutation
    {
        private readonly int _id;

        public TimerMutation(int id)
        {
            _id = id;
        }

        public Timer Enable([Service] IClient client)
            => Toggle(client, true);

        public Timer Disable([Service] IClient client)
            => Toggle(client, false);

        public Timer Toggle([Service] IClient client)
            => Toggle(client, null);

        private Timer Toggle(IClient client, bool? state)
        {
            Items.Timer timer = client.GetTimer(_id);
            bool newState = state ?? !timer.State;
            timer.State = newState;
            return Timer.Create(timer).WithIsEnabled(newState);
        }

        public Timer SetTime([Service] IClient client, int hour, int minute)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException(nameof(hour), "Value must be in the range [0, 23]");

            if (minute < 0 || minute > 59)
                throw new ArgumentOutOfRangeException(nameof(minute), "Value must be in the range [0, 59]");

            Items.Timer timer = client.GetTimer(_id);
            timer.Hour = (byte)hour;
            timer.Minute = (byte)minute;
            return Timer.Create(timer).WithTime(hour: hour, minute: minute);
        }
    }
}
