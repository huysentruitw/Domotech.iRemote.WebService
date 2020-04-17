using System;

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
            ThermostatMode mode)
        {
            Id = id;
            Name = name;
            CurrentTemperature = currentTemperature;
            TargetDayTemperature = targetDayTemperature;
            TargetNightTemperature = targetNightTemperature;
            TargetAircoTemperature = targetAircoTemperature;
            Mode = mode;
        }

        public int Id { get; }

        public string Name { get; }

        public string TemperatureUnit { get; } = "°C";

        public decimal CurrentTemperature { get; }

        public decimal TargetDayTemperature { get; }

        public decimal TargetNightTemperature { get; }

        public decimal TargetAircoTemperature { get; }

        public ThermostatMode Mode { get; }

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
                });
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
}
