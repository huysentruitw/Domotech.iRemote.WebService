using System;
using Domotech.iRemote.WebService.GraphApi.OutputTypes;
using HotChocolate;

namespace Domotech.iRemote.WebService.GraphApi.Mutations
{
    public sealed class ThermostatMutation
    {
        private readonly int _id;

        public ThermostatMutation(int id)
        {
            _id = id;
        }

        public ThermostatCurveStepMutation CurveStep([Service] IClient client, int id)
        {
            Items.Room room = client.GetRoom(_id);
            return new ThermostatCurveStepMutation(room.CurveSteps[id]);
        }

        public Thermostat ChangeMode([Service] IClient client, ThermostatMode mode)
        {
            Items.Room room = client.GetRoom(_id);

            room.ControlMode = mode switch
            {
                ThermostatMode.Off => RoomControlMode.Off,
                ThermostatMode.DayTemperature => RoomControlMode.Day,
                ThermostatMode.NightTemperature => RoomControlMode.Night,
                ThermostatMode.AircoTemperature => RoomControlMode.AircoTemp,
                ThermostatMode.AircoContinuous => RoomControlMode.AircoContinu,
                ThermostatMode.Curve => RoomControlMode.Auto,
                _ => throw new InvalidOperationException($"Unknown mode {mode}"),
            };

            return Thermostat.Create(room).WithMode(mode);
        }

        public Thermostat SetTargetDayTemperature([Service] IClient client, decimal targetDayTemperature)
        {
            if (targetDayTemperature < 5 || targetDayTemperature > 50)
                throw new ArgumentOutOfRangeException(nameof(targetDayTemperature), "Value must be in the range [5, 50]");

            Items.Room room = client.GetRoom(_id);
            room.DayTemp = (float)targetDayTemperature;
            return Thermostat.Create(room);
        }

        public Thermostat SetTargetNightTemperature([Service] IClient client, decimal targetNightTemperature)
        {
            if (targetNightTemperature < 5 || targetNightTemperature > 50)
                throw new ArgumentOutOfRangeException(nameof(targetNightTemperature), "Value must be in the range [5, 50]");

            Items.Room room = client.GetRoom(_id);
            room.NightTemp = (float)targetNightTemperature;
            return Thermostat.Create(room);
        }

        public Thermostat SetTargetAircoTemperature([Service] IClient client, decimal targetAircoTemperature)
        {
            if (targetAircoTemperature < 5 || targetAircoTemperature > 50)
                throw new ArgumentOutOfRangeException(nameof(targetAircoTemperature), "Value must be in the range [5, 50]");

            Items.Room room = client.GetRoom(_id);
            room.AircoTemp = (float)targetAircoTemperature;
            return Thermostat.Create(room);
        }
    }
}
