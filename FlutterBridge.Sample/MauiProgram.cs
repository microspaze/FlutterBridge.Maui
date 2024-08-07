﻿using FlutterBridge.Maui.Extensions;
using FlutterBridge.Sample.Services;
using Microsoft.Extensions.Logging;

namespace FlutterBridge.Sample
{
    public static class MauiProgram
    {
        private static readonly Dictionary<string, object> _bridgeServices = new()
        {
            { "animals_service", new AnimalsService() },
            { "counter_service", new CounterService() },
        };

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseFlutterBridge((config) =>
                {
                    config.InitialRoute = "/animals";
                    config.BridgeServices = _bridgeServices;
                    config.EnableWebsocket = true;
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
