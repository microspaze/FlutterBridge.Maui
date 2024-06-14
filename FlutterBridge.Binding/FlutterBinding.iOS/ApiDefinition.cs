using System;
using CoreGraphics;
using CoreVideo;
using Foundation;
using ObjCRuntime;
using UIKit;
using UserNotifications;

namespace FlutterBinding
{
    [Static]
    interface Constants
    {
        // extern const NSNotificationName _Nonnull FlutterSemanticsUpdateNotification;
        [Field("FlutterSemanticsUpdateNotification", "__Internal")]
        NSString FlutterSemanticsUpdateNotification { get; }

        // extern NSString *const _Nonnull FlutterDefaultDartEntrypoint;
        [Field("FlutterDefaultDartEntrypoint", "__Internal")]
        NSString FlutterDefaultDartEntrypoint { get; }

        // extern NSString *const _Nonnull FlutterDefaultInitialRoute;
        [Field("FlutterDefaultInitialRoute", "__Internal")]
        NSString FlutterDefaultInitialRoute { get; }

        // extern const NSObject * _Nonnull FlutterEndOfEventStream;
        [Field("FlutterEndOfEventStream", "__Internal")]
        [Internal]
        //NSObject FlutterEndOfEventStream { get; }
        IntPtr FlutterEndOfEventStream { get; }

        // extern const NSObject * _Nonnull FlutterMethodNotImplemented;
        [Field("FlutterMethodNotImplemented", "__Internal")]
        [Internal]
        //NSObject FlutterMethodNotImplemented { get; }
        IntPtr FlutterMethodNotImplemented { get; }
    }

    // typedef void (^FlutterBinaryReply)(NSData * _Nullable);
    delegate void FlutterBinaryReply([NullAllowed] NSData reply);

    // typedef void (^FlutterBinaryMessageHandler)(NSData * _Nullable, FlutterBinaryReply _Nonnull);
    delegate void FlutterBinaryMessageHandler([NullAllowed] NSData message, [BlockCallback] FlutterBinaryReply callback);

    // @protocol FlutterBinaryMessenger <NSObject>
    [Protocol]
    interface FlutterBinaryMessenger
    {
        // @required -(void)sendOnChannel:(NSString * _Nonnull)channel message:(NSData * _Nullable)message;
        [Abstract]
        [Export("sendOnChannel:message:")]
        void SendOnChannel(string channel, [NullAllowed] NSData message);

        // @required -(void)sendOnChannel:(NSString * _Nonnull)channel message:(NSData * _Nullable)message binaryReply:(FlutterBinaryReply _Nullable)callback;
        [Abstract]
        [Export("sendOnChannel:message:binaryReply:")]
        void SendOnChannel(string channel, [NullAllowed] NSData message, [NullAllowed] FlutterBinaryReply callback);

        // @required -(void)setMessageHandlerOnChannel:(NSString * _Nonnull)channel binaryMessageHandler:(FlutterBinaryMessageHandler _Nullable)handler;
        [Abstract]
        [Export("setMessageHandlerOnChannel:binaryMessageHandler:")]
        void SetMessageHandlerOnChannel(string channel, [NullAllowed] FlutterBinaryMessageHandler handler);

        // @required -(void)cleanupConnection:(FlutterBinaryMessengerConnection)connection;
        [Abstract]
        [Export("cleanupConnection:")]
        void CleanupConnection(long connection);
    }

    interface IFlutterBinaryMessenger
    {
    }

    // @protocol FlutterMessageCodec
    [Protocol]
    interface FlutterMessageCodec
    {
        // @required +(instancetype _Nonnull)sharedInstance;
        [Static, Abstract]
        [Export("sharedInstance")]
        IFlutterMessageCodec SharedInstance { get; }
        
        // @required -(NSData * _Nullable)encode:(id _Nullable)message;
        [Abstract]
        [Export("encode:")]
        [return: NullAllowed]
        NSData Encode([NullAllowed] NSObject message);

        // @required -(id _Nullable)decode:(NSData * _Nullable)message;
        [Abstract]
        [Export("decode:")]
        [return: NullAllowed]
        NSObject Decode([NullAllowed] NSData message);
    }

    interface IFlutterMessageCodec
    {
    }

    // @interface FlutterBinaryCodec : NSObject <FlutterMessageCodec>
    [BaseType(typeof(NSObject))]
    interface FlutterBinaryCodec : IFlutterMessageCodec
    {
    }

    // @interface FlutterStringCodec : NSObject <FlutterMessageCodec>
    [BaseType(typeof(NSObject))]
    interface FlutterStringCodec : IFlutterMessageCodec
    {
    }

    // @interface FlutterJSONMessageCodec : NSObject <FlutterMessageCodec>
    [BaseType(typeof(NSObject))]
    interface FlutterJSONMessageCodec : IFlutterMessageCodec
    {
    }

    // @interface FlutterStandardWriter : NSObject
    [BaseType(typeof(NSObject))]
    interface FlutterStandardWriter
    {
        // -(instancetype _Nonnull)initWithData:(NSMutableData * _Nonnull)data;
        [Export("initWithData:")]
        IntPtr Constructor(NSMutableData data);

        // -(void)writeByte:(UInt8)value;
        [Export("writeByte:")]
        void WriteByte(byte value);

        // -(void)writeBytes:(const void * _Nonnull)bytes length:(NSUInteger)length;
        [Export("writeBytes:length:")]
        //NOTE: Is this correct? Objective Sharpie output was:
        //unsafe void WriteBytes(void* bytes, nuint length);
        void WriteBytes(IntPtr bytes, nuint length);

        // -(void)writeData:(NSData * _Nonnull)data;
        [Export("writeData:")]
        void WriteData(NSData data);

        // -(void)writeSize:(UInt32)size;
        [Export("writeSize:")]
        void WriteSize(uint size);

        // -(void)writeAlignment:(UInt8)alignment;
        [Export("writeAlignment:")]
        void WriteAlignment(byte alignment);

        // -(void)writeUTF8:(NSString * _Nonnull)value;
        [Export("writeUTF8:")]
        void WriteUTF8(string value);

        // -(void)writeValue:(id _Nonnull)value;
        [Export("writeValue:")]
        void WriteValue(NSObject value);
    }

    // @interface FlutterStandardReader : NSObject
    [BaseType(typeof(NSObject))]
    interface FlutterStandardReader
    {
        // -(instancetype _Nonnull)initWithData:(NSData * _Nonnull)data;
        [Export("initWithData:")]
        IntPtr Constructor(NSData data);

        // -(BOOL)hasMore;
        [Export("hasMore")]
        bool HasMore { get; }

        // -(UInt8)readByte;
        [Export("readByte")]
        byte ReadByte();

        // -(void)readBytes:(void * _Nonnull)destination length:(NSUInteger)length;
        [Export("readBytes:length:")]
        //NOTE: Is this correct? Objective Sharpie output was:
        //ReadBytes(void* destination, nuint length);
        void ReadBytes(IntPtr destination, nuint length);

        // -(NSData * _Nonnull)readData:(NSUInteger)length;
        [Export("readData:")]
        NSData ReadData(nuint length);

        // -(UInt32)readSize;
        [Export("readSize")]
        uint ReadSize();

        // -(void)readAlignment:(UInt8)alignment;
        [Export("readAlignment:")]
        void ReadAlignment(byte alignment);

        // -(NSString * _Nonnull)readUTF8;
        [Export("readUTF8")]
        string ReadUTF8();

        // -(id _Nullable)readValue;
        [Export("readValue")]
        [return: NullAllowed]
        NSObject ReadValue();

