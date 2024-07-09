//
//  Generated code. Do not modify.
//  source: proto/flutter_maui_bridge.proto
//
// @dart = 2.12

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names, library_prefixes
// ignore_for_file: non_constant_identifier_names, prefer_final_fields
// ignore_for_file: unnecessary_import, unnecessary_this, unused_import

import 'dart:convert' as $convert;
import 'dart:core' as $core;
import 'dart:typed_data' as $typed_data;

@$core.Deprecated('Use bridgeModeDescriptor instead')
const BridgeMode$json = {
  '1': 'BridgeMode',
  '2': [
    {'1': 'PlatformChannel', '2': 0},
    {'1': 'WebSocket', '2': 1},
  ],
};

/// Descriptor for `BridgeMode`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List bridgeModeDescriptor = $convert.base64Decode(
    'CgpCcmlkZ2VNb2RlEhMKD1BsYXRmb3JtQ2hhbm5lbBAAEg0KCVdlYlNvY2tldBAB');

@$core.Deprecated('Use bridgeErrorCodeDescriptor instead')
const BridgeErrorCode$json = {
  '1': 'BridgeErrorCode',
  '2': [
    {'1': 'OperationNotImplemented', '2': 0},
    {'1': 'OperationArgumentsCountMismatch', '2': 1},
    {'1': 'OperationArgumentsInvalid', '2': 2},
    {'1': 'OperationArgumentsParsingError', '2': 3},
    {'1': 'OperationFailed', '2': 4},
    {'1': 'OperationCanceled', '2': 5},
    {'1': 'EnvironmentNotInitialized', '2': 6},
  ],
};

/// Descriptor for `BridgeErrorCode`. Decode as a `google.protobuf.EnumDescriptorProto`.
final $typed_data.Uint8List bridgeErrorCodeDescriptor = $convert.base64Decode(
    'Cg9CcmlkZ2VFcnJvckNvZGUSGwoXT3BlcmF0aW9uTm90SW1wbGVtZW50ZWQQABIjCh9PcGVyYX'
    'Rpb25Bcmd1bWVudHNDb3VudE1pc21hdGNoEAESHQoZT3BlcmF0aW9uQXJndW1lbnRzSW52YWxp'
    'ZBACEiIKHk9wZXJhdGlvbkFyZ3VtZW50c1BhcnNpbmdFcnJvchADEhMKD09wZXJhdGlvbkZhaW'
    'xlZBAEEhUKEU9wZXJhdGlvbkNhbmNlbGVkEAUSHQoZRW52aXJvbm1lbnROb3RJbml0aWFsaXpl'
    'ZBAG');

@$core.Deprecated('Use bridgeExceptionDescriptor instead')
const BridgeException$json = {
  '1': 'BridgeException',
  '2': [
    {'1': 'code', '3': 1, '4': 1, '5': 14, '6': '.BridgeErrorCode', '10': 'code'},
    {'1': 'message', '3': 2, '4': 1, '5': 9, '10': 'message'},
  ],
};

/// Descriptor for `BridgeException`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List bridgeExceptionDescriptor = $convert.base64Decode(
    'Cg9CcmlkZ2VFeGNlcHRpb24SJAoEY29kZRgBIAEoDjIQLkJyaWRnZUVycm9yQ29kZVIEY29kZR'
    'IYCgdtZXNzYWdlGAIgASgJUgdtZXNzYWdl');

@$core.Deprecated('Use bridgeEventInfoDescriptor instead')
const BridgeEventInfo$json = {
  '1': 'BridgeEventInfo',
  '2': [
    {'1': 'serviceName', '3': 1, '4': 1, '5': 9, '10': 'serviceName'},
    {'1': 'eventName', '3': 2, '4': 1, '5': 9, '10': 'eventName'},
    {'1': 'eventData', '3': 3, '4': 1, '5': 12, '10': 'eventData'},
  ],
};

/// Descriptor for `BridgeEventInfo`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List bridgeEventInfoDescriptor = $convert.base64Decode(
    'Cg9CcmlkZ2VFdmVudEluZm8SIAoLc2VydmljZU5hbWUYASABKAlSC3NlcnZpY2VOYW1lEhwKCW'
    'V2ZW50TmFtZRgCIAEoCVIJZXZlbnROYW1lEhwKCWV2ZW50RGF0YRgDIAEoDFIJZXZlbnREYXRh');

