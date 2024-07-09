//
//  Generated code. Do not modify.
//  source: proto/flutter_maui_bridge.proto
//
// @dart = 2.12

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names, library_prefixes
// ignore_for_file: non_constant_identifier_names, prefer_final_fields
// ignore_for_file: unnecessary_import, unnecessary_this, unused_import

import 'dart:core' as $core;

import 'package:protobuf/protobuf.dart' as $pb;

class BridgeMode extends $pb.ProtobufEnum {
  static const BridgeMode PlatformChannel = BridgeMode._(0, _omitEnumNames ? '' : 'PlatformChannel');
  static const BridgeMode WebSocket = BridgeMode._(1, _omitEnumNames ? '' : 'WebSocket');

  static const $core.List<BridgeMode> values = <BridgeMode> [
    PlatformChannel,
    WebSocket,
  ];

  static final $core.Map<$core.int, BridgeMode> _byValue = $pb.ProtobufEnum.initByValue(values);
  static BridgeMode? valueOf($core.int value) => _byValue[value];

  const BridgeMode._($core.int v, $core.String n) : super(v, n);
}

class BridgeErrorCode extends $pb.ProtobufEnum {
  static const BridgeErrorCode OperationNotImplemented = BridgeErrorCode._(0, _omitEnumNames ? '' : 'OperationNotImplemented');
  static const BridgeErrorCode OperationArgumentsCountMismatch = BridgeErrorCode._(1, _omitEnumNames ? '' : 'OperationArgumentsCountMismatch');
  static const BridgeErrorCode OperationArgumentsInvalid = BridgeErrorCode._(2, _omitEnumNames ? '' : 'OperationArgumentsInvalid');
  static const BridgeErrorCode OperationArgumentsParsingError = BridgeErrorCode._(3, _omitEnumNames ? '' : 'OperationArgumentsParsingError');
  static const BridgeErrorCode OperationFailed = BridgeErrorCode._(4, _omitEnumNames ? '' : 'OperationFailed');
  static const BridgeErrorCode OperationCanceled = BridgeErrorCode._(5, _omitEnumNames ? '' : 'OperationCanceled');
  static const BridgeErrorCode EnvironmentNotInitialized = BridgeErrorCode._(6, _omitEnumNames ? '' : 'EnvironmentNotInitialized');

  static const $core.List<BridgeErrorCode> values = <BridgeErrorCode> [
    OperationNotImplemented,
    OperationArgumentsCountMismatch,
    OperationArgumentsInvalid,
    OperationArgumentsParsingError,
    OperationFailed,
    OperationCanceled,
    EnvironmentNotInitialized,
  ];

  static final $core.Map<$core.int, BridgeErrorCode> _byValue = $pb.ProtobufEnum.initByValue(values);
  static BridgeErrorCode? valueOf($core.int value) => _byValue[value];

  const BridgeErrorCode._($core.int v, $core.String n) : super(v, n);
}


const _omitEnumNames = $core.bool.fromEnvironment('protobuf.omit_enum_names');
