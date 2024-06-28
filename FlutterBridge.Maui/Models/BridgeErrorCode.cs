using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Models
{
    [ProtoContract]
    public enum BridgeErrorCode
    {
        [ProtoEnum]
        OperationNotImplemented = 0,

        [ProtoEnum]
        OperationArgumentsCountMismatch = 1,

        [ProtoEnum]
        OperationArgumentsInvalid = 2,

        [ProtoEnum]
        OperationArgumentsParsingError = 3,

        [ProtoEnum]
        OperationFailed = 4,

        [ProtoEnum]
        OperationCanceled = 5,

        [ProtoEnum]
        EnvironmentNotInitialized = 6,
    }
}
