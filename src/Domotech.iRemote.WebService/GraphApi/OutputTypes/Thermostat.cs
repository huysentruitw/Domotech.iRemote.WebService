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
                    _ => throw new InvalidOperationException($"Unknown mode {room.ControlMode}"),
                },
                curve: room.CurveSteps.Select(ThermostatCurveStep.Create).ToArray());

        internal Thermostat WithMode(ThermostatMode mode)
            => new Thermostat(
                id: Id,
                name: Name,
                currentTemperature: CurrentTemperature,
                targetDayTemperature: TargetDayTemperature,
                targetNightTemperature: TargetNightTemperature,
                targetAircoTemperature: TargetAircoTemperature,
                mode: mode,
                curve: Curve.Select(ThermostatCurveStep.Create).ToArray());
    }
}
