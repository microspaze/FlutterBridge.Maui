// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'flutnet_exception.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

FlutnetException _$FlutnetExceptionFromJson(Map json) => FlutnetException(
      code: $enumDecodeNullable(_$FlutnetErrorCodeEnumMap, json['Code']) ??
          FlutnetErrorCode.OperationFailed,
      message: json['Message'] as String? ?? "",
    );

Map<String, dynamic> _$FlutnetExceptionToJson(FlutnetException instance) =>
    <String, dynamic>{
      'Message': instance.message,
      'Code': _$FlutnetErrorCodeEnumMap[instance.code]!,
    };

const _$FlutnetErrorCodeEnumMap = {
  FlutnetErrorCode.OperationNotImplemented: 'OperationNotImplemented',
  FlutnetErrorCode.OperationArgumentCountMismatch:
      'OperationArgumentCountMismatch',
  FlutnetErrorCode.InvalidOperationArguments: 'InvalidOperationArguments',
  FlutnetErrorCode.OperationArgumentParsingError:
      'OperationArgumentParsingError',
  FlutnetErrorCode.OperationFailed: 'OperationFailed',
  FlutnetErrorCode.OperationCanceled: 'OperationCanceled',
};
