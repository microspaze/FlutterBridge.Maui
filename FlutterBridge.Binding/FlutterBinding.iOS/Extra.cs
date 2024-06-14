using Foundation;
using ObjCRuntime;

namespace FlutterBinding
{
    public class ConstantsEx
    {
        static NSObject _FlutterEndOfEventStream;
        public static NSObject FlutterEndOfEventStream
        {
            get
            {
                if (_FlutterEndOfEventStream == null)
                {
                    _FlutterEndOfEventStream = Runtime.GetNSObject(Constants.FlutterEndOfEventStream);
                }
                return _FlutterEndOfEventStream;
            }
        }

        static NSObject _FlutterMethodNotImplemented;
        public static NSObject FlutterMethodNotImplemented
        {
            get
            {
                if (_FlutterMethodNotImplemented == null)
                {
                    _FlutterMethodNotImplemented = Runtime.GetNSObject(Constants.FlutterMethodNotImplemented);
                }
                return _FlutterMethodNotImplemented;
            }
        }
    }
}