// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'res_counter_service_get_value.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ResCounterServiceGetValue _$ResCounterServiceGetValueFromJson(Map json) =>
    ResCounterServiceGetValue(
      returnValue: (json['ReturnValue'] as num?)?.toInt() ?? 0,
    );

Map<String, dynamic> _$ResCounterServiceGetValueToJson(
        ResCounterServiceGetValue instance) =>
    <String, dynamic>{
      'ReturnValue': instance.returnValue,
    };
