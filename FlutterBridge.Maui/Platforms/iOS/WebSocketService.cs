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
        readonly BridgeWebSocket? _socket;

        public WebSocketService()
        {
            _socket = new BridgeWebSocket();
            _socket.Start();
        }

        public void Dispose()
        {
            _socket?.Stop();
        }
    }
}
