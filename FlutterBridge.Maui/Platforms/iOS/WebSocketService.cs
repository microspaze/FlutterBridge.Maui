using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace FlutterBridge.Maui
{
    internal class WebSocketService : IDisposable
    {
        readonly nint _taskId;
        readonly BridgeWebSocket? _socket;

        public WebSocketService()
        {
            // Generate a long task when start the debug service
            _taskId = UIApplication.SharedApplication.BeginBackgroundTask("FlutnetWebSocket", () =>
            {
                // Expiration handler do nothing
            });

            _socket = new BridgeWebSocket();
            _socket.Start();
        }

        public void Dispose()
        {
            _socket?.Stop();

            UIApplication.SharedApplication.EndBackgroundTask(_taskId);
        }
    }
}