        // -(id _Nullable)readValueOfType:(UInt8)type;
        [Export("readValueOfType:")]
        [return: NullAllowed]
        NSObject ReadValueOfType(byte type);
    }

    // @interface FlutterStandardReaderWriter : NSObject
    [BaseType(typeof(NSObject))]
    interface FlutterStandardReaderWriter
    {
        // -(FlutterStandardWriter * _Nonnull)writerWithData:(NSMutableData * _Nonnull)data;
        [Export("writerWithData:")]
        FlutterStandardWriter WriterWithData(NSMutableData data);

        // -(FlutterStandardReader * _Nonnull)readerWithData:(NSData * _Nonnull)data;
        [Export("readerWithData:")]
        FlutterStandardReader ReaderWithData(NSData data);
    }

    // @interface FlutterStandardMessageCodec : NSObject <FlutterMessageCodec>
    [BaseType(typeof(NSObject))]
    interface FlutterStandardMessageCodec : IFlutterMessageCodec
    {
        // +(instancetype _Nonnull)codecWithReaderWriter:(FlutterStandardReaderWriter * _Nonnull)readerWriter;
        [Static]
        [Export("codecWithReaderWriter:")]
        FlutterStandardMessageCodec Create(FlutterStandardReaderWriter readerWriter);
    }

    // @interface FlutterMethodCall : NSObject
    [BaseType(typeof(NSObject))]
    interface FlutterMethodCall
    {
        // +(instancetype _Nonnull)methodCallWithMethodName:(NSString * _Nonnull)method arguments:(id _Nullable)arguments;
        [Static]
        [Export("methodCallWithMethodName:arguments:")]
        FlutterMethodCall Create(string method, [NullAllowed] NSObject arguments);

        // @property (readonly, nonatomic) NSString * _Nonnull method;
        [Export("method")]
        string Method { get; }

        // @property (readonly, nonatomic) id _Nullable arguments;
        [NullAllowed, Export("arguments")]
        NSObject Arguments { get; }
    }

    // @interface FlutterError : NSObject
    [BaseType(typeof(NSObject))]
    interface FlutterError
    {
        // +(instancetype _Nonnull)errorWithCode:(NSString * _Nonnull)code message:(NSString * _Nullable)message details:(id _Nullable)details;
        [Static]
        [Export("errorWithCode:message:details:")]
        FlutterError Create(string code, [NullAllowed] string message, [NullAllowed] NSObject details);

        // @property (readonly, nonatomic) NSString * _Nonnull code;
        [Export("code")]
        string Code { get; }

        // @property (readonly, nonatomic) NSString * _Nullable message;
        [NullAllowed, Export("message")]
        string Message { get; }

        // @property (readonly, nonatomic) id _Nullable details;
        [NullAllowed, Export("details")]
        NSObject Details { get; }
    }

    // @interface FlutterStandardTypedData : NSObject
    [BaseType(typeof(NSObject))]
    interface FlutterStandardTypedData
    {
        // +(instancetype _Nonnull)typedDataWithBytes:(NSData * _Nonnull)data;
        [Static]
        [Export("typedDataWithBytes:")]
        FlutterStandardTypedData FromBytes(NSData data);

        // +(instancetype _Nonnull)typedDataWithInt32:(NSData * _Nonnull)data;
        [Static]
        [Export("typedDataWithInt32:")]
        FlutterStandardTypedData FromInt32(NSData data);

        // +(instancetype _Nonnull)typedDataWithInt64:(NSData * _Nonnull)data;
        [Static]
        [Export("typedDataWithInt64:")]
        FlutterStandardTypedData FromInt64(NSData data);

        // +(instancetype _Nonnull)typedDataWithFloat64:(NSData * _Nonnull)data;
        [Static]
        [Export("typedDataWithFloat64:")]
        FlutterStandardTypedData FromFloat64(NSData data);

        // @property (readonly, nonatomic) NSData * _Nonnull data;
        [Export("data")]
        NSData Data { get; }

        // @property (readonly, nonatomic) FlutterStandardDataType type;
        [Export("type")]
        FlutterStandardDataType Type { get; }

        // @property (readonly, nonatomic) UInt32 elementCount;
        [Export("elementCount")]
        uint ElementCount { get; }

        // @property (readonly, nonatomic) UInt8 elementSize;
        [Export("elementSize")]
        byte ElementSize { get; }
    }

    // @protocol FlutterMethodCodec
    [Protocol]
    interface FlutterMethodCodec
    {
        // @required +(instancetype _Nonnull)sharedInstance;
        [Static, Abstract]
        [Export("sharedInstance")]
        IFlutterMethodCodec SharedInstance { get; }

        // @required -(NSData * _Nonnull)encodeMethodCall:(FlutterMethodCall * _Nonnull)methodCall;
        [Abstract]
        [Export("encodeMethodCall:")]
        NSData EncodeMethodCall(FlutterMethodCall methodCall);

        // @required -(FlutterMethodCall * _Nonnull)decodeMethodCall:(NSData * _Nonnull)methodCall;
        [Abstract]
        [Export("decodeMethodCall:")]
        FlutterMethodCall DecodeMethodCall(NSData methodCall);

        // @required -(NSData * _Nonnull)encodeSuccessEnvelope:(id _Nullable)result;
        [Abstract]
        [Export("encodeSuccessEnvelope:")]
        NSData EncodeSuccessEnvelope([NullAllowed] NSObject result);

        // @required -(NSData * _Nonnull)encodeErrorEnvelope:(FlutterError * _Nonnull)error;
        [Abstract]
        [Export("encodeErrorEnvelope:")]
        NSData EncodeErrorEnvelope(FlutterError error);

        // @required -(id _Nullable)decodeEnvelope:(NSData * _Nonnull)envelope;
        [Abstract]
        [Export("decodeEnvelope:")]
        [return: NullAllowed]
        NSObject DecodeEnvelope(NSData envelope);
    }

    interface IFlutterMethodCodec
    {
    }

    // @interface FlutterJSONMethodCodec : NSObject <FlutterMethodCodec>
    [BaseType(typeof(NSObject))]
    interface FlutterJSONMethodCodec : IFlutterMethodCodec
    {
    }

    // @interface FlutterStandardMethodCodec : NSObject <FlutterMethodCodec>
    [BaseType(typeof(NSObject))]
    interface FlutterStandardMethodCodec : IFlutterMethodCodec
    {
        // +(instancetype _Nonnull)codecWithReaderWriter:(FlutterStandardReaderWriter * _Nonnull)readerWriter;
        [Static]
        [Export("codecWithReaderWriter:")]
        FlutterStandardMethodCodec Create(FlutterStandardReaderWriter readerWriter);
    }

    // typedef void (^FlutterReply)(id _Nullable);
    delegate void FlutterReply([NullAllowed] NSObject reply);

    // typedef void (^FlutterMessageHandler)(id _Nullable, FlutterReply _Nonnull);
    delegate void FlutterMessageHandler([NullAllowed] NSObject message, [BlockCallback] FlutterReply callback);

    // @interface FlutterBasicMessageChannel : NSObject
    [BaseType(typeof(NSObject))]
    interface FlutterBasicMessageChannel : IFlutterBinaryMessenger
    {
        // +(instancetype _Nonnull)messageChannelWithName:(NSString * _Nonnull)name binaryMessenger:(NSObject<FlutterBinaryMessenger> * _Nonnull)messenger;
        [Static]
        [Export("messageChannelWithName:binaryMessenger:")]
        FlutterBasicMessageChannel Create(string name, IFlutterBinaryMessenger messenger);

