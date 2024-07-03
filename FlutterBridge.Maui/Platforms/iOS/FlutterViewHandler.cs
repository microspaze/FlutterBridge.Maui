using FlutterBinding;
using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace FlutterBridge.Maui
{
    public class FlutterViewHandler : ViewHandler<FlutterView, UIView>
    {
        private FlutterView _flutterView => VirtualView;
        private FlutterViewController? _flutterViewController;

        public static IPropertyMapper<FlutterView, FlutterViewHandler> PropertyMapper = new PropertyMapper<FlutterView, FlutterViewHandler>(ViewHandler.ViewMapper)
        {
        };

        public FlutterViewHandler() : base(PropertyMapper)
        {
        }

        public FlutterViewHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
        {
        }

        protected override UIView CreatePlatformView()
        {
            _flutterViewController ??= new FlutterViewController(BridgeRuntime.Engine!, null, null);
            return _flutterViewController.View;
        }

        protected override void ConnectHandler(UIView platformView)
        {
            base.ConnectHandler(platformView);
        }

        protected override void DisconnectHandler(UIView platformView)
        {
            platformView.Dispose();
            base.DisconnectHandler(platformView);
        }
    }
}
