import 'dart:async';
import 'package:flutter/services.dart';
import 'package:flutter_maui_bridge/flutter_bridge.dart';
import 'package:flutter_maui_bridge/proto/flutter_maui_bridge.pb.dart';
import 'package:flutter_module/proto/flutter_module.pb.dart';

class CounterService {
  static const String _type = 'FlutterBridge.Sample.Services.CounterService';

  CounterService(
    this.instanceId,
  ) : _valueChanged = FlutterBridge()
            .events(instanceId: instanceId, event: 'valueChanged')
            .map((_) => CounterValueResult.fromBuffer(_));

  final String instanceId;

  // Events *****************************
  final Stream<CounterValueResult> _valueChanged;
  Stream<CounterValueResult> get valueChanged => _valueChanged;

  // Operations *****************************
  static final _bridge = FlutterBridge();
  static const _kGetValue = 'GetValue()';
  Future<int> getValue() async {
    // Errors occurring on the platform side cause invokeMethod to throw
    // PlatformExceptions.
    try {
      await _bridge.invokeMethod(
        instanceId: instanceId,
        service: _type,
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
    // Errors occurring on the platform side cause invokeMethod to throw
    // PlatformExceptions.
    try {
      await _bridge.invokeMethod(
        instanceId: instanceId,
        service: _type,
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
    // Errors occurring on the platform side cause invokeMethod to throw
    // PlatformExceptions.
    try {
      await _bridge.invokeMethod(
        instanceId: instanceId,
        service: _type,
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
