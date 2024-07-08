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

import 'package:fixnum/fixnum.dart' as $fixnum;
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
    $core.String? serviceName,
    $core.String? eventName,
    $core.List<$core.int>? eventData,
  }) {
    final $result = create();
    if (serviceName != null) {
      $result.serviceName = serviceName;
    }
    if (eventName != null) {
      $result.eventName = eventName;
    }
    if (eventData != null) {
      $result.eventData = eventData;
    }
    return $result;
  }
  BridgeEventInfo._() : super();
  factory BridgeEventInfo.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BridgeEventInfo.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'BridgeEventInfo', createEmptyInstance: create)
    ..aOS(1, _omitFieldNames ? '' : 'serviceName', protoName: 'serviceName')
    ..aOS(2, _omitFieldNames ? '' : 'eventName', protoName: 'eventName')
    ..a<$core.List<$core.int>>(3, _omitFieldNames ? '' : 'eventData', $pb.PbFieldType.OY, protoName: 'eventData')
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
  $core.String get serviceName => $_getSZ(0);
  @$pb.TagNumber(1)
  set serviceName($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasServiceName() => $_has(0);
  @$pb.TagNumber(1)
  void clearServiceName() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get eventName => $_getSZ(1);
  @$pb.TagNumber(2)
  set eventName($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasEventName() => $_has(1);
  @$pb.TagNumber(2)
  void clearEventName() => clearField(2);

  @$pb.TagNumber(3)
  $core.List<$core.int> get eventData => $_getN(2);
  @$pb.TagNumber(3)
  set eventData($core.List<$core.int> v) { $_setBytes(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasEventData() => $_has(2);
  @$pb.TagNumber(3)
  void clearEventData() => clearField(3);
}

class BridgeMessageInfo extends $pb.GeneratedMessage {
  factory BridgeMessageInfo({
    $core.int? requestId,
    $core.String? operationKey,
    $core.Map<$core.String, $core.List<$core.int>>? arguments,
    $core.List<$core.int>? result,
    BridgeEventInfo? event,
    BridgeException? exception,
    BridgeErrorCode? errorCode,
    $core.String? errorMessage,
  }) {
    final $result = create();
    if (requestId != null) {
      $result.requestId = requestId;
    }
    if (operationKey != null) {
      $result.operationKey = operationKey;
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
    ..a<$core.int>(1, _omitFieldNames ? '' : 'requestId', $pb.PbFieldType.O3, protoName: 'requestId')
    ..aOS(2, _omitFieldNames ? '' : 'operationKey', protoName: 'operationKey')
    ..m<$core.String, $core.List<$core.int>>(3, _omitFieldNames ? '' : 'arguments', entryClassName: 'BridgeMessageInfo.ArgumentsEntry', keyFieldType: $pb.PbFieldType.OS, valueFieldType: $pb.PbFieldType.OY)
    ..a<$core.List<$core.int>>(4, _omitFieldNames ? '' : 'result', $pb.PbFieldType.OY)
    ..aOM<BridgeEventInfo>(5, _omitFieldNames ? '' : 'event', subBuilder: BridgeEventInfo.create)
    ..aOM<BridgeException>(6, _omitFieldNames ? '' : 'exception', subBuilder: BridgeException.create)
    ..e<BridgeErrorCode>(7, _omitFieldNames ? '' : 'errorCode', $pb.PbFieldType.OE, protoName: 'errorCode', defaultOrMaker: BridgeErrorCode.OperationNotImplemented, valueOf: BridgeErrorCode.valueOf, enumValues: BridgeErrorCode.values)
    ..aOS(8, _omitFieldNames ? '' : 'errorMessage', protoName: 'errorMessage')
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
  $core.int get requestId => $_getIZ(0);
  @$pb.TagNumber(1)
  set requestId($core.int v) { $_setSignedInt32(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasRequestId() => $_has(0);
  @$pb.TagNumber(1)
  void clearRequestId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get operationKey => $_getSZ(1);
  @$pb.TagNumber(2)
  set operationKey($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasOperationKey() => $_has(1);
  @$pb.TagNumber(2)
  void clearOperationKey() => clearField(2);

  @$pb.TagNumber(3)
  $core.Map<$core.String, $core.List<$core.int>> get arguments => $_getMap(2);

  @$pb.TagNumber(4)
  $core.List<$core.int> get result => $_getN(3);
  @$pb.TagNumber(4)
  set result($core.List<$core.int> v) { $_setBytes(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasResult() => $_has(3);
  @$pb.TagNumber(4)
  void clearResult() => clearField(4);

  @$pb.TagNumber(5)
  BridgeEventInfo get event => $_getN(4);
  @$pb.TagNumber(5)
  set event(BridgeEventInfo v) { setField(5, v); }
  @$pb.TagNumber(5)
  $core.bool hasEvent() => $_has(4);
  @$pb.TagNumber(5)
  void clearEvent() => clearField(5);
  @$pb.TagNumber(5)
  BridgeEventInfo ensureEvent() => $_ensure(4);

  @$pb.TagNumber(6)
  BridgeException get exception => $_getN(5);
  @$pb.TagNumber(6)
  set exception(BridgeException v) { setField(6, v); }
  @$pb.TagNumber(6)
  $core.bool hasException() => $_has(5);
  @$pb.TagNumber(6)
  void clearException() => clearField(6);
  @$pb.TagNumber(6)
  BridgeException ensureException() => $_ensure(5);

  @$pb.TagNumber(7)
  BridgeErrorCode get errorCode => $_getN(6);
  @$pb.TagNumber(7)
  set errorCode(BridgeErrorCode v) { setField(7, v); }
  @$pb.TagNumber(7)
  $core.bool hasErrorCode() => $_has(6);
  @$pb.TagNumber(7)
  void clearErrorCode() => clearField(7);

  @$pb.TagNumber(8)
  $core.String get errorMessage => $_getSZ(7);
  @$pb.TagNumber(8)
  set errorMessage($core.String v) { $_setString(7, v); }
  @$pb.TagNumber(8)
  $core.bool hasErrorMessage() => $_has(7);
  @$pb.TagNumber(8)
  void clearErrorMessage() => clearField(8);
}

class BoolValue extends $pb.GeneratedMessage {
  factory BoolValue({
    $core.bool? value,
  }) {
    final $result = create();
    if (value != null) {
      $result.value = value;
    }
    return $result;
  }
  BoolValue._() : super();
  factory BoolValue.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BoolValue.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'BoolValue', createEmptyInstance: create)
    ..aOB(1, _omitFieldNames ? '' : 'value')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  BoolValue clone() => BoolValue()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  BoolValue copyWith(void Function(BoolValue) updates) => super.copyWith((message) => updates(message as BoolValue)) as BoolValue;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static BoolValue create() => BoolValue._();
  BoolValue createEmptyInstance() => create();
  static $pb.PbList<BoolValue> createRepeated() => $pb.PbList<BoolValue>();
  @$core.pragma('dart2js:noInline')
  static BoolValue getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BoolValue>(create);
  static BoolValue? _defaultInstance;

  @$pb.TagNumber(1)
  $core.bool get value => $_getBF(0);
  @$pb.TagNumber(1)
  set value($core.bool v) { $_setBool(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasValue() => $_has(0);
  @$pb.TagNumber(1)
  void clearValue() => clearField(1);
}

class StringValue extends $pb.GeneratedMessage {
  factory StringValue({
    $core.String? value,
  }) {
    final $result = create();
    if (value != null) {
      $result.value = value;
    }
    return $result;
  }
  StringValue._() : super();
  factory StringValue.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory StringValue.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'StringValue', createEmptyInstance: create)
    ..aOS(1, _omitFieldNames ? '' : 'value')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  StringValue clone() => StringValue()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  StringValue copyWith(void Function(StringValue) updates) => super.copyWith((message) => updates(message as StringValue)) as StringValue;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static StringValue create() => StringValue._();
  StringValue createEmptyInstance() => create();
  static $pb.PbList<StringValue> createRepeated() => $pb.PbList<StringValue>();
  @$core.pragma('dart2js:noInline')
  static StringValue getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<StringValue>(create);
  static StringValue? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get value => $_getSZ(0);
  @$pb.TagNumber(1)
  set value($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasValue() => $_has(0);
  @$pb.TagNumber(1)
  void clearValue() => clearField(1);
}

class Int32Value extends $pb.GeneratedMessage {
  factory Int32Value({
    $core.int? value,
  }) {
    final $result = create();
    if (value != null) {
      $result.value = value;
    }
    return $result;
  }
  Int32Value._() : super();
  factory Int32Value.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Int32Value.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Int32Value', createEmptyInstance: create)
    ..a<$core.int>(1, _omitFieldNames ? '' : 'value', $pb.PbFieldType.O3)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Int32Value clone() => Int32Value()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Int32Value copyWith(void Function(Int32Value) updates) => super.copyWith((message) => updates(message as Int32Value)) as Int32Value;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Int32Value create() => Int32Value._();
  Int32Value createEmptyInstance() => create();
  static $pb.PbList<Int32Value> createRepeated() => $pb.PbList<Int32Value>();
  @$core.pragma('dart2js:noInline')
  static Int32Value getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Int32Value>(create);
  static Int32Value? _defaultInstance;

  @$pb.TagNumber(1)
  $core.int get value => $_getIZ(0);
  @$pb.TagNumber(1)
  set value($core.int v) { $_setSignedInt32(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasValue() => $_has(0);
  @$pb.TagNumber(1)
  void clearValue() => clearField(1);
}

class Int64Value extends $pb.GeneratedMessage {
  factory Int64Value({
    $fixnum.Int64? value,
  }) {
    final $result = create();
    if (value != null) {
      $result.value = value;
    }
    return $result;
  }
  Int64Value._() : super();
  factory Int64Value.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Int64Value.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Int64Value', createEmptyInstance: create)
    ..aInt64(1, _omitFieldNames ? '' : 'value')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Int64Value clone() => Int64Value()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Int64Value copyWith(void Function(Int64Value) updates) => super.copyWith((message) => updates(message as Int64Value)) as Int64Value;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Int64Value create() => Int64Value._();
  Int64Value createEmptyInstance() => create();
  static $pb.PbList<Int64Value> createRepeated() => $pb.PbList<Int64Value>();
  @$core.pragma('dart2js:noInline')
  static Int64Value getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Int64Value>(create);
  static Int64Value? _defaultInstance;

  @$pb.TagNumber(1)
  $fixnum.Int64 get value => $_getI64(0);
  @$pb.TagNumber(1)
  set value($fixnum.Int64 v) { $_setInt64(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasValue() => $_has(0);
  @$pb.TagNumber(1)
  void clearValue() => clearField(1);
}

class DoubleValue extends $pb.GeneratedMessage {
  factory DoubleValue({
    $core.double? value,
  }) {
    final $result = create();
    if (value != null) {
      $result.value = value;
    }
    return $result;
  }
  DoubleValue._() : super();
  factory DoubleValue.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory DoubleValue.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'DoubleValue', createEmptyInstance: create)
    ..a<$core.double>(1, _omitFieldNames ? '' : 'value', $pb.PbFieldType.OD)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  DoubleValue clone() => DoubleValue()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  DoubleValue copyWith(void Function(DoubleValue) updates) => super.copyWith((message) => updates(message as DoubleValue)) as DoubleValue;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static DoubleValue create() => DoubleValue._();
  DoubleValue createEmptyInstance() => create();
  static $pb.PbList<DoubleValue> createRepeated() => $pb.PbList<DoubleValue>();
  @$core.pragma('dart2js:noInline')
  static DoubleValue getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<DoubleValue>(create);
  static DoubleValue? _defaultInstance;

  @$pb.TagNumber(1)
  $core.double get value => $_getN(0);
  @$pb.TagNumber(1)
  set value($core.double v) { $_setDouble(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasValue() => $_has(0);
  @$pb.TagNumber(1)
  void clearValue() => clearField(1);
}

class Int32ListValue extends $pb.GeneratedMessage {
  factory Int32ListValue({
    $core.Iterable<$core.int>? value,
  }) {
    final $result = create();
    if (value != null) {
      $result.value.addAll(value);
    }
    return $result;
  }
  Int32ListValue._() : super();
  factory Int32ListValue.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Int32ListValue.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Int32ListValue', createEmptyInstance: create)
    ..p<$core.int>(1, _omitFieldNames ? '' : 'value', $pb.PbFieldType.K3)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Int32ListValue clone() => Int32ListValue()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Int32ListValue copyWith(void Function(Int32ListValue) updates) => super.copyWith((message) => updates(message as Int32ListValue)) as Int32ListValue;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Int32ListValue create() => Int32ListValue._();
  Int32ListValue createEmptyInstance() => create();
  static $pb.PbList<Int32ListValue> createRepeated() => $pb.PbList<Int32ListValue>();
  @$core.pragma('dart2js:noInline')
  static Int32ListValue getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Int32ListValue>(create);
  static Int32ListValue? _defaultInstance;

  @$pb.TagNumber(1)
  $core.List<$core.int> get value => $_getList(0);
}

class Int64ListValue extends $pb.GeneratedMessage {
  factory Int64ListValue({
    $core.Iterable<$fixnum.Int64>? value,
  }) {
    final $result = create();
    if (value != null) {
      $result.value.addAll(value);
    }
    return $result;
  }
  Int64ListValue._() : super();
  factory Int64ListValue.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Int64ListValue.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Int64ListValue', createEmptyInstance: create)
    ..p<$fixnum.Int64>(1, _omitFieldNames ? '' : 'value', $pb.PbFieldType.K6)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Int64ListValue clone() => Int64ListValue()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Int64ListValue copyWith(void Function(Int64ListValue) updates) => super.copyWith((message) => updates(message as Int64ListValue)) as Int64ListValue;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Int64ListValue create() => Int64ListValue._();
  Int64ListValue createEmptyInstance() => create();
  static $pb.PbList<Int64ListValue> createRepeated() => $pb.PbList<Int64ListValue>();
  @$core.pragma('dart2js:noInline')
  static Int64ListValue getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Int64ListValue>(create);
  static Int64ListValue? _defaultInstance;

  @$pb.TagNumber(1)
  $core.List<$fixnum.Int64> get value => $_getList(0);
}

class DoubleListValue extends $pb.GeneratedMessage {
  factory DoubleListValue({
    $core.Iterable<$core.double>? value,
  }) {
    final $result = create();
    if (value != null) {
      $result.value.addAll(value);
    }
    return $result;
  }
  DoubleListValue._() : super();
  factory DoubleListValue.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory DoubleListValue.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'DoubleListValue', createEmptyInstance: create)
    ..p<$core.double>(1, _omitFieldNames ? '' : 'value', $pb.PbFieldType.KD)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  DoubleListValue clone() => DoubleListValue()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  DoubleListValue copyWith(void Function(DoubleListValue) updates) => super.copyWith((message) => updates(message as DoubleListValue)) as DoubleListValue;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static DoubleListValue create() => DoubleListValue._();
  DoubleListValue createEmptyInstance() => create();
  static $pb.PbList<DoubleListValue> createRepeated() => $pb.PbList<DoubleListValue>();
  @$core.pragma('dart2js:noInline')
  static DoubleListValue getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<DoubleListValue>(create);
  static DoubleListValue? _defaultInstance;

  @$pb.TagNumber(1)
  $core.List<$core.double> get value => $_getList(0);
}

class Float64ListValue extends $pb.GeneratedMessage {
  factory Float64ListValue({
    $core.Iterable<$core.double>? value,
  }) {
    final $result = create();
    if (value != null) {
      $result.value.addAll(value);
    }
    return $result;
  }
  Float64ListValue._() : super();
  factory Float64ListValue.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Float64ListValue.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Float64ListValue', createEmptyInstance: create)
    ..p<$core.double>(1, _omitFieldNames ? '' : 'value', $pb.PbFieldType.KD)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Float64ListValue clone() => Float64ListValue()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Float64ListValue copyWith(void Function(Float64ListValue) updates) => super.copyWith((message) => updates(message as Float64ListValue)) as Float64ListValue;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Float64ListValue create() => Float64ListValue._();
  Float64ListValue createEmptyInstance() => create();
  static $pb.PbList<Float64ListValue> createRepeated() => $pb.PbList<Float64ListValue>();
  @$core.pragma('dart2js:noInline')
  static Float64ListValue getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Float64ListValue>(create);
  static Float64ListValue? _defaultInstance;

  @$pb.TagNumber(1)
  $core.List<$core.double> get value => $_getList(0);
}


const _omitFieldNames = $core.bool.fromEnvironment('protobuf.omit_field_names');
const _omitMessageNames = $core.bool.fromEnvironment('protobuf.omit_message_names');
