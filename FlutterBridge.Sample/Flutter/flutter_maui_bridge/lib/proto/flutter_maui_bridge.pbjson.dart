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
    {'1': 'instanceId', '3': 1, '4': 1, '5': 9, '10': 'instanceId'},
    {'1': 'event', '3': 2, '4': 1, '5': 9, '10': 'event'},
    {'1': 'args', '3': 3, '4': 1, '5': 12, '10': 'args'},
  ],
};

/// Descriptor for `BridgeEventInfo`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List bridgeEventInfoDescriptor = $convert.base64Decode(
    'Cg9CcmlkZ2VFdmVudEluZm8SHgoKaW5zdGFuY2VJZBgBIAEoCVIKaW5zdGFuY2VJZBIUCgVldm'
    'VudBgCIAEoCVIFZXZlbnQSEgoEYXJncxgDIAEoDFIEYXJncw==');

@$core.Deprecated('Use bridgeMessageInfoDescriptor instead')
const BridgeMessageInfo$json = {
  '1': 'BridgeMessageInfo',
  '2': [
    {'1': 'methodInfo', '3': 1, '4': 1, '5': 11, '6': '.BridgeMethodInfo', '10': 'methodInfo'},
    {'1': 'arguments', '3': 2, '4': 3, '5': 11, '6': '.BridgeMessageInfo.ArgumentsEntry', '10': 'arguments'},
    {'1': 'result', '3': 3, '4': 1, '5': 12, '10': 'result'},
    {'1': 'event', '3': 4, '4': 1, '5': 11, '6': '.BridgeEventInfo', '10': 'event'},
    {'1': 'exception', '3': 5, '4': 1, '5': 11, '6': '.BridgeException', '10': 'exception'},
    {'1': 'errorCode', '3': 6, '4': 1, '5': 14, '6': '.BridgeErrorCode', '10': 'errorCode'},
    {'1': 'errorMessage', '3': 7, '4': 1, '5': 9, '10': 'errorMessage'},
  ],
  '3': [BridgeMessageInfo_ArgumentsEntry$json],
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
    'ChFCcmlkZ2VNZXNzYWdlSW5mbxIxCgptZXRob2RJbmZvGAEgASgLMhEuQnJpZGdlTWV0aG9kSW'
    '5mb1IKbWV0aG9kSW5mbxI/Cglhcmd1bWVudHMYAiADKAsyIS5CcmlkZ2VNZXNzYWdlSW5mby5B'
    'cmd1bWVudHNFbnRyeVIJYXJndW1lbnRzEhYKBnJlc3VsdBgDIAEoDFIGcmVzdWx0EiYKBWV2ZW'
    '50GAQgASgLMhAuQnJpZGdlRXZlbnRJbmZvUgVldmVudBIuCglleGNlcHRpb24YBSABKAsyEC5C'
    'cmlkZ2VFeGNlcHRpb25SCWV4Y2VwdGlvbhIuCgllcnJvckNvZGUYBiABKA4yEC5CcmlkZ2VFcn'
    'JvckNvZGVSCWVycm9yQ29kZRIiCgxlcnJvck1lc3NhZ2UYByABKAlSDGVycm9yTWVzc2FnZRo8'
    'Cg5Bcmd1bWVudHNFbnRyeRIQCgNrZXkYASABKAlSA2tleRIUCgV2YWx1ZRgCIAEoDFIFdmFsdW'
    'U6AjgB');

@$core.Deprecated('Use bridgeMethodInfoDescriptor instead')
const BridgeMethodInfo$json = {
  '1': 'BridgeMethodInfo',
  '2': [
    {'1': 'requestId', '3': 1, '4': 1, '5': 5, '10': 'requestId'},
    {'1': 'instance', '3': 2, '4': 1, '5': 9, '10': 'instance'},
    {'1': 'service', '3': 3, '4': 1, '5': 9, '10': 'service'},
    {'1': 'operation', '3': 4, '4': 1, '5': 9, '10': 'operation'},
  ],
};

/// Descriptor for `BridgeMethodInfo`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List bridgeMethodInfoDescriptor = $convert.base64Decode(
    'ChBCcmlkZ2VNZXRob2RJbmZvEhwKCXJlcXVlc3RJZBgBIAEoBVIJcmVxdWVzdElkEhoKCGluc3'
    'RhbmNlGAIgASgJUghpbnN0YW5jZRIYCgdzZXJ2aWNlGAMgASgJUgdzZXJ2aWNlEhwKCW9wZXJh'
    'dGlvbhgEIAEoCVIJb3BlcmF0aW9u');

