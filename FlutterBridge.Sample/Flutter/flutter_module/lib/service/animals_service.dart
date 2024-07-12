import 'dart:async';
import 'package:flutter/services.dart';
import 'package:flutter_maui_bridge/flutter_bridge.dart';
import 'package:flutter_maui_bridge/proto/flutter_maui_bridge.pb.dart';
import '../proto/flutter_module_animals.pb.dart';

class AnimalsService {
  static const String _serviceName = "animals_service";
  static final FlutterBridge _bridge = FlutterBridge();

  // Operations *****************************
  static const _kGetAnimals = 'GetAnimals()';
  Future<AnimalListValue> getAnimals() async {
    try {
      var result = await _bridge.invokeMethod(
        service: _serviceName,
        operation: _kGetAnimals,
        arguments: null,
      );
      return AnimalListValue.fromBuffer(result as Uint8List);
    } on PlatformException catch (e) {
      throw Exception("Unable to execute method 'getAnimals': ${e.code}, ${e.message}");
    } on BridgeException catch (fe) {
      throw fe;
    } on Exception catch (e) {
      throw Exception("Unable to execute method 'getAnimals': $e");
    }
  }
}
