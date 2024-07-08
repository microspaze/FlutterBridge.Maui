import 'dart:async';
import 'dart:typed_data';
import 'package:fixnum/fixnum.dart';
import 'package:flutter/services.dart';
import 'package:flutter_maui_bridge/flutter_bridge.dart';
import 'package:flutter_maui_bridge/proto/flutter_maui_bridge.pb.dart';
import 'package:flutter_module/proto/flutter_module.pb.dart';

class CounterService {
  static const String _serviceName = "counter_service";
  static final FlutterBridge _bridge = FlutterBridge();

  // Events *****************************
  final Stream<CounterValueResult> _valueChanged = _bridge.events(serviceName: _serviceName, eventName: 'valueChanged').map((_) => CounterValueResult.fromBuffer(_));
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
      var sleeps = Float64List.fromList([4.5, 2.1, 2.4, 5.5]); //DO NOT USE Float32List, There's no float type in Dart;
      var stamps = Int64List.fromList([birth]); //Int64List
      var stampList = [Int64(birth)]; //List<Int64>
      var milstamps = Float64List.fromList([1707447360000.5]); //Float64List
      var avatar = StringValue(value: "avatar").writeToBuffer(); //Uint8List
      var prevValue = currentValue >= 0 ? CounterValueResult(value: currentValue).writeToBuffer() : null; //Model
      var useProto = currentValue % 2 == 0;
      var arguments = Map<String, dynamic>();
      arguments["name"] = !useProto ? name : StringValue(value: name).writeToBuffer();
      arguments["male"] = !useProto ? male : BoolValue(value: male).writeToBuffer();
      arguments["age"] = !useProto ? age : Int32Value(value: age).writeToBuffer();
      arguments["birth"] = !useProto ? birth : Int64Value(value: Int64(birth)).writeToBuffer();
      arguments["weight"] = !useProto ? weight : DoubleValue(value: weight).writeToBuffer();
      arguments["milks"] = !useProto ? milks : Int32ListValue(value: milks).writeToBuffer();
      arguments["sleeps"] = !useProto ? sleeps : DoubleListValue(value: sleeps).writeToBuffer();
      arguments["stamps"] = !useProto ? stamps : Int64ListValue(value: stampList).writeToBuffer();
      arguments["milstamps"] = !useProto ? milstamps : Float64ListValue(value: milstamps).writeToBuffer();
      arguments["avatar"] = avatar;
      arguments["prev"] = prevValue;
      var result = await _bridge.invokeMethod(
        service: _serviceName,
        operation: _kGetValue,
        arguments: arguments,
      );
      var resultValue = currentValue;
      if (result == null) {
        return resultValue;
      }
      dynamic resultModel;
      switch (currentValue % 13) {
        case 0:
          resultModel = Int32Value.fromBuffer(result); //int32
          if (resultModel is Int32Value) {
            resultValue = resultModel.value;
          }
          break;
        case 1:
          resultModel = StringValue.fromBuffer(result); //String
          break;
        case 2:
          resultModel = BoolValue.fromBuffer(result); //bool
          break;
        case 3:
          resultModel = Int32Value.fromBuffer(result); //int32
          break;
        case 4:
          resultModel = Int64Value.fromBuffer(result); //int64
          break;
        case 5:
          resultModel = DoubleValue.fromBuffer(result); //double
          break;
        case 6:
          resultModel = Int32ListValue.fromBuffer(result); //int[]
          break;
        case 7:
          resultModel = DoubleListValue.fromBuffer(result); //float[] There's no float type in Dart, use double instead.
          break;
        case 8:
          resultModel = Int64ListValue.fromBuffer(result); //long[]
          break;
        case 9:
          resultModel = Float64ListValue.fromBuffer(result); //double[] Float64ListValue equals DoubleListValue
          break;
        case 10:
          resultModel = result; //byte[]
          break;
        case 11:
          resultModel = CounterValueResult.fromBuffer(result); //Model
          break;
        case 12:
          // Exception from MAUI!
          break;
      }

      print(resultModel);

      return resultValue;
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
