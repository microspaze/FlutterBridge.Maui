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
        private UIView? _flutterNativeView;

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
            var parentView = _flutterView.Parent.Handler?.PlatformView as UIView;
            var parentViewController = FetchViewController(parentView);
            if (parentView != null && parentViewController != null)
            {
                _flutterViewController ??= new FlutterViewController(BridgeRuntime.Engine!, null, null);
                if (_flutterView.IsTransparen)
                {
                    _flutterViewController.ViewOpaque = false;
                }                
                if (!string.IsNullOrEmpty(_flutterView.InitialRoute))
                {
                    _flutterViewController.PushRoute(_flutterView.InitialRoute);
                }
                parentViewController.AddChildViewController(_flutterViewController);
                _flutterViewController.DidMoveToParentViewController(parentViewController);
                _flutterNativeView = _flutterViewController.View!;
                _flutterNativeView.SetNeedsLayout();
            }
            return _flutterNativeView;
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
        
        private static UIViewController? FetchViewController(UIView? view)
        {
            var responder = view?.NextResponder;
            while (responder != null)
            {
                if (responder is UIViewController parentViewController)
                {
                    return parentViewController;
                }
                responder = responder.NextResponder;
            }
            return null;
        }
    }
}
