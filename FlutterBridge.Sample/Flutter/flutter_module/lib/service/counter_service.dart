import 'dart:async';
import 'dart:typed_data';
import 'package:fixnum/fixnum.dart';
import 'package:flutter/services.dart';
import 'package:flutter_maui_bridge/flutter_bridge.dart';
import 'package:flutter_maui_bridge/proto/flutter_maui_bridge.pb.dart';
import 'package:flutter_module/proto/flutter_module.pb.dart';
import 'package:protobuf_wellknown/protobuf_wellknown.dart';

class CounterService {
  static const String _serviceName = "counter_service";
  static final FlutterBridge _bridge = FlutterBridge();

  // Events *****************************
  final Stream<CounterValueResult> _valueChanged =
      _bridge.events(serviceName: _serviceName, eventName: 'valueChanged').map((_) => CounterValueResult.fromBuffer(_));
  Stream<CounterValueResult> get valueChanged => _valueChanged;

  // Operations *****************************
  static const _kGetValue = 'GetValue()';
  Future<int> getValue([int currentValue = 0]) async {
    try {
      var name = "flutterbridge"; //String
      var male = true; //bool
      var age = 1; //int32
      var birth = 1707447360000; //int64 from package:fixnum
      var weight = 3.7; //double
      var milks = Int32List.fromList([151, 120, 160, 130, 150]); //Int32List
      var sleeps = Float32List.fromList([4.5, 2.1, 2.4, 5.5]); //Float32List
      var stamps = Int64List.fromList([birth]); //Int64List
      var stampList = [Int64(birth)]; //List<Int64>
      var milstamps = Float64List.fromList([1707447360000.5]); //Float64List
      var avatar = StringValue(value: "avatar").writeToBuffer(); //Uint8List
      var prevValue = CounterValueResult(value: 0).writeToBuffer(); //Model
      var useProto = currentValue % 2 == 0;
      var arguments = Map<String, dynamic>();
      arguments["name"] = !useProto ? name : StringValue(value: name).writeToBuffer();
      arguments["male"] = !useProto ? male : BoolValue(value: male).writeToBuffer();
      arguments["age"] = !useProto ? age : Int32Value(value: age).writeToBuffer();
      arguments["birth"] = !useProto ? birth : Int64Value(value: Int64(birth)).writeToBuffer();
      arguments["weight"] = !useProto ? weight : DoubleValue(value: weight).writeToBuffer();
      arguments["milks"] = !useProto ? milks : Int32ListValue(value: milks).writeToBuffer();
      arguments["sleeps"] = !useProto ? sleeps : Float32ListValue(value: sleeps).writeToBuffer();
      arguments["stamps"] = !useProto ? stamps : Int64ListValue(value: stampList).writeToBuffer();
      arguments["milstamps"] = !useProto ? milstamps : Float64ListValue(value: milstamps).writeToBuffer();
      arguments["avatar"] = avatar;
      arguments["prevValue"] = prevValue;
      var result = await _bridge.invokeMethod(
        service: _serviceName,
        operation: _kGetValue,
        arguments: arguments,
      );
      return Int32Value.fromBuffer(result!).value;
    } on PlatformException catch (e) {
      throw Exception("Unable to execute method 'getValue': ${e.code}, ${e.message}");
    } on BridgeException catch (fe) {
      throw fe;
    } on Exception catch (e) {
      throw Exception("Unable to execute method 'getValue': $e");
    }
  }

  static const _kIncrement = 'Increment()';
  Future<void> increment() async {
    try {
      await _bridge.invokeMethod(
        service: _serviceName,
        operation: _kIncrement,
        arguments: null,
      );
    } on PlatformException catch (e) {
      throw Exception("Unable to execute method 'increment': ${e.code}, ${e.message}");
    } on BridgeException catch (fe) {
      throw fe;
    } on Exception catch (e) {
      throw Exception("Unable to execute method 'increment': $e");
    }
  }

  static const _kDecrement = 'Decrement()';
  Future<void> decrement() async {
    try {
      await _bridge.invokeMethod(
        service: _serviceName,
        operation: _kDecrement,
        arguments: null,
      );
    } on PlatformException catch (e) {
      throw Exception("Unable to execute method 'decrement': ${e.code}, ${e.message}");
    } on BridgeException catch (fe) {
      throw fe;
    } on Exception catch (e) {
      throw Exception("Unable to execute method 'decrement': $e");
    }
  }
}
