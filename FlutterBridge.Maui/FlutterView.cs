using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui
{
    public class FlutterView : View
    {
        public static readonly BindableProperty InitialRouteProperty = BindableProperty.Create(nameof(InitialRoute), typeof(string), typeof(FlutterView), "/");

        public string InitialRoute
        {
            get => (string)GetValue(InitialRouteProperty);
            set => SetValue(InitialRouteProperty, value);
        }

        public static readonly BindableProperty IsTransparenProperty = BindableProperty.Create(nameof(IsTransparen), typeof(bool), typeof(FlutterView), false);

        public bool IsTransparen
        {
            get => (bool)GetValue(IsTransparenProperty);
            set => SetValue(IsTransparenProperty, value);
        }
    }
}
