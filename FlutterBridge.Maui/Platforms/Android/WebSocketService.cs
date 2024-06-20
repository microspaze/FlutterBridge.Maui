using Android.App;
using Android.Content;
using Android.OS;

namespace FlutterBridge.Maui
{
    [Service]
    internal class WebSocketService : Service
    {
        BridgeWebSocket? _socket;

        public override IBinder OnBind(Intent? intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            base.OnCreate();

            _socket = new BridgeWebSocket();
        }

        public override StartCommandResult OnStartCommand(Intent? intent, StartCommandFlags flags, int startId)
        {
            _socket?.Start();

            // Keep the service open
            return StartCommandResult.NotSticky;
        }

        public override void OnTaskRemoved(Intent? rootIntent)
        {
            // The user kill the app from the task manager
            _socket?.Stop();

            base.OnTaskRemoved(rootIntent);
        }
    }
}