        // +(instancetype _Nonnull)messageChannelWithName:(NSString * _Nonnull)name binaryMessenger:(NSObject<FlutterBinaryMessenger> * _Nonnull)messenger codec:(NSObject<FlutterMessageCodec> * _Nonnull)codec;
        [Static]
        [Export("messageChannelWithName:binaryMessenger:codec:")]
        FlutterBasicMessageChannel Create(string name, IFlutterBinaryMessenger messenger, IFlutterMessageCodec codec);

        // -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name binaryMessenger:(NSObject<FlutterBinaryMessenger> * _Nonnull)messenger codec:(NSObject<FlutterMessageCodec> * _Nonnull)codec;
        [Export("initWithName:binaryMessenger:codec:")]
        IntPtr Constructor(string name, IFlutterBinaryMessenger messenger, IFlutterMessageCodec codec);

        // -(void)sendMessage:(id _Nullable)message;
        [Export("sendMessage:")]
        void SendMessage([NullAllowed] NSObject message);

        // -(void)sendMessage:(id _Nullable)message reply:(FlutterReply _Nullable)callback;
        [Export("sendMessage:reply:")]
        void SendMessage([NullAllowed] NSObject message, [NullAllowed] FlutterReply callback);

        // -(void)setMessageHandler:(FlutterMessageHandler _Nullable)handler;
        [Export("setMessageHandler:")]
        void SetMessageHandler([NullAllowed] FlutterMessageHandler handler);

        // -(void)resizeChannelBuffer:(NSInteger)newSize;
        [Export("resizeChannelBuffer:")]
        void ResizeChannelBuffer(nint newSize);
    }

    // typedef void (^FlutterResult)(id _Nullable);
    delegate void FlutterResult([NullAllowed] NSObject result);

    // typedef void (^FlutterMethodCallHandler)(FlutterMethodCall * _Nonnull, FlutterResult _Nonnull);
    delegate void FlutterMethodCallHandler(FlutterMethodCall call, [BlockCallback] FlutterResult callback);

    // @interface FlutterMethodChannel : NSObject
    [BaseType(typeof(NSObject))]
    interface FlutterMethodChannel : IFlutterBinaryMessenger
    {
        // +(instancetype _Nonnull)methodChannelWithName:(NSString * _Nonnull)name binaryMessenger:(NSObject<FlutterBinaryMessenger> * _Nonnull)messenger;
        [Static]
        [Export("methodChannelWithName:binaryMessenger:")]
        FlutterMethodChannel Create(string name, IFlutterBinaryMessenger messenger);

        // +(instancetype _Nonnull)methodChannelWithName:(NSString * _Nonnull)name binaryMessenger:(NSObject<FlutterBinaryMessenger> * _Nonnull)messenger codec:(NSObject<FlutterMethodCodec> * _Nonnull)codec;
        [Static]
        [Export("methodChannelWithName:binaryMessenger:codec:")]
        FlutterMethodChannel Create(string name, IFlutterBinaryMessenger messenger, IFlutterMethodCodec codec);

        // -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name binaryMessenger:(NSObject<FlutterBinaryMessenger> * _Nonnull)messenger codec:(NSObject<FlutterMethodCodec> * _Nonnull)codec;
        [Export("initWithName:binaryMessenger:codec:")]
        IntPtr Constructor(string name, IFlutterBinaryMessenger messenger, IFlutterMethodCodec codec);

        // -(void)invokeMethod:(NSString * _Nonnull)method arguments:(id _Nullable)arguments;
        [Export("invokeMethod:arguments:")]
        void InvokeMethod(string method, [NullAllowed] NSObject arguments);

        // -(void)invokeMethod:(NSString * _Nonnull)method arguments:(id _Nullable)arguments result:(FlutterResult _Nullable)callback;
        [Export("invokeMethod:arguments:result:")]
        void InvokeMethod(string method, [NullAllowed] NSObject arguments, [NullAllowed] FlutterResult callback);

        // -(void)setMethodCallHandler:(FlutterMethodCallHandler _Nullable)handler;
        [Export("setMethodCallHandler:")]
        void SetMethodCallHandler([NullAllowed] FlutterMethodCallHandler handler);

        // -(void)resizeChannelBuffer:(NSInteger)newSize;
        [Export("resizeChannelBuffer:")]
        void ResizeChannelBuffer(nint newSize);
    }

    // typedef void (^FlutterEventSink)(id _Nullable);
    delegate void FlutterEventSink([NullAllowed] NSObject @event);

    // @protocol FlutterStreamHandler
    [Protocol]
    interface FlutterStreamHandler
    {
        // @required -(FlutterError * _Nullable)onListenWithArguments:(id _Nullable)arguments eventSink:(FlutterEventSink _Nonnull)events;
        [Abstract]
        [Export("onListenWithArguments:eventSink:")]
        [return: NullAllowed]
        FlutterError OnListen([NullAllowed] NSObject arguments, FlutterEventSink events);

        // @required -(FlutterError * _Nullable)onCancelWithArguments:(id _Nullable)arguments;
        [Abstract]
        [Export("onCancelWithArguments:")]
        [return: NullAllowed]
        FlutterError OnCancel([NullAllowed] NSObject arguments);
    }

    interface IFlutterStreamHandler
    {
    }

    // @interface FlutterEventChannel : NSObject
    [BaseType(typeof(NSObject))]
    interface FlutterEventChannel : IFlutterBinaryMessenger
    {
        // +(instancetype _Nonnull)eventChannelWithName:(NSString * _Nonnull)name binaryMessenger:(NSObject<FlutterBinaryMessenger> * _Nonnull)messenger;
        [Static]
        [Export("eventChannelWithName:binaryMessenger:")]
        FlutterEventChannel Create(string name, IFlutterBinaryMessenger messenger);

        // +(instancetype _Nonnull)eventChannelWithName:(NSString * _Nonnull)name binaryMessenger:(NSObject<FlutterBinaryMessenger> * _Nonnull)messenger codec:(NSObject<FlutterMethodCodec> * _Nonnull)codec;
        [Static]
        [Export("eventChannelWithName:binaryMessenger:codec:")]
        FlutterEventChannel Create(string name, IFlutterBinaryMessenger messenger, IFlutterMethodCodec codec);

        // -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name binaryMessenger:(NSObject<FlutterBinaryMessenger> * _Nonnull)messenger codec:(NSObject<FlutterMethodCodec> * _Nonnull)codec;
        [Export("initWithName:binaryMessenger:codec:")]
        IntPtr Constructor(string name, IFlutterBinaryMessenger messenger, IFlutterMethodCodec codec);

        // -(void)setStreamHandler:(NSObject<FlutterStreamHandler> * _Nullable)handler;
        [Export("setStreamHandler:")]
        void SetStreamHandler([NullAllowed] IFlutterStreamHandler handler);
    }

    /// <summary>
    /// NOTE: This protocol can be safely excluded from binding as it's only needed by native iOS plugins.
    /// </summary>
    // @protocol FlutterPlatformView <NSObject>
    [Protocol]
    interface FlutterPlatformView
    {
        // @required -(UIView * _Nonnull)view;
        [Abstract]
        [Export("view")]
        UIView View { get; }
    }

    interface IFlutterPlatformView
    {
    }

    /// <summary>
    /// NOTE: This protocol can be safely excluded from binding as it's only needed by native iOS plugins.
    /// </summary>
    // @protocol FlutterPlatformViewFactory <NSObject>
    [Protocol]
    interface FlutterPlatformViewFactory
    {
        // @required -(NSObject<FlutterPlatformView> * _Nonnull)createWithFrame:(CGRect)frame viewIdentifier:(int64_t)viewId arguments:(id _Nullable)args;
        [Abstract]
        [Export("createWithFrame:viewIdentifier:arguments:")]
        IFlutterPlatformView CreateWithFrame(CGRect frame, long viewId, [NullAllowed] NSObject args);

