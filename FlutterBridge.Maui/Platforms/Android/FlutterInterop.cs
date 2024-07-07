using Android.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlutterBridge.Maui.Attributes;
using FlutterBridge.Maui.Models;
using FlutterBridge.Maui.Extensions;

namespace FlutterBridge.Maui
{
    internal class FlutterInterop
    {
        /// <summary>
        /// Converts a .NET object to a valid <see cref="Java.Lang.Object"/>
        /// that can be sent to Flutter as a successful result of
        /// a <see cref="FlutterBinding.Plugin.Common.MethodChannel"/> method invoke.
        /// </summary>
        public static Java.Lang.Object? ToMethodChannelResult(int value)
        {
            return value.ToProtoBytes();
        }

        /// <summary>
        /// Converts a .NET object to a valid <see cref="Java.Lang.Object"/>
        /// that can be sent to Flutter as a successful result of
        /// a <see cref="FlutterBinding.Plugin.Common.MethodChannel"/> method invoke.
        /// </summary>
        public static Java.Lang.Object? ToMethodChannelResult(BridgeMessageInfo message)
        {
            return message.ToProtoBytes();
        }

        /// <summary>
        /// Converts a .NET object to a valid <see cref="Java.Lang.Object"/>
        /// that can be sent to Flutter as a successful result of
        /// a <see cref="FlutterBinding.Plugin.Common.MethodChannel"/> method invoke.
        /// </summary>
        public static Java.Lang.Object? ToMethodChannelResult(BridgeEventInfo message)
        {
            return message.ToProtoBytes();
        }
    }
}
