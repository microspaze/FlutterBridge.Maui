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
        public int GetValue(string name, int version, int versionNum, CounterValueResult prevValue)
        {
            if (prevValue != null && prevValue.Value == _value)
            {
                throw new BridgeException(BridgeErrorCode.OperationCanceled, "Value Not Changed!");
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