        // @optional -(NSObject<FlutterMessageCodec> * _Nonnull)createArgsCodec;
        [Export("createArgsCodec")]
        IFlutterMessageCodec CreateArgsCodec { get; }
    }

    interface IFlutterPlatformViewFactory
    {
    }

    /// <summary>
    /// NOTE: This protocol can be safely excluded from binding as it's only needed by native iOS plugins.
    /// </summary>
    // @protocol FlutterTexture <NSObject>
    [Protocol]
    interface FlutterTexture
    {
        // @required -(CVPixelBufferRef _Nullable)copyPixelBuffer;
        [Abstract]
        [NullAllowed, Export("copyPixelBuffer")]
        //unsafe CVPixelBufferRef* CopyPixelBuffer { get; }
        CVPixelBuffer CopyPixelBuffer { get; }

        // @optional -(void)onTextureUnregistered:(NSObject<FlutterTexture> * _Nonnull)texture;
        [Export("onTextureUnregistered:")]
        void OnTextureUnregistered(IFlutterTexture texture);
    }

    interface IFlutterTexture
    {
    }

    /// <summary>
    /// NOTE: This protocol can be safely excluded from binding as it's only needed by native iOS plugins.
    /// </summary>
    // @protocol FlutterTextureRegistry <NSObject>
    [Protocol]
    interface FlutterTextureRegistry
    {
        // @required -(int64_t)registerTexture:(NSObject<FlutterTexture> * _Nonnull)texture;
        [Abstract]
        [Export("registerTexture:")]
        long RegisterTexture(IFlutterTexture texture);

        // @required -(void)textureFrameAvailable:(int64_t)textureId;
        [Abstract]
        [Export("textureFrameAvailable:")]
        void TextureFrameAvailable(long textureId);

        // @required -(void)unregisterTexture:(int64_t)textureId;
        [Abstract]
        [Export("unregisterTexture:")]
        void UnregisterTexture(long textureId);
    }

    interface IFlutterTextureRegistry
    {
    }

    // @protocol FlutterApplicationLifeCycleDelegate <UNUserNotificationCenterDelegate>
    [Protocol]
    interface FlutterApplicationLifeCycleDelegate : IUNUserNotificationCenterDelegate
    {
        // -(BOOL)application:(UIApplication * _Nonnull)application didFinishLaunchingWithOptions:(NSDictionary * _Nonnull)launchOptions;
        [Export("application:didFinishLaunchingWithOptions:")]
        bool ApplicationFinishedLaunching(UIApplication application, [NullAllowed] NSDictionary launchOptions);

        // -(BOOL)application:(UIApplication * _Nonnull)application willFinishLaunchingWithOptions:(NSDictionary * _Nonnull)launchOptions;
        [Export("application:willFinishLaunchingWithOptions:")]
        bool ApplicationWillFinishLaunching(UIApplication application, [NullAllowed] NSDictionary launchOptions);

        // @optional -(void)applicationDidBecomeActive:(UIApplication * _Nonnull)application;
        [Export("applicationDidBecomeActive:")]
        void ApplicationOnActivated(UIApplication application);

        // @optional -(void)applicationWillResignActive:(UIApplication * _Nonnull)application;
        [Export("applicationWillResignActive:")]
        void ApplicationOnResignActivation(UIApplication application);

        // @optional -(void)applicationDidEnterBackground:(UIApplication * _Nonnull)application;
        [Export("applicationDidEnterBackground:")]
        void ApplicationDidEnterBackground(UIApplication application);

        // @optional -(void)applicationWillEnterForeground:(UIApplication * _Nonnull)application;
        [Export("applicationWillEnterForeground:")]
        void ApplicationWillEnterForeground(UIApplication application);

        // @optional -(void)applicationWillTerminate:(UIApplication * _Nonnull)application;
        [Export("applicationWillTerminate:")]
        void ApplicationWillTerminate(UIApplication application);

        // -(void)application:(UIApplication * _Nonnull)application didRegisterUserNotificationSettings:(UIUserNotificationSettings * _Nonnull)notificationSettings __attribute__((availability(ios, introduced=8.0, deprecated=10.0)));
        [Introduced(PlatformName.iOS, 8, 0)]
        [Deprecated(PlatformName.iOS, 10, 0, message: "See UIApplicationDelegate.DidRegisterUserNotificationSettings deprecation.")]
        [Unavailable(PlatformName.TvOS)]
        [Export("application:didRegisterUserNotificationSettings:")]
        void ApplicationDidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings);

        // -(void)application:(UIApplication * _Nonnull)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData * _Nonnull)deviceToken;
        [Export("application:didRegisterForRemoteNotificationsWithDeviceToken:")]
        void ApplicationRegisteredForRemoteNotifications(UIApplication application, NSData deviceToken);

