using Android.App;
using AndroidX.AppCompat.App;
using AndroidX.Fragment.App;
using FlutterBinding.Embedding.Android;
using FlutterBinding.Embedding.Engine;
using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace FlutterBridge.Maui
{
    public class FlutterViewHandler : ViewHandler<FlutterView, Android.Views.View>
    {
        private FlutterView _flutterView => VirtualView;

        public static IPropertyMapper<FlutterView, FlutterViewHandler> PropertyMapper = new PropertyMapper<FlutterView, FlutterViewHandler>(ViewHandler.ViewMapper)
        {
        };

        public FlutterViewHandler() : base(PropertyMapper)
        {
        }

        public FlutterViewHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
        {
        }

        protected override Android.Views.View CreatePlatformView()
        {
            Android.Views.View? platformView = null;
            var activity = Platform.CurrentActivity;
            if (activity != null && activity is AppCompatActivity appCompatActivity)
            {
                var view = new FragmentContainerView(appCompatActivity) { Id = Android.Views.View.GenerateViewId() };
                var fragmentBuilder = FlutterFragment.WithCachedEngine(BridgeRuntime.DEFAULT_ENGINE_ID);
                if (fragmentBuilder != null)
                {
                    if (_flutterView.IsTransparen)
                    {
                        fragmentBuilder.TransparencyMode(TransparencyMode.Transparent!);
                    }

                    var fragment = (FlutterFragment)fragmentBuilder.Build();
                    var flutterEngine = FlutterEngineCache.Instance.Get(BridgeRuntime.DEFAULT_ENGINE_ID);
                    if (!string.IsNullOrEmpty(_flutterView.InitialRoute))
                    {
                        flutterEngine?.NavigationChannel.PushRoute(_flutterView.InitialRoute);
                    }
                    appCompatActivity.SupportFragmentManager.BeginTransaction().Add(view.Id, fragment).Commit();
                    platformView = view;
                }
            }
            return platformView;
        }

        protected override void ConnectHandler(Android.Views.View platformView)
        {
            base.ConnectHandler(platformView);
        }

        protected override void DisconnectHandler(Android.Views.View platformView)
        {
            platformView.Dispose();
            base.DisconnectHandler(platformView);
        }
    }
}
