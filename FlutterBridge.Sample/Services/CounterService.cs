using FlutterBridge.Maui.Attributes;
using FlutterBridge.Maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Sample.Services
{
    [BridgeService]
    public class CounterService
    {
        [BridgeEvent]
        public event EventHandler<CounterValueResult>? ValueChanged;

        private int _value = -1;

        [BridgeOperation("GetValue()")]
        public object GetValue(string name, bool male, int age, long birth, double weight, int[] milks, double[] sleeps, long[] stamps, double[] milstamps, byte[] avatar, CounterValueResult prev)
        {
            //Test Result Parsing in Flutter
            var prevValue = prev.Value;
            switch (prevValue % 13)
            {
                case 0: return _value;
                case 1: return name;
                case 2: return male;
                case 3: return age;
                case 4: return birth;
                case 5: return weight;
                case 6: return milks;
                case 7: return sleeps;
                case 8: return stamps;
                case 9: return milstamps;
                case 10: return avatar;
                case 11: return prevValue;
                case 12: throw new BridgeException(BridgeErrorCode.OperationCanceled, "Exception from MAUI!");
                default: break;
            }

            return _value;
        }

        [BridgeOperation]
        public void Increment()
        {
            _value++;
            OnValueChanged(new()
            {
                Value = _value
            });
        }

        [BridgeOperation]
        public void Decrement()
        {
            _value--;
            OnValueChanged(new()
            {
                Value = _value
            });
        }

        protected virtual void OnValueChanged(CounterValueResult e)
        {
            ValueChanged?.Invoke(this, e);
        }
    }
}