@$core.Deprecated('Use bridgeMessageInfoDescriptor instead')
const BridgeMessageInfo$json = {
  '1': 'BridgeMessageInfo',
  '2': [
    {'1': 'requestId', '3': 1, '4': 1, '5': 5, '10': 'requestId'},
    {'1': 'operationKey', '3': 2, '4': 1, '5': 9, '10': 'operationKey'},
    {'1': 'arguments', '3': 3, '4': 3, '5': 11, '6': '.BridgeMessageInfo.ArgumentsEntry', '10': 'arguments'},
    {'1': 'result', '3': 4, '4': 1, '5': 12, '9': 0, '10': 'result', '17': true},
    {'1': 'event', '3': 5, '4': 1, '5': 11, '6': '.BridgeEventInfo', '9': 1, '10': 'event', '17': true},
    {'1': 'exception', '3': 6, '4': 1, '5': 11, '6': '.BridgeException', '9': 2, '10': 'exception', '17': true},
    {'1': 'errorCode', '3': 7, '4': 1, '5': 14, '6': '.BridgeErrorCode', '9': 3, '10': 'errorCode', '17': true},
    {'1': 'errorMessage', '3': 8, '4': 1, '5': 9, '10': 'errorMessage'},
  ],
  '3': [BridgeMessageInfo_ArgumentsEntry$json],
  '8': [
    {'1': '_result'},
    {'1': '_event'},
    {'1': '_exception'},
    {'1': '_errorCode'},
  ],
};

@$core.Deprecated('Use bridgeMessageInfoDescriptor instead')
const BridgeMessageInfo_ArgumentsEntry$json = {
  '1': 'ArgumentsEntry',
  '2': [
    {'1': 'key', '3': 1, '4': 1, '5': 9, '10': 'key'},
    {'1': 'value', '3': 2, '4': 1, '5': 12, '10': 'value'},
  ],
  '7': {'7': true},
};

/// Descriptor for `BridgeMessageInfo`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List bridgeMessageInfoDescriptor = $convert.base64Decode(
    'ChFCcmlkZ2VNZXNzYWdlSW5mbxIcCglyZXF1ZXN0SWQYASABKAVSCXJlcXVlc3RJZBIiCgxvcG'
    'VyYXRpb25LZXkYAiABKAlSDG9wZXJhdGlvbktleRI/Cglhcmd1bWVudHMYAyADKAsyIS5Ccmlk'
    'Z2VNZXNzYWdlSW5mby5Bcmd1bWVudHNFbnRyeVIJYXJndW1lbnRzEhsKBnJlc3VsdBgEIAEoDE'
    'gAUgZyZXN1bHSIAQESKwoFZXZlbnQYBSABKAsyEC5CcmlkZ2VFdmVudEluZm9IAVIFZXZlbnSI'
    'AQESMwoJZXhjZXB0aW9uGAYgASgLMhAuQnJpZGdlRXhjZXB0aW9uSAJSCWV4Y2VwdGlvbogBAR'
    'IzCgllcnJvckNvZGUYByABKA4yEC5CcmlkZ2VFcnJvckNvZGVIA1IJZXJyb3JDb2RliAEBEiIK'
    'DGVycm9yTWVzc2FnZRgIIAEoCVIMZXJyb3JNZXNzYWdlGjwKDkFyZ3VtZW50c0VudHJ5EhAKA2'
    'tleRgBIAEoCVIDa2V5EhQKBXZhbHVlGAIgASgMUgV2YWx1ZToCOAFCCQoHX3Jlc3VsdEIICgZf'
    'ZXZlbnRCDAoKX2V4Y2VwdGlvbkIMCgpfZXJyb3JDb2Rl');

@$core.Deprecated('Use boolValueDescriptor instead')
const BoolValue$json = {
  '1': 'BoolValue',
  '2': [
    {'1': 'value', '3': 1, '4': 1, '5': 8, '10': 'value'},
  ],
};

