// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'value_changed_event_args.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ValueChangedEventArgs _$ValueChangedEventArgsFromJson(Map json) =>
    ValueChangedEventArgs(
      value: (json['Value'] as num?)?.toInt() ?? 0,
    );

Map<String, dynamic> _$ValueChangedEventArgsToJson(
        ValueChangedEventArgs instance) =>
    <String, dynamic>{
      'Value': instance.value,
    };
