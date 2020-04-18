using System;
using Domotech.iRemote.WebService.GraphApi.OutputTypes;

namespace Domotech.iRemote.WebService.GraphApi.Mutations
{
    public sealed class ThermostatCurveStepMutation
    {
        private readonly Items.CurveStep _curveStep;

        public ThermostatCurveStepMutation(Items.CurveStep curveStep)
        {
            _curveStep = curveStep;
        }

        public ThermostatCurveStep SetTime(int hour, int minute)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException(nameof(hour), "Value must be in the range [0, 23]");

            if (minute < 0 || minute > 59)
                throw new ArgumentOutOfRangeException(nameof(minute), "Value must be in the range [0, 59]");

            _curveStep.Hour = (byte)hour;
            _curveStep.Minute = (byte)minute;
            return ThermostatCurveStep.Create(_curveStep).WithTime(hour: hour, minute: minute);
        }

        public ThermostatCurveStep SetTargetTemperature(decimal targetTemperature)
        {
            if (targetTemperature < 5 || targetTemperature > 50)
                throw new ArgumentOutOfRangeException(nameof(targetTemperature), "Value must be in the range [5, 50]");

            _curveStep.Temperature = (float)targetTemperature;
            return ThermostatCurveStep.Create(_curveStep).WithTargetTemperature(targetTemperature);
        }
    }
}
