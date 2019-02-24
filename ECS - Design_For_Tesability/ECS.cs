using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;

namespace ECS___Design_For_Tesability
{
    public class ECS
    {
        private readonly IHeater _heater;
        private readonly ITemp _temp;
        private readonly IWindow _window;
        private int _lowerTemperatureThreshold;
        private int _upperTemperatureThreshold;

        public int LowerTemperatureThreshold
        {
            get { return _lowerTemperatureThreshold; }
            set
            {
                // Validation is done in the property set method
                // value is the built in name for the set value
                if (value <= _upperTemperatureThreshold)
                    _lowerTemperatureThreshold = value;
                else throw new ArgumentException("Lower threshold must be <= upper threshold");
            }
        }

        public int UpperTemperatureThreshold
        {
            get { return _upperTemperatureThreshold; }
            set
            {
                // Validation is done in the property set method
                // value is the built in name for the set value
                if (value >= _lowerTemperatureThreshold)
                    _upperTemperatureThreshold = value;
                else throw new ArgumentException("Upper threshold must be <= lower threshold");
            }
        }


        public ECS(IHeater heater, ITemp temp, IWindow window, int upperTemperatureThreshold, int lowerTemperatureThreshold)
        {
            _heater = heater;
            _temp = temp;
            _window = window;

            UpperTemperatureThreshold = upperTemperatureThreshold;
            LowerTemperatureThreshold = lowerTemperatureThreshold;
        }

      

        public void Regulate()
        {
            var t = _temp.getTemp();
            // Determine which action to take according to the temperature
            if (t < LowerTemperatureThreshold)
            {
                _heater.TurnOn();
                _window.Close();
            }
            else if (t >= LowerTemperatureThreshold && t <= UpperTemperatureThreshold)
            {
                _heater.TurnOff();
                _window.Close();
            }
            else
            {
                _heater.TurnOff();
                _window.Open();
            }
        }
    }
}