/// Descriptor for `BoolValue`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List boolValueDescriptor = $convert.base64Decode(
    'CglCb29sVmFsdWUSFAoFdmFsdWUYASABKAhSBXZhbHVl');

@$core.Deprecated('Use stringValueDescriptor instead')
const StringValue$json = {
  '1': 'StringValue',
  '2': [
    {'1': 'value', '3': 1, '4': 1, '5': 9, '10': 'value'},
  ],
};

/// Descriptor for `StringValue`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List stringValueDescriptor = $convert.base64Decode(
    'CgtTdHJpbmdWYWx1ZRIUCgV2YWx1ZRgBIAEoCVIFdmFsdWU=');

@$core.Deprecated('Use int32ValueDescriptor instead')
const Int32Value$json = {
  '1': 'Int32Value',
  '2': [
    {'1': 'value', '3': 1, '4': 1, '5': 5, '10': 'value'},
  ],
};

/// Descriptor for `Int32Value`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List int32ValueDescriptor = $convert.base64Decode(
    'CgpJbnQzMlZhbHVlEhQKBXZhbHVlGAEgASgFUgV2YWx1ZQ==');

@$core.Deprecated('Use int64ValueDescriptor instead')
const Int64Value$json = {
  '1': 'Int64Value',
  '2': [
    {'1': 'value', '3': 1, '4': 1, '5': 3, '10': 'value'},
  ],
};

/// Descriptor for `Int64Value`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List int64ValueDescriptor = $convert.base64Decode(
    'CgpJbnQ2NFZhbHVlEhQKBXZhbHVlGAEgASgDUgV2YWx1ZQ==');

@$core.Deprecated('Use doubleValueDescriptor instead')
const DoubleValue$json = {
  '1': 'DoubleValue',
  '2': [
    {'1': 'value', '3': 1, '4': 1, '5': 1, '10': 'value'},
  ],
};

/// Descriptor for `DoubleValue`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List doubleValueDescriptor = $convert.base64Decode(
    'CgtEb3VibGVWYWx1ZRIUCgV2YWx1ZRgBIAEoAVIFdmFsdWU=');

@$core.Deprecated('Use int32ListValueDescriptor instead')
const Int32ListValue$json = {
  '1': 'Int32ListValue',
  '2': [
    {'1': 'value', '3': 1, '4': 3, '5': 5, '10': 'value'},
  ],
};

/// Descriptor for `Int32ListValue`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List int32ListValueDescriptor = $convert.base64Decode(
    'Cg5JbnQzMkxpc3RWYWx1ZRIUCgV2YWx1ZRgBIAMoBVIFdmFsdWU=');

@$core.Deprecated('Use int64ListValueDescriptor instead')
const Int64ListValue$json = {
  '1': 'Int64ListValue',
  '2': [
    {'1': 'value', '3': 1, '4': 3, '5': 3, '10': 'value'},
  ],
};

/// Descriptor for `Int64ListValue`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List int64ListValueDescriptor = $convert.base64Decode(
    'Cg5JbnQ2NExpc3RWYWx1ZRIUCgV2YWx1ZRgBIAMoA1IFdmFsdWU=');

@$core.Deprecated('Use doubleListValueDescriptor instead')
const DoubleListValue$json = {
  '1': 'DoubleListValue',
  '2': [
    {'1': 'value', '3': 1, '4': 3, '5': 1, '10': 'value'},
  ],
};

/// Descriptor for `DoubleListValue`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List doubleListValueDescriptor = $convert.base64Decode(
    'Cg9Eb3VibGVMaXN0VmFsdWUSFAoFdmFsdWUYASADKAFSBXZhbHVl');

@$core.Deprecated('Use float64ListValueDescriptor instead')
const Float64ListValue$json = {
  '1': 'Float64ListValue',
  '2': [
    {'1': 'value', '3': 1, '4': 3, '5': 1, '10': 'value'},
  ],
};

/// Descriptor for `Float64ListValue`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List float64ListValueDescriptor = $convert.base64Decode(
    'ChBGbG9hdDY0TGlzdFZhbHVlEhQKBXZhbHVlGAEgAygBUgV2YWx1ZQ==');

