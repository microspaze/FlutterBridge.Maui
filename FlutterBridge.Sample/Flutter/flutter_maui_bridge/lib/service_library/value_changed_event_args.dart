import 'package:json_annotation/json_annotation.dart';
import 'package:meta/meta.dart';

part 'value_changed_event_args.g.dart';

/// An annotation for the code generator to know that this class needs the
/// the star denotes the source file name.
@immutable
@JsonSerializable(nullable: true, explicitToJson: true, anyMap: true)
class ValueChangedEventArgs {
  ValueChangedEventArgs({
    this.value = 0,
  });

  @JsonKey(name: "Value", nullable: false)
  final int value;

  factory ValueChangedEventArgs.fromJson(Map<dynamic, dynamic> json) =>
      _$ValueChangedEventArgsFromJson(json);

  Map<String, dynamic> toJson() => _$ValueChangedEventArgsToJson(this);
}
