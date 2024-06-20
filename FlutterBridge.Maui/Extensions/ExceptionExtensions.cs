using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Extensions
{
    internal static class ExceptionExtensions
    {
        static readonly Assembly BridgeAssembly = typeof(FlutterBridge).Assembly;

        public static string GetDetails(this Exception exception)
        {
#if ANDROID
            // Get stack trace for the exception with source file information
            StackTrace st = new(exception, true);
            if (st.FrameCount > 0)
            {
                // Get the top stack frame
                StackFrame frame = st.GetFrame(0)!;
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();
                // Get the line number from the stack frame
                var filename = frame.GetFileName();
                // Get the line number from the stack frame
                var details = $"{exception.GetType()} thrown at line {line}, file: {filename}.\nError message: {exception.Message}";
                return details;
            }
#endif

            return exception.ToString();
        }

        /// <summary>
        /// Returns a particular representation of the stack trace for this exception
        /// where methods defined inside Flutnet class library are excluded.
        /// See: https://stackoverflow.com/questions/2973343/how-to-hide-the-current-method-from-exception-stack-trace-in-net
        /// </summary>
        public static string GetStackTraceCleared(this Exception exception)
        {
            var stacktrace = new StackTrace(exception, true);
            var frames = stacktrace.GetFrames() ?? [];
            var selectedFrames = frames.Where(frame => frame.GetMethod()?.DeclaringType?.Assembly != BridgeAssembly).ToList();

            // full stacktrace info list
            var frameInfos = new List<string>();
            foreach (StackFrame frame in selectedFrames)
            {
                if (frame == null)
                    continue;

                string info = new StackTrace(frame).ToString();

                frameInfos.Add(info);
            }

            return frameInfos.Count > 0 ? string.Concat(frameInfos) : exception.GetDetails();


            /*
            return string.Concat(
                new StackTrace(exception, true)
                    .GetFrames()
                    .Where(frame => frame.GetMethod().DeclaringType?.Assembly != BridgeAssembly)
                    // The following line is required as we want the usual stack trace formatting:
                    // StackFrame.ToString() formats differently
                    .Select(frame => new StackTrace(frame).ToString())
                    .ToArray());*/
        }

        /// <summary>
        /// Return all the exception info.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string ToStringCleared(this Exception exception)
        {
            string message = exception.Message;

            string header;

            if (message == null || message.Length <= 0)
            {
                header = exception.GetType().ToString();
            }
            else
            {
                header = exception.GetType().ToString() + ": " + message;
            }

            return $"{header}{Environment.NewLine}{exception.GetStackTraceCleared()}";
        }
    }
}
