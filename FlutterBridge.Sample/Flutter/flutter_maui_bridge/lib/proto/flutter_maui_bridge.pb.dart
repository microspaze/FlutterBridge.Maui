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

import 'flutter_maui_bridge.pbenum.dart';

export 'flutter_maui_bridge.pbenum.dart';

class BridgeException extends $pb.GeneratedMessage {
  factory BridgeException({
    BridgeErrorCode? code,
    $core.String? message,
  }) {
    final $result = create();
    if (code != null) {
      $result.code = code;
    }
    if (message != null) {
      $result.message = message;
    }
    return $result;
  }
  BridgeException._() : super();
  factory BridgeException.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BridgeException.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'BridgeException', createEmptyInstance: create)
    ..e<BridgeErrorCode>(1, _omitFieldNames ? '' : 'code', $pb.PbFieldType.OE, defaultOrMaker: BridgeErrorCode.OperationNotImplemented, valueOf: BridgeErrorCode.valueOf, enumValues: BridgeErrorCode.values)
    ..aOS(2, _omitFieldNames ? '' : 'message')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BridgeException clone() => BridgeException()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BridgeException copyWith(void Function(BridgeException) updates) => super.copyWith((message) => updates(message as BridgeException)) as BridgeException;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static BridgeException create() => BridgeException._();
  BridgeException createEmptyInstance() => create();
  static $pb.PbList<BridgeException> createRepeated() => $pb.PbList<BridgeException>();
  @$core.pragma('dart2js:noInline')
  static BridgeException getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BridgeException>(create);
  static BridgeException? _defaultInstance;

  @$pb.TagNumber(1)
  BridgeErrorCode get code => $_getN(0);
  @$pb.TagNumber(1)
  set code(BridgeErrorCode v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasCode() => $_has(0);
  @$pb.TagNumber(1)
  void clearCode() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get message => $_getSZ(1);
  @$pb.TagNumber(2)
  set message($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasMessage() => $_has(1);
  @$pb.TagNumber(2)
  void clearMessage() => clearField(2);
}

class BridgeEventInfo extends $pb.GeneratedMessage {
  factory BridgeEventInfo({
    $core.String? instanceId,
    $core.String? event,
    $core.List<$core.int>? args,
  }) {
    final $result = create();
    if (instanceId != null) {
      $result.instanceId = instanceId;
    }
    if (event != null) {
      $result.event = event;
    }
    if (args != null) {
      $result.args = args;
    }
    return $result;
  }
  BridgeEventInfo._() : super();
  factory BridgeEventInfo.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BridgeEventInfo.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'BridgeEventInfo', createEmptyInstance: create)
    ..aOS(1, _omitFieldNames ? '' : 'instanceId', protoName: 'instanceId')
    ..aOS(2, _omitFieldNames ? '' : 'event')
    ..a<$core.List<$core.int>>(3, _omitFieldNames ? '' : 'args', $pb.PbFieldType.OY)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BridgeEventInfo clone() => BridgeEventInfo()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BridgeEventInfo copyWith(void Function(BridgeEventInfo) updates) => super.copyWith((message) => updates(message as BridgeEventInfo)) as BridgeEventInfo;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static BridgeEventInfo create() => BridgeEventInfo._();
  BridgeEventInfo createEmptyInstance() => create();
  static $pb.PbList<BridgeEventInfo> createRepeated() => $pb.PbList<BridgeEventInfo>();
  @$core.pragma('dart2js:noInline')
  static BridgeEventInfo getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BridgeEventInfo>(create);
  static BridgeEventInfo? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get instanceId => $_getSZ(0);
  @$pb.TagNumber(1)
  set instanceId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasInstanceId() => $_has(0);
  @$pb.TagNumber(1)
  void clearInstanceId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get event => $_getSZ(1);
  @$pb.TagNumber(2)
  set event($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasEvent() => $_has(1);
  @$pb.TagNumber(2)
  void clearEvent() => clearField(2);

  @$pb.TagNumber(3)
  $core.List<$core.int> get args => $_getN(2);
  @$pb.TagNumber(3)
  set args($core.List<$core.int> v) { $_setBytes(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasArgs() => $_has(2);
  @$pb.TagNumber(3)
  void clearArgs() => clearField(3);
}

class BridgeMessageInfo extends $pb.GeneratedMessage {
  factory BridgeMessageInfo({
    BridgeMethodInfo? methodInfo,
    $core.Map<$core.String, $core.List<$core.int>>? arguments,
    $core.List<$core.int>? result,
    BridgeEventInfo? event,
    BridgeException? exception,
    BridgeErrorCode? errorCode,
    $core.String? errorMessage,
  }) {
    final $result = create();
    if (methodInfo != null) {
      $result.methodInfo = methodInfo;
    }
    if (arguments != null) {
      $result.arguments.addAll(arguments);
    }
    if (result != null) {
      $result.result = result;
    }
    if (event != null) {
      $result.event = event;
    }
    if (exception != null) {
      $result.exception = exception;
    }
    if (errorCode != null) {
      $result.errorCode = errorCode;
    }
    if (errorMessage != null) {
      $result.errorMessage = errorMessage;
    }
    return $result;
  }
  BridgeMessageInfo._() : super();
  factory BridgeMessageInfo.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BridgeMessageInfo.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'BridgeMessageInfo', createEmptyInstance: create)
    ..aOM<BridgeMethodInfo>(1, _omitFieldNames ? '' : 'methodInfo', protoName: 'methodInfo', subBuilder: BridgeMethodInfo.create)
    ..m<$core.String, $core.List<$core.int>>(2, _omitFieldNames ? '' : 'arguments', entryClassName: 'BridgeMessageInfo.ArgumentsEntry', keyFieldType: $pb.PbFieldType.OS, valueFieldType: $pb.PbFieldType.OY)
    ..a<$core.List<$core.int>>(3, _omitFieldNames ? '' : 'result', $pb.PbFieldType.OY)
    ..aOM<BridgeEventInfo>(4, _omitFieldNames ? '' : 'event', subBuilder: BridgeEventInfo.create)
    ..aOM<BridgeException>(5, _omitFieldNames ? '' : 'exception', subBuilder: BridgeException.create)
    ..e<BridgeErrorCode>(6, _omitFieldNames ? '' : 'errorCode', $pb.PbFieldType.OE, protoName: 'errorCode', defaultOrMaker: BridgeErrorCode.OperationNotImplemented, valueOf: BridgeErrorCode.valueOf, enumValues: BridgeErrorCode.values)
    ..aOS(7, _omitFieldNames ? '' : 'errorMessage', protoName: 'errorMessage')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BridgeMessageInfo clone() => BridgeMessageInfo()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BridgeMessageInfo copyWith(void Function(BridgeMessageInfo) updates) => super.copyWith((message) => updates(message as BridgeMessageInfo)) as BridgeMessageInfo;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static BridgeMessageInfo create() => BridgeMessageInfo._();
  BridgeMessageInfo createEmptyInstance() => create();
  static $pb.PbList<BridgeMessageInfo> createRepeated() => $pb.PbList<BridgeMessageInfo>();
  @$core.pragma('dart2js:noInline')
  static BridgeMessageInfo getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BridgeMessageInfo>(create);
  static BridgeMessageInfo? _defaultInstance;

  @$pb.TagNumber(1)
  BridgeMethodInfo get methodInfo => $_getN(0);
  @$pb.TagNumber(1)
  set methodInfo(BridgeMethodInfo v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasMethodInfo() => $_has(0);
  @$pb.TagNumber(1)
  void clearMethodInfo() => clearField(1);
  @$pb.TagNumber(1)
  BridgeMethodInfo ensureMethodInfo() => $_ensure(0);

  @$pb.TagNumber(2)
  $core.Map<$core.String, $core.List<$core.int>> get arguments => $_getMap(1);

  @$pb.TagNumber(3)
  $core.List<$core.int> get result => $_getN(2);
  @$pb.TagNumber(3)
  set result($core.List<$core.int> v) { $_setBytes(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasResult() => $_has(2);
  @$pb.TagNumber(3)
  void clearResult() => clearField(3);

  @$pb.TagNumber(4)
  BridgeEventInfo get event => $_getN(3);
  @$pb.TagNumber(4)
  set event(BridgeEventInfo v) { setField(4, v); }
  @$pb.TagNumber(4)
  $core.bool hasEvent() => $_has(3);
  @$pb.TagNumber(4)
  void clearEvent() => clearField(4);
  @$pb.TagNumber(4)
  BridgeEventInfo ensureEvent() => $_ensure(3);

  @$pb.TagNumber(5)
  BridgeException get exception => $_getN(4);
  @$pb.TagNumber(5)
  set exception(BridgeException v) { setField(5, v); }
  @$pb.TagNumber(5)
  $core.bool hasException() => $_has(4);
  @$pb.TagNumber(5)
  void clearException() => clearField(5);
  @$pb.TagNumber(5)
  BridgeException ensureException() => $_ensure(4);

  @$pb.TagNumber(6)
  BridgeErrorCode get errorCode => $_getN(5);
  @$pb.TagNumber(6)
  set errorCode(BridgeErrorCode v) { setField(6, v); }
  @$pb.TagNumber(6)
  $core.bool hasErrorCode() => $_has(5);
  @$pb.TagNumber(6)
  void clearErrorCode() => clearField(6);

  @$pb.TagNumber(7)
  $core.String get errorMessage => $_getSZ(6);
  @$pb.TagNumber(7)
  set errorMessage($core.String v) { $_setString(6, v); }
  @$pb.TagNumber(7)
  $core.bool hasErrorMessage() => $_has(6);
  @$pb.TagNumber(7)
  void clearErrorMessage() => clearField(7);
}

class BridgeMethodInfo extends $pb.GeneratedMessage {
  factory BridgeMethodInfo({
    $core.int? requestId,
    $core.String? instance,
    $core.String? service,
    $core.String? operation,
  }) {
    final $result = create();
    if (requestId != null) {
      $result.requestId = requestId;
    }
    if (instance != null) {
      $result.instance = instance;
    }
    if (service != null) {
      $result.service = service;
    }
    if (operation != null) {
      $result.operation = operation;
    }
    return $result;
  }
  BridgeMethodInfo._() : super();
  factory BridgeMethodInfo.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BridgeMethodInfo.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'BridgeMethodInfo', createEmptyInstance: create)
    ..a<$core.int>(1, _omitFieldNames ? '' : 'requestId', $pb.PbFieldType.O3, protoName: 'requestId')
    ..aOS(2, _omitFieldNames ? '' : 'instance')
    ..aOS(3, _omitFieldNames ? '' : 'service')
    ..aOS(4, _omitFieldNames ? '' : 'operation')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BridgeMethodInfo clone() => BridgeMethodInfo()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BridgeMethodInfo copyWith(void Function(BridgeMethodInfo) updates) => super.copyWith((message) => updates(message as BridgeMethodInfo)) as BridgeMethodInfo;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static BridgeMethodInfo create() => BridgeMethodInfo._();
  BridgeMethodInfo createEmptyInstance() => create();
  static $pb.PbList<BridgeMethodInfo> createRepeated() => $pb.PbList<BridgeMethodInfo>();
  @$core.pragma('dart2js:noInline')
  static BridgeMethodInfo getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BridgeMethodInfo>(create);
  static BridgeMethodInfo? _defaultInstance;

  @$pb.TagNumber(1)
  $core.int get requestId => $_getIZ(0);
  @$pb.TagNumber(1)
  set requestId($core.int v) { $_setSignedInt32(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasRequestId() => $_has(0);
  @$pb.TagNumber(1)
  void clearRequestId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get instance => $_getSZ(1);
  @$pb.TagNumber(2)
  set instance($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasInstance() => $_has(1);
  @$pb.TagNumber(2)
  void clearInstance() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get service => $_getSZ(2);
  @$pb.TagNumber(3)
  set service($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasService() => $_has(2);
  @$pb.TagNumber(3)
  void clearService() => clearField(3);

  @$pb.TagNumber(4)
  $core.String get operation => $_getSZ(3);
  @$pb.TagNumber(4)
  set operation($core.String v) { $_setString(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasOperation() => $_has(3);
  @$pb.TagNumber(4)
  void clearOperation() => clearField(4);
}


const _omitFieldNames = $core.bool.fromEnvironment('protobuf.omit_field_names');
const _omitMessageNames = $core.bool.fromEnvironment('protobuf.omit_message_names');