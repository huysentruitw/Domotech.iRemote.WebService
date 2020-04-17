using System;
using System.Linq;

namespace Domotech.iRemote.WebService.GraphApi.OutputTypes
{
    public sealed class Thermostat
    {
        private Thermostat(
            int id,
            string name,
            decimal currentTemperature,
            decimal targetDayTemperature,
            decimal targetNightTemperature,
            decimal targetAircoTemperature,
            ThermostatMode mode,
            ThermostatCurveStep[] curve)
        {
            Id = id;
            Name = name;
            CurrentTemperature = currentTemperature;
            TargetDayTemperature = targetDayTemperature;
            TargetNightTemperature = targetNightTemperature;
            TargetAircoTemperature = targetAircoTemperature;
            Mode = mode;
            Curve = curve;
        }

        public int Id { get; }

        public string Name { get; }

        public string TemperatureUnit { get; } = "°C";

        public decimal CurrentTemperature { get; }

        public decimal TargetDayTemperature { get; }

        public decimal TargetNightTemperature { get; }

        public decimal TargetAircoTemperature { get; }

        public ThermostatMode Mode { get; }

        public ThermostatCurveStep[] Curve { get; }

        internal static Thermostat Create(Items.Room room)
            => new Thermostat(
                id: room.Index,
                name: room.Name,
                currentTemperature: (decimal)room.MeasuredTemp,
                targetDayTemperature: (decimal)room.DayTemp,
                targetNightTemperature: (decimal)room.NightTemp,
                targetAircoTemperature: (decimal)room.AircoTemp,
                mode: room.ControlMode switch
                {
                    RoomControlMode.Off => ThermostatMode.Off,
                    RoomControlMode.Day => ThermostatMode.DayTemperature,
                    RoomControlMode.Night => ThermostatMode.NightTemperature,
                    RoomControlMode.AircoTemp => ThermostatMode.AircoTemperature,
                    RoomControlMode.AircoContinu => ThermostatMode.AircoContinuous,
                    RoomControlMode.Auto => ThermostatMode.Curve,
                    _ => throw new NotImplementedException($"Unknown mode {room.ControlMode}"),
                },
                curve: room.CurveSteps.Select(ThermostatCurveStep.Create).ToArray());
    }

    public enum ThermostatMode
    {
        Off,
        DayTemperature,
        NightTemperature,
        AircoTemperature,
        AircoContinuous,
        Curve,
    }

    public sealed class ThermostatCurveStep
    {
        private ThermostatCurveStep(
            int id,
            DayOfWeek dayOfWeek,
            int hour,
            int minute,
            decimal targetTemperature)
        {
            Id = id;
            DayOfWeek = dayOfWeek;
            Hour = hour;
            Minute = minute;
            TargetTemperature = targetTemperature;
        }

        public int Id { get; }

        public DayOfWeek DayOfWeek { get; }

        public int Hour { get; }

        public int Minute { get; }

        public decimal TargetTemperature { get; }

        internal static ThermostatCurveStep Create(Items.CurveStep step)
            => new ThermostatCurveStep(
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
    }
}
