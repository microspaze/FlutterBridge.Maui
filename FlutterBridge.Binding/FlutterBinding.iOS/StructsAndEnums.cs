using ObjCRuntime;

namespace FlutterBinding
{
    [Native]
    public enum FlutterStandardDataType : long
    {
        UInt8,
        Int32,
        Int64,
        Float64
    }

    public enum FlutterPlatformViewGestureRecognizersBlockingPolicy : uint
    {
        Eager,
        WaitUntilTouchesEnded
    }
}