import 'dart:async';
import 'package:flutter/services.dart';
import 'package:flutter_maui_bridge/flutter_bridge.dart';
import 'package:flutter_maui_bridge/proto/flutter_maui_bridge.pb.dart';
import 'package:flutter_module/proto/flutter_module.pb.dart';

class CounterService {
  static const String _serviceName = "counter_service";
  static final FlutterBridge _bridge = FlutterBridge();

  // Events *****************************
  final Stream<CounterValueResult> _valueChanged = _bridge
      .events(serviceName: _serviceName, eventName: 'valueChanged')
      .map((_) => CounterValueResult.fromBuffer(_));
  Stream<CounterValueResult> get valueChanged => _valueChanged;

  // Operations *****************************
  static const _kGetValue = 'GetValue()';
  Future<int> getValue() async {
    try {
      await _bridge.invokeMethod(
        service: _serviceName,
        operation: _kGetValue,
        arguments: null,
      );
      return 0;
    } on PlatformException catch (e) {
      throw Exception(
          "Unable to execute method 'getValue': ${e.code}, ${e.message}");
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
      throw Exception(
          "Unable to execute method 'increment': ${e.code}, ${e.message}");
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
      throw Exception(
          "Unable to execute method 'decrement': ${e.code}, ${e.message}");
    } on BridgeException catch (fe) {
      throw fe;
    } on Exception catch (e) {
      throw Exception("Unable to execute method 'decrement': $e");
    }
  }
}
