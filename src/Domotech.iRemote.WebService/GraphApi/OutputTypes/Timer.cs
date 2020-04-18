using System;

namespace Domotech.iRemote.WebService.GraphApi.OutputTypes
{
    public sealed class Timer
    {
        private Timer(
            int id,
            string name,
            bool isEnabled,
            DayOfWeek dayOfWeek,
            int hour,
            int minute)
        {
            Id = id;
            Name = name;
            IsEnabled = isEnabled;
            DayOfWeek = dayOfWeek;
            Hour = hour;
            Minute = minute;
        }

        public int Id { get; }

        public string Name { get; }

        public bool IsEnabled { get; }

        public DayOfWeek DayOfWeek { get; }

        public int Hour { get; }

        public int Minute { get; }

        internal static Timer Create(Items.Timer timer)
            => new Timer(
                id: timer.Index,
                name: timer.Name,
                isEnabled: timer.State,
                dayOfWeek: timer.Day switch
                {
                    0 => DayOfWeek.Monday,
                    1 => DayOfWeek.Tuesday,
                    2 => DayOfWeek.Wednesday,
                    3 => DayOfWeek.Thursday,
                    4 => DayOfWeek.Friday,
                    5 => DayOfWeek.Saturday,
                    6 => DayOfWeek.Sunday,
                    _ => throw new InvalidOperationException($"Unknown day of week value: {timer.Day}"),
                },
                hour: timer.Hour,
                minute: timer.Minute);

        internal Timer WithIsEnabled(bool isEnabled)
            => new Timer(
                id: Id,
                name: Name,
                isEnabled: isEnabled,
                dayOfWeek: DayOfWeek,
                hour: Hour,
                minute: Minute);

        internal Timer WithTime(int hour, int minute)
            => new Timer(
                id: Id,
                name: Name,
                isEnabled: IsEnabled,
                dayOfWeek: DayOfWeek,
                hour: hour,
                minute: minute);
    }
}