        // -(void)application:(UIApplication * _Nonnull)application didReceiveRemoteNotification:(NSDictionary * _Nonnull)userInfo fetchCompletionHandler:(void (^ _Nonnull)(UIBackgroundFetchResult))completionHandler;
        [Deprecated(PlatformName.iOS, 10, 0, message: "See UIApplicationDelegate.ReceivedRemoteNotification deprecation.")]
        [Export("application:didReceiveRemoteNotification:fetchCompletionHandler:")]
        void ApplicationReceivedRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler);

        // -(void)application:(UIApplication * _Nonnull)application didReceiveLocalNotification:(UILocalNotification * _Nonnull)notification __attribute__((availability(ios, introduced=4.0, deprecated=10.0)));
        [Introduced(PlatformName.iOS, 4, 0)]
        [Deprecated(PlatformName.iOS, 10, 0, message: "See UIApplicationDelegate.ReceivedLocalNotification deprecation.")]
        [Unavailable(PlatformName.TvOS)]
        [Export("application:didReceiveLocalNotification:")]
        void ApplicationReceivedLocalNotification(UIApplication application, UILocalNotification notification);

        // -(BOOL)application:(UIApplication * _Nonnull)application openURL:(NSURL * _Nonnull)url options:(NSDictionary<UIApplicationOpenURLOptionsKey,id> * _Nonnull)options;
        [Introduced(PlatformName.iOS, 9, 0)]
        [Export("application:openURL:options:")]
        bool ApplicationOpenUrl(UIApplication application, NSUrl url, NSDictionary options);

        // -(BOOL)application:(UIApplication * _Nonnull)application handleOpenURL:(NSURL * _Nonnull)url;
        [Obsoleted(PlatformName.iOS, 9, 0, message: "See UIApplicationDelegate.HandleOpenURL deprecation.")]
        [Unavailable(PlatformName.TvOS)]
        [Export("application:handleOpenURL:")]
        bool ApplicationHandleOpenURL(UIApplication application, NSUrl url);

        // -(BOOL)application:(UIApplication * _Nonnull)application openURL:(NSURL * _Nonnull)url sourceApplication:(NSString * _Nonnull)sourceApplication annotation:(id _Nonnull)annotation;
        [Obsoleted(PlatformName.iOS, 9, 0, message: "See UIApplicationDelegate.OpenUrl(UIApplication, NSUrl, string, NSObject) deprecation.")]
        [Unavailable(PlatformName.TvOS)]
        [Export("application:openURL:sourceApplication:annotation:")]
        bool ApplicationOpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation);

        // -(void)application:(UIApplication * _Nonnull)application performActionForShortcutItem:(UIApplicationShortcutItem * _Nonnull)shortcutItem completionHandler:(void (^ _Nonnull)(BOOL))completionHandler __attribute__((availability(ios, introduced=9.0)));
        [Introduced(PlatformName.iOS, 9, 0)]
        [Unavailable(PlatformName.TvOS)]
        [Export("application:performActionForShortcutItem:completionHandler:")]
        void ApplicationPerformActionForShortcutItem(UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler);

        // -(BOOL)application:(UIApplication * _Nonnull)application handleEventsForBackgroundURLSession:(NSString * _Nonnull)identifier completionHandler:(void (^ _Nonnull)(void))completionHandler;
        [Introduced(PlatformName.iOS, 7, 0)]
        [Export("application:handleEventsForBackgroundURLSession:completionHandler:")]
        bool ApplicationHandleEventsForBackgroundUrl(UIApplication application, string identifier, Action completionHandler);

        // -(BOOL)application:(UIApplication * _Nonnull)application performFetchWithCompletionHandler:(void (^ _Nonnull)(UIBackgroundFetchResult))completionHandler;
        [Introduced(PlatformName.iOS, 7, 0)]
        [Introduced(PlatformName.TvOS, 11, 0)]
        [Export("application:performFetchWithCompletionHandler:")]
        bool ApplicationPerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler);

        // -(BOOL)application:(UIApplication * _Nonnull)application continueUserActivity:(NSUserActivity * _Nonnull)userActivity restorationHandler:(void (^ _Nonnull)(NSArray * _Nonnull))restorationHandler;
        [Introduced(PlatformName.iOS, 8, 0)]
        [Export("application:continueUserActivity:restorationHandler:")]
        bool ApplicationContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler restorationHandler);
    }

    interface IFlutterApplicationLifeCycleDelegate
    {
    }

    /// <summary>
    /// NOTE: This delegate can be safely excluded from binding as it's only needed by native iOS plugins.
    /// </summary>
    // typedef void (*FlutterPluginRegistrantCallback)(NSObject<FlutterPluginRegistry>* registry);
    delegate void FlutterPluginRegistrantCallback(IFlutterPluginRegistry registry);

    /// <summary>
    /// NOTE: This protocol can be safely excluded from binding as it's only needed by native iOS plugins.
    /// </summary>
    // @protocol FlutterPlugin <NSObject, FlutterApplicationLifeCycleDelegate>
    [Protocol]
    interface FlutterPlugin : FlutterApplicationLifeCycleDelegate
    {
        // @required +(void)registerWithRegistrar:(NSObject<FlutterPluginRegistrar> * _Nonnull)registrar;
        [Static, Abstract]
        [Export("registerWithRegistrar:")]
        void Register(IFlutterPluginRegistrar registrar);

        // @optional +(void)setPluginRegistrantCallback:(FlutterPluginRegistrantCallback _Nonnull)callback;
        [Static]
        [Export("setPluginRegistrantCallback:")]
        //NOTE: Is this correct? Objective Sharpie output was:
        //unsafe void SetPluginRegistrantCallback(FlutterPluginRegistrantCallback* callback);
        void SetPluginRegistrantCallback(FlutterPluginRegistrantCallback callback);

        // @optional -(void)handleMethodCall:(FlutterMethodCall * _Nonnull)call result:(FlutterResult _Nonnull)result;
        [Export("handleMethodCall:result:")]
        void HandleMethodCall(FlutterMethodCall call, FlutterResult result);
    }

    interface IFlutterPlugin
    {
    }

    /// <summary>
    /// NOTE: This protocol can be safely excluded from binding as it's only needed by native iOS plugins.
    /// </summary>
    // @protocol FlutterPluginRegistrar <NSObject>
    [Protocol]
    interface FlutterPluginRegistrar
    {
        // @required -(NSObject<FlutterBinaryMessenger> * _Nonnull)messenger;
        [Abstract]
        [Export("messenger")]
        IFlutterBinaryMessenger Messenger { get; }

        // @required -(NSObject<FlutterTextureRegistry> * _Nonnull)textures;
        [Abstract]
        [Export("textures")]
        IFlutterTextureRegistry Textures { get; }

        // @required -(void)registerViewFactory:(NSObject<FlutterPlatformViewFactory> * _Nonnull)factory withId:(NSString * _Nonnull)factoryId;
        [Abstract]
        [Export("registerViewFactory:withId:")]
        void RegisterViewFactory(IFlutterPlatformViewFactory factory, string factoryId);

        // @required -(void)registerViewFactory:(NSObject<FlutterPlatformViewFactory> * _Nonnull)factory withId:(NSString * _Nonnull)factoryId gestureRecognizersBlockingPolicy:(FlutterPlatformViewGestureRecognizersBlockingPolicy)gestureRecognizersBlockingPolicy;
        [Abstract]
        [Export("registerViewFactory:withId:gestureRecognizersBlockingPolicy:")]
        void RegisterViewFactory(IFlutterPlatformViewFactory factory, string factoryId, FlutterPlatformViewGestureRecognizersBlockingPolicy gestureRecognizersBlockingPolicy);

        // @required -(void)publish:(NSObject * _Nonnull)value;
        [Abstract]
        [Export("publish:")]
        void Publish(NSObject value);

        // @required -(void)addMethodCallDelegate:(NSObject<FlutterPlugin> * _Nonnull)delegate channel:(FlutterMethodChannel * _Nonnull)channel;
        [Abstract]
        [Export("addMethodCallDelegate:channel:")]
        void AddMethodCallDelegate(IFlutterPlugin @delegate, FlutterMethodChannel channel);

        // @required -(void)addApplicationDelegate:(NSObject<FlutterPlugin> * _Nonnull)delegate;
        [Abstract]
        [Export("addApplicationDelegate:")]
        void AddApplicationDelegate(IFlutterPlugin @delegate);

        // @required -(NSString * _Nonnull)lookupKeyForAsset:(NSString * _Nonnull)asset;
        [Abstract]
        [Export("lookupKeyForAsset:")]
        string LookupKeyForAsset(string asset);

        // @required -(NSString * _Nonnull)lookupKeyForAsset:(NSString * _Nonnull)asset fromPackage:(NSString * _Nonnull)package;
        [Abstract]
        [Export("lookupKeyForAsset:fromPackage:")]
        string LookupKeyForAsset(string asset, string package);
    }

    interface IFlutterPluginRegistrar
    {
    }

    /// <summary>
    /// NOTE: This protocol can be safely excluded from binding as it's only needed by native iOS plugins.
    /// </summary>
    // @protocol FlutterPluginRegistry <NSObject>
    [Protocol]
    interface FlutterPluginRegistry
    {
        // @required -(NSObject<FlutterPluginRegistrar> * _Nonnull)registrarForPlugin:(NSString * _Nonnull)pluginKey;
        [Abstract]
        [Export("registrarForPlugin:")]
        IFlutterPluginRegistrar RegistrarForPlugin(string pluginKey);

        // @required -(BOOL)hasPlugin:(NSString * _Nonnull)pluginKey;
        [Abstract]
        [Export("hasPlugin:")]
        bool HasPlugin(string pluginKey);

        // @required -(NSObject * _Nonnull)valuePublishedByPlugin:(NSString * _Nonnull)pluginKey;
        [Abstract]
        [Export("valuePublishedByPlugin:")]
        NSObject ValuePublishedByPlugin(string pluginKey);
    }

    interface IFlutterPluginRegistry
    {
    }

    // @protocol FlutterAppLifeCycleProvider <UNUserNotificationCenterDelegate>
    [Protocol]
    interface FlutterAppLifeCycleProvider : IUNUserNotificationCenterDelegate
    {
        // @required -(void)addApplicationLifeCycleDelegate:(NSObject<FlutterApplicationLifeCycleDelegate> * _Nonnull)delegate;
        [Abstract]
        [Export("addApplicationLifeCycleDelegate:")]
        void AddApplicationLifeCycleDelegate(NSObject @delegate);
    }

    interface IFlutterAppLifeCycleProvider
    {
    }

    // @interface FlutterAppDelegate : UIResponder <UIApplicationDelegate, FlutterPluginRegistry, FlutterAppLifeCycleProvider>
    [BaseType(typeof(UIResponder))]
    interface FlutterAppDelegate : IUIApplicationDelegate, IFlutterPluginRegistry, IFlutterAppLifeCycleProvider
    {
        // @property (nonatomic, strong) UIWindow * window;
        [Export("window", ArgumentSemantic.Strong)]
        UIWindow Window { get; set; }

        /// <summary>
        /// NOTE: DO NOT REMOVE this method! It's required as we're (re)defining a base <see href="https://docs.microsoft.com/en-us/dotnet/api/uikit.uiapplicationdelegate?view=xamarin-ios-sdk-12">UIApplicationDelegate</see>.
        /// </summary>
        // @optional -(BOOL)application:(UIApplication * _Nonnull)application didFinishLaunchingWithOptions:(NSDictionary * _Nonnull)launchOptions;
        [Export("application:didFinishLaunchingWithOptions:")]
        bool FinishedLaunching(UIApplication application, [NullAllowed] NSDictionary launchOptions);
    }

    // @interface FlutterCallbackInformation : NSObject
    [BaseType(typeof(NSObject))]
    interface FlutterCallbackInformation
    {
        // @property (retain) NSString * callbackName;
        [Export("callbackName", ArgumentSemantic.Retain)]
        string CallbackName { get; set; }

        // @property (retain) NSString * callbackClassName;
        [Export("callbackClassName", ArgumentSemantic.Retain)]
        string CallbackClassName { get; set; }

        // @property (retain) NSString * callbackLibraryPath;
        [Export("callbackLibraryPath", ArgumentSemantic.Retain)]
        string CallbackLibraryPath { get; set; }
    }

    // @interface FlutterCallbackCache : NSObject
    [BaseType(typeof(NSObject))]
    interface FlutterCallbackCache
    {
        // +(FlutterCallbackInformation *)lookupCallbackInformation:(int64_t)handle;
        [Static]
        [Export("lookupCallbackInformation:")]
        FlutterCallbackInformation LookupCallbackInformation(long handle);
    }

    // @interface FlutterDartProject : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface FlutterDartProject
    {
        // -(instancetype _Nonnull)initWithPrecompiledDartBundle:(NSBundle * _Nullable)bundle __attribute__((objc_designated_initializer));
        [Export("initWithPrecompiledDartBundle:")]
        [DesignatedInitializer]
        IntPtr Constructor([NullAllowed] NSBundle bundle);

        // +(NSString * _Nonnull)lookupKeyForAsset:(NSString * _Nonnull)asset;
        [Static]
        [Export("lookupKeyForAsset:")]
        string LookupKeyForAsset(string asset);

        // +(NSString * _Nonnull)lookupKeyForAsset:(NSString * _Nonnull)asset fromBundle:(NSBundle * _Nullable)bundle;
        [Static]
        [Export("lookupKeyForAsset:fromBundle:")]
        string LookupKeyForAsset(string asset, [NullAllowed] NSBundle bundle);

        // +(NSString * _Nonnull)lookupKeyForAsset:(NSString * _Nonnull)asset fromPackage:(NSString * _Nonnull)package;
        [Static]
        [Export("lookupKeyForAsset:fromPackage:")]
        string LookupKeyForAsset(string asset, string package);

        // +(NSString * _Nonnull)lookupKeyForAsset:(NSString * _Nonnull)asset fromPackage:(NSString * _Nonnull)package fromBundle:(NSBundle * _Nullable)bundle;
        [Static]
        [Export("lookupKeyForAsset:fromPackage:fromBundle:")]
        string LookupKeyForAsset(string asset, string package, [NullAllowed] NSBundle bundle);

        // +(NSString * _Nonnull)defaultBundleIdentifier;
        [Static]
        [Export("defaultBundleIdentifier")]
        string DefaultBundleIdentifier { get; }
    }

    // @interface FlutterEngine : NSObject <FlutterTextureRegistry, FlutterPluginRegistry>
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface FlutterEngine : IFlutterTextureRegistry, IFlutterPluginRegistry
    {
        // -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)labelPrefix;
        [Export("initWithName:")]
        IntPtr Constructor(string labelPrefix);

        // -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)labelPrefix project:(FlutterDartProject * _Nullable)project;
        [Export("initWithName:project:")]
        IntPtr Constructor(string labelPrefix, [NullAllowed] FlutterDartProject project);

        // -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)labelPrefix project:(FlutterDartProject * _Nullable)project allowHeadlessExecution:(BOOL)allowHeadlessExecution;
        [Export ("initWithName:project:allowHeadlessExecution:")]
        IntPtr Constructor(string labelPrefix, [NullAllowed] FlutterDartProject project, bool allowHeadlessExecution);

        // -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)labelPrefix project:(FlutterDartProject * _Nullable)project allowHeadlessExecution:(BOOL)allowHeadlessExecution restorationEnabled:(BOOL)restorationEnabled __attribute__((objc_designated_initializer));
        [Export ("initWithName:project:allowHeadlessExecution:restorationEnabled:")]
        [DesignatedInitializer]
        IntPtr Constructor(string labelPrefix, [NullAllowed] FlutterDartProject project, bool allowHeadlessExecution, bool restorationEnabled);

        // -(BOOL)run;
        [Export("run")]
        bool Run();

        // -(BOOL)runWithEntrypoint:(NSString * _Nullable)entrypoint;
        [Export("runWithEntrypoint:")]
        bool Run([NullAllowed] string entrypoint);

        // -(BOOL)runWithEntrypoint:(NSString * _Nullable)entrypoint initialRoute:(NSString * _Nullable)initialRoute;
        [Export ("runWithEntrypoint:initialRoute:")]
        bool Run([NullAllowed] string entrypoint, [NullAllowed] string initialRoute);

        // -(BOOL)runWithEntrypoint:(NSString * _Nullable)entrypoint libraryURI:(NSString * _Nullable)uri;
        [Export("runWithEntrypoint:libraryURI:")]
        bool RunWithUri([NullAllowed] string entrypoint, [NullAllowed] string uri);

        // -(void)destroyContext;
        [Export("destroyContext")]
        void DestroyContext();

        // -(void)ensureSemanticsEnabled;
        [Export("ensureSemanticsEnabled")]
        void EnsureSemanticsEnabled();

        // @property (nonatomic, weak) FlutterViewController * _Nullable viewController;
        [NullAllowed, Export("viewController", ArgumentSemantic.Weak)]
        FlutterViewController ViewController { get; set; }

        // @property (readonly, nonatomic) FlutterMethodChannel * _Nullable localizationChannel;
        [NullAllowed, Export("localizationChannel")]
        FlutterMethodChannel LocalizationChannel { get; }

        // @property (readonly, nonatomic) FlutterMethodChannel * _Nonnull navigationChannel;
        [Export("navigationChannel")]
        FlutterMethodChannel NavigationChannel { get; }

        // @property (readonly, nonatomic) FlutterMethodChannel * _Nonnull restorationChannel;
        [Export ("restorationChannel")]
        FlutterMethodChannel RestorationChannel { get; }

        // @property (readonly, nonatomic) FlutterMethodChannel * _Nonnull platformChannel;
        [Export("platformChannel")]
        FlutterMethodChannel PlatformChannel { get; }

        // @property (readonly, nonatomic) FlutterMethodChannel * _Nonnull textInputChannel;
        [Export("textInputChannel")]
        FlutterMethodChannel TextInputChannel { get; }

        // @property (readonly, nonatomic) FlutterBasicMessageChannel * _Nonnull lifecycleChannel;
        [Export("lifecycleChannel")]
        FlutterBasicMessageChannel LifecycleChannel { get; }

        // @property (readonly, nonatomic) FlutterBasicMessageChannel * _Nonnull systemChannel;
        [Export("systemChannel")]
        FlutterBasicMessageChannel SystemChannel { get; }

        // @property (readonly, nonatomic) FlutterBasicMessageChannel * _Nonnull settingsChannel;
        [Export("settingsChannel")]
        FlutterBasicMessageChannel SettingsChannel { get; }

        // @property (readonly, nonatomic) FlutterBasicMessageChannel * _Nonnull keyEventChannel;
        [Export ("keyEventChannel")]
        FlutterBasicMessageChannel KeyEventChannel { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable observatoryUrl;
        [NullAllowed, Export("observatoryUrl")]
        NSUrl ObservatoryUrl { get; }

        // @property (readonly, nonatomic) NSObject<FlutterBinaryMessenger> * _Nonnull binaryMessenger;
        [Export("binaryMessenger")]
        IFlutterBinaryMessenger BinaryMessenger { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable isolateId;
        [NullAllowed, Export("isolateId")]
        string IsolateId { get; }

        // @property (assign, nonatomic) BOOL isGpuDisabled;
        [Export("isGpuDisabled")]
        bool IsGpuDisabled { get; set; }
    }

    // @interface FlutterEngineGroup : NSObject
    [BaseType (typeof(NSObject))]
    [DisableDefaultCtor]
    interface FlutterEngineGroup
    {
        // -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name project:(FlutterDartProject * _Nullable)project __attribute__((objc_designated_initializer));
        [Export ("initWithName:project:")]
        [DesignatedInitializer]
        IntPtr Constructor(string name, [NullAllowed] FlutterDartProject project);

        // -(FlutterEngine * _Nonnull)makeEngineWithEntrypoint:(NSString * _Nullable)entrypoint libraryURI:(NSString * _Nullable)libraryURI;
        [Export ("makeEngineWithEntrypoint:libraryURI:")]
        FlutterEngine MakeEngine([NullAllowed] string entrypoint, [NullAllowed] string libraryURI);
    }

    // typedef void (^FlutterHeadlessDartRunnerCallback)(BOOL);
    delegate void FlutterHeadlessDartRunnerCallback(bool callback);

    // @interface FlutterHeadlessDartRunner : FlutterEngine
    [BaseType(typeof(FlutterEngine))]
    interface FlutterHeadlessDartRunner
    {
        // -(instancetype)initWithName:(NSString *)labelPrefix project:(FlutterDartProject *)projectOrNil;
        [Export("initWithName:project:")]
        IntPtr Constructor(string labelPrefix, FlutterDartProject projectOrNil);

        // -(instancetype)initWithName:(NSString *)labelPrefix project:(FlutterDartProject *)projectOrNil allowHeadlessExecution:(BOOL)allowHeadlessExecution;
        [Export ("initWithName:project:allowHeadlessExecution:")]
        IntPtr Constructor(string labelPrefix, FlutterDartProject projectOrNil, bool allowHeadlessExecution);

        // -(instancetype)initWithName:(NSString *)labelPrefix project:(FlutterDartProject *)projectOrNil allowHeadlessExecution:(BOOL)allowHeadlessExecution restorationEnabled:(BOOL)restorationEnabled __attribute__((objc_designated_initializer));
        [Export ("initWithName:project:allowHeadlessExecution:restorationEnabled:")]
        [DesignatedInitializer]
        IntPtr Constructor(string labelPrefix, FlutterDartProject projectOrNil, bool allowHeadlessExecution, bool restorationEnabled);
    }

    // @interface FlutterPluginAppLifeCycleDelegate : NSObject <UNUserNotificationCenterDelegate>
    [BaseType(typeof(NSObject))]
    interface FlutterPluginAppLifeCycleDelegate : IUNUserNotificationCenterDelegate
    {
        // -(void)addDelegate:(NSObject<FlutterApplicationLifeCycleDelegate> * _Nonnull)delegate;
        [Export("addDelegate:")]
        //void AddDelegate(NSObject @delegate);
        void AddDelegate(IFlutterApplicationLifeCycleDelegate @delegate);

        // -(BOOL)application:(UIApplication * _Nonnull)application didFinishLaunchingWithOptions:(NSDictionary * _Nonnull)launchOptions;
        [Export("application:didFinishLaunchingWithOptions:")]
        bool ApplicationFinishedLaunching(UIApplication application, [NullAllowed] NSDictionary launchOptions);

        // -(BOOL)application:(UIApplication * _Nonnull)application willFinishLaunchingWithOptions:(NSDictionary * _Nonnull)launchOptions;
        [Export("application:willFinishLaunchingWithOptions:")]
        bool ApplicationWillFinishLaunching(UIApplication application, [NullAllowed] NSDictionary launchOptions);

        // -(void)application:(UIApplication * _Nonnull)application didRegisterUserNotificationSettings:(UIUserNotificationSettings * _Nonnull)notificationSettings __attribute__((availability(ios, introduced=8.0, deprecated=10.0)));
        [Introduced(PlatformName.iOS, 8, 0)]
        [Deprecated(PlatformName.iOS, 10, 0, message: "See UIApplicationDelegate.DidRegisterUserNotificationSettings deprecation.")]
        [Unavailable(PlatformName.TvOS)]
        [Export("application:didRegisterUserNotificationSettings:")]
        void ApplicationDidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings);

        // -(void)application:(UIApplication * _Nonnull)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData * _Nonnull)deviceToken;
        [Export("application:didRegisterForRemoteNotificationsWithDeviceToken:")]
        void ApplicationRegisteredForRemoteNotifications(UIApplication application, NSData deviceToken);

        // -(void)application:(UIApplication * _Nonnull)application didReceiveRemoteNotification:(NSDictionary * _Nonnull)userInfo fetchCompletionHandler:(void (^ _Nonnull)(UIBackgroundFetchResult))completionHandler;
        [Deprecated(PlatformName.iOS, 10, 0, message: "See UIApplicationDelegate.ReceivedRemoteNotification deprecation.")]
        [Export("application:didReceiveRemoteNotification:fetchCompletionHandler:")]
        void ApplicationReceivedRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler);

        // -(void)application:(UIApplication * _Nonnull)application didReceiveLocalNotification:(UILocalNotification * _Nonnull)notification __attribute__((availability(ios, introduced=4.0, deprecated=10.0)));
        [Introduced(PlatformName.iOS, 4, 0)]
        [Deprecated(PlatformName.iOS, 10, 0, message: "See UIApplicationDelegate.ReceivedLocalNotification deprecation.")]
        [Unavailable(PlatformName.TvOS)]
        [Export("application:didReceiveLocalNotification:")]
        void ApplicationReceivedLocalNotification(UIApplication application, UILocalNotification notification);

        // -(BOOL)application:(UIApplication * _Nonnull)application openURL:(NSURL * _Nonnull)url options:(NSDictionary<UIApplicationOpenURLOptionsKey,id> * _Nonnull)options;
        [Introduced(PlatformName.iOS, 9, 0)]
        [Export("application:openURL:options:")]
        bool ApplicationOpenUrl(UIApplication application, NSUrl url, NSDictionary options);

        // -(BOOL)application:(UIApplication * _Nonnull)application handleOpenURL:(NSURL * _Nonnull)url;
        [Obsoleted(PlatformName.iOS, 9, 0, message: "See UIApplicationDelegate.HandleOpenURL deprecation.")]
        [Unavailable(PlatformName.TvOS)]
        [Export("application:handleOpenURL:")]
        bool ApplicationHandleOpenURL(UIApplication application, NSUrl url);

        // -(BOOL)application:(UIApplication * _Nonnull)application openURL:(NSURL * _Nonnull)url sourceApplication:(NSString * _Nonnull)sourceApplication annotation:(id _Nonnull)annotation;
        [Obsoleted(PlatformName.iOS, 9, 0, message: "See UIApplicationDelegate.OpenUrl(UIApplication, NSUrl, string, NSObject) deprecation.")]
        [Unavailable(PlatformName.TvOS)]
        [Export("application:openURL:sourceApplication:annotation:")]
        bool ApplicationOpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation);

        // -(void)application:(UIApplication * _Nonnull)application performActionForShortcutItem:(UIApplicationShortcutItem * _Nonnull)shortcutItem completionHandler:(void (^ _Nonnull)(BOOL))completionHandler __attribute__((availability(ios, introduced=9.0)));
        [Introduced(PlatformName.iOS, 9, 0)]
        [Unavailable(PlatformName.TvOS)]
        [Export("application:performActionForShortcutItem:completionHandler:")]
        void ApplicationPerformActionForShortcutItem(UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler);

        // -(BOOL)application:(UIApplication * _Nonnull)application handleEventsForBackgroundURLSession:(NSString * _Nonnull)identifier completionHandler:(void (^ _Nonnull)(void))completionHandler;
        [Introduced(PlatformName.iOS, 7, 0)]
        [Export("application:handleEventsForBackgroundURLSession:completionHandler:")]
        bool ApplicationHandleEventsForBackgroundUrl(UIApplication application, string identifier, Action completionHandler);

        // -(BOOL)application:(UIApplication * _Nonnull)application performFetchWithCompletionHandler:(void (^ _Nonnull)(UIBackgroundFetchResult))completionHandler;
        [Introduced(PlatformName.iOS, 7, 0)]
        [Introduced(PlatformName.TvOS, 11, 0)]
        [Export("application:performFetchWithCompletionHandler:")]
        bool ApplicationPerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler);

        // -(BOOL)application:(UIApplication * _Nonnull)application continueUserActivity:(NSUserActivity * _Nonnull)userActivity restorationHandler:(void (^ _Nonnull)(NSArray * _Nonnull))restorationHandler;
        [Introduced(PlatformName.iOS, 8, 0)]
        [Export("application:continueUserActivity:restorationHandler:")]
        bool ApplicationContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler restorationHandler);
    }

    // @interface FlutterViewController : UIViewController <FlutterTextureRegistry, FlutterPluginRegistry>
    [BaseType(typeof(UIViewController))]
    interface FlutterViewController : IFlutterTextureRegistry, IFlutterPluginRegistry
    {
        // -(instancetype _Nonnull)initWithEngine:(FlutterEngine * _Nonnull)engine nibName:(NSString * _Nullable)nibName bundle:(NSBundle * _Nullable)nibBundle __attribute__((objc_designated_initializer));
        [Export("initWithEngine:nibName:bundle:")]
        [DesignatedInitializer]
        IntPtr Constructor(FlutterEngine engine, [NullAllowed] string nibName, [NullAllowed] NSBundle nibBundle);

        // -(instancetype _Nonnull)initWithProject:(FlutterDartProject * _Nullable)project nibName:(NSString * _Nullable)nibName bundle:(NSBundle * _Nullable)nibBundle __attribute__((objc_designated_initializer));
        [Export("initWithProject:nibName:bundle:")]
        [DesignatedInitializer]
        IntPtr Constructor([NullAllowed] FlutterDartProject project, [NullAllowed] string nibName, [NullAllowed] NSBundle nibBundle);

        // -(instancetype _Nonnull)initWithProject:(FlutterDartProject * _Nullable)project initialRoute:(NSString * _Nullable)initialRoute nibName:(NSString * _Nullable)nibName bundle:(NSBundle * _Nullable)nibBundle __attribute__((objc_designated_initializer));
        [Export("initWithProject:initialRoute:nibName:bundle:")]
        [DesignatedInitializer]
        IntPtr Constructor([NullAllowed] FlutterDartProject project, [NullAllowed] string initialRoute, [NullAllowed] string nibName, [NullAllowed] NSBundle nibBundle);

        /*
        // -(instancetype _Nonnull)initWithCoder:(NSCoder * _Nonnull)aDecoder __attribute__((objc_designated_initializer));
        [Export("initWithCoder:")]
        [DesignatedInitializer]
        IntPtr Constructor(NSCoder aDecoder);
         */

        // -(void)setFlutterViewDidRenderCallback:(void (^ _Nonnull)(void))callback;
        [Export("setFlutterViewDidRenderCallback:")]
        void SetFlutterViewDidRenderCallback(Action callback);

        // -(NSString * _Nonnull)lookupKeyForAsset:(NSString * _Nonnull)asset;
        [Export("lookupKeyForAsset:")]
        string LookupKeyForAsset(string asset);

        // -(NSString * _Nonnull)lookupKeyForAsset:(NSString * _Nonnull)asset fromPackage:(NSString * _Nonnull)package;
        [Export("lookupKeyForAsset:fromPackage:")]
        string LookupKeyForAsset(string asset, string package);

        // -(void)setInitialRoute:(NSString * _Nonnull)route;
        [Export("setInitialRoute:")]
        void SetInitialRoute(string route);

        // -(void)popRoute;
        [Export("popRoute")]
        void PopRoute();

        // -(void)pushRoute:(NSString * _Nonnull)route;
        [Export("pushRoute:")]
        void PushRoute(string route);

        // -(id<FlutterPluginRegistry> _Nonnull)pluginRegistry;
        [Export("pluginRegistry")]
        IFlutterPluginRegistry PluginRegistry { get; }

        // @property (readonly, getter = isDisplayingFlutterUI, nonatomic) BOOL displayingFlutterUI;
        [Export("displayingFlutterUI")]
        bool DisplayingFlutterUI { [Bind("isDisplayingFlutterUI")] get; }

        // @property (nonatomic, strong) UIView * _Nonnull splashScreenView;
        [Export("splashScreenView", ArgumentSemantic.Strong)]
        UIView SplashScreenView { get; set; }

        // -(BOOL)loadDefaultSplashScreenView;
        [Export("loadDefaultSplashScreenView")]
        bool LoadDefaultSplashScreenView();

        // @property (getter = isViewOpaque, nonatomic) BOOL viewOpaque;
        [Export("viewOpaque")]
        bool ViewOpaque { [Bind("isViewOpaque")] get; set; }

        // @property (readonly, nonatomic, weak) FlutterEngine * _Nullable engine;
        [NullAllowed, Export("engine", ArgumentSemantic.Weak)]
        FlutterEngine Engine { get; }

        // @property (readonly, nonatomic) NSObject<FlutterBinaryMessenger> * _Nonnull binaryMessenger;
        [Export("binaryMessenger")]
        IFlutterBinaryMessenger BinaryMessenger { get; }

        // @property (readonly, nonatomic) BOOL engineAllowHeadlessExecution;
        [Export("engineAllowHeadlessExecution")]
        bool EngineAllowHeadlessExecution { get; }
    }
}