using Foundation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlutterBridge.Maui.Models;
using FlutterBridge.Maui.Extensions;
using FlutterBridge.Maui.Attributes;

namespace FlutterBridge.Maui
{
    internal class FlutterInterop
    {
        /// <summary>
        /// Converts a .NET object to a valid <see cref="NSObject"/>
        /// that can be sent to Flutter as a successful result of
        /// a <see cref="Flutnet.Interop.FlutterMethodChannel"/> method invoke.
        /// </summary>
        public static NSObject ToMethodChannelResult(int value)
        {
            return value.ToProtoBytes().ToByteData();
        }

        /// <summary>
        /// Converts a .NET object to a valid <see cref="NSObject"/>
        /// that can be sent to Flutter as a successful result of
        /// a <see cref="Flutnet.Interop.FlutterMethodChannel"/> method invoke.
        /// </summary>
        public static NSObject ToMethodChannelResult(BridgeMessageInfo message)
        {
            return message.ToProtoBytes().ToByteData();
        }

        /// <summary>
        /// Converts a .NET object to a valid <see cref="NSObject"/>
        /// that can be sent to Flutter as a successful result of
        /// a <see cref="Flutnet.Interop.FlutterMethodChannel"/> method invoke.
        /// </summary>
        public static NSObject ToMethodChannelResult(BridgeEventInfo message)
        {
            return message.ToProtoBytes().ToByteData();
        }
    }
}
