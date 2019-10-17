using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixelCLB.Net.Events
{
    public class ValueChangedEventArgs : EventArgs
    {
        public readonly string LastValue;
        public readonly string NewValue;

        public ValueChangedEventArgs(string LastValue, string NewValue)
        {
            this.LastValue = LastValue;
            this.NewValue = NewValue;
        }
    }

    public class Values
    {
        public Values(string InitialValue)
        {
            _value = InitialValue;
            if (InitialValue == "")
                _value = "Disconnected";
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        protected virtual void OnValueChanged(ValueChangedEventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }


        private string _value;
        public string Value
        {
            get 
            { 
                return _value; 
            }
            set
            {
                OnValueChanged(new ValueChangedEventArgs(_value, value));
                _value = value;
            }
        }
    }
}
