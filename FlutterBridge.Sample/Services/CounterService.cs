using FlutterBridge.Maui.Attributes;
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

        private int _value = 0;

        [BridgeOperation]
        public int GetValue()
        {
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
