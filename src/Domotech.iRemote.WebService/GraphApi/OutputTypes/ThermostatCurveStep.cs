using System;

namespace Domotech.iRemote.WebService.GraphApi.OutputTypes
{
    public sealed class ThermostatCurveStep
    {
        private ThermostatCurveStep(
            int thermostatId,
            int id,
            DayOfWeek dayOfWeek,
            int hour,
            int minute,
            decimal targetTemperature)
        {
            ThermostatId = thermostatId;
            Id = id;
            DayOfWeek = dayOfWeek;
            Hour = hour;
            Minute = minute;
            TargetTemperature = targetTemperature;
        }

        public int Id { get; }

        public int ThermostatId { get; }

        public DayOfWeek DayOfWeek { get; }

        public int Hour { get; }

        public int Minute { get; }

        public decimal TargetTemperature { get; }

        internal static ThermostatCurveStep Create(Items.CurveStep step)
            => new ThermostatCurveStep(
                thermostatId: step.Room.Index,
                id: step.Index,
                dayOfWeek: step.Day switch
                {
                    0 => DayOfWeek.Monday,
                    1 => DayOfWeek.Tuesday,
                    2 => DayOfWeek.Wednesday,
                    3 => DayOfWeek.Thursday,
                    4 => DayOfWeek.Friday,
                    5 => DayOfWeek.Saturday,
                    6 => DayOfWeek.Sunday,
                    _ => throw new InvalidOperationException($"Unknown day of week value: {step.Day}"),
                },
                hour: step.Hour,
                minute: step.Minute,
                targetTemperature: (decimal)step.Temperature);

        internal static ThermostatCurveStep Create(ThermostatCurveStep step)
            => new ThermostatCurveStep(
                thermostatId: step.ThermostatId,
                id: step.Id,
                dayOfWeek: step.DayOfWeek,
                hour: step.Hour,
                minute: step.Minute,
                targetTemperature: step.TargetTemperature);

        internal ThermostatCurveStep WithTime(int hour, int minute)
            => new ThermostatCurveStep(
                thermostatId: ThermostatId,
                id: Id,
                dayOfWeek: DayOfWeek,
                hour: hour,
                minute: minute,
                targetTemperature: TargetTemperature);

        internal ThermostatCurveStep WithTargetTemperature(decimal targetTemperature)
            => new ThermostatCurveStep(
                thermostatId: ThermostatId,
                id: Id,
                dayOfWeek: DayOfWeek,
                hour: Hour,
                minute: Minute,
                targetTemperature: targetTemperature);
    }
}
