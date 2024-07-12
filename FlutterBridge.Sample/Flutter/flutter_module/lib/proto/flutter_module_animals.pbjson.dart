//
//  Generated code. Do not modify.
//  source: proto/flutter_module_animals.proto
//
// @dart = 2.12

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names, library_prefixes
// ignore_for_file: non_constant_identifier_names, prefer_final_fields
// ignore_for_file: unnecessary_import, unnecessary_this, unused_import

import 'dart:convert' as $convert;
import 'dart:core' as $core;
import 'dart:typed_data' as $typed_data;

@$core.Deprecated('Use animalListValueDescriptor instead')
const AnimalListValue$json = {
  '1': 'AnimalListValue',
  '2': [
    {'1': 'value', '3': 1, '4': 3, '5': 11, '6': '.Animal', '10': 'value'},
  ],
};

/// Descriptor for `AnimalListValue`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List animalListValueDescriptor = $convert.base64Decode(
    'Cg9BbmltYWxMaXN0VmFsdWUSHQoFdmFsdWUYASADKAsyBy5BbmltYWxSBXZhbHVl');

@$core.Deprecated('Use animalDescriptor instead')
const Animal$json = {
  '1': 'Animal',
  '2': [
    {'1': 'name', '3': 1, '4': 1, '5': 9, '10': 'name'},
    {'1': 'birthday', '3': 2, '4': 1, '5': 9, '10': 'birthday'},
    {'1': 'image', '3': 3, '4': 1, '5': 12, '10': 'image'},
    {'1': 'weight', '3': 4, '4': 1, '5': 1, '10': 'weight'},
    {'1': 'height', '3': 5, '4': 1, '5': 1, '10': 'height'},
    {'1': 'type', '3': 6, '4': 1, '5': 9, '10': 'type'},
    {'1': 'butterfly', '3': 16, '4': 1, '5': 11, '6': '.Butterfly', '9': 0, '10': 'butterfly', '17': true},
    {'1': 'elephant', '3': 17, '4': 1, '5': 11, '6': '.Elephant', '9': 1, '10': 'elephant', '17': true},
    {'1': 'monkey', '3': 18, '4': 1, '5': 11, '6': '.Monkey', '9': 2, '10': 'monkey', '17': true},
    {'1': 'parrot', '3': 19, '4': 1, '5': 11, '6': '.Parrot', '9': 3, '10': 'parrot', '17': true},
    {'1': 'rhino', '3': 20, '4': 1, '5': 11, '6': '.Rhino', '9': 4, '10': 'rhino', '17': true},
    {'1': 'snake', '3': 21, '4': 1, '5': 11, '6': '.Snake', '9': 5, '10': 'snake', '17': true},
    {'1': 'tiger', '3': 22, '4': 1, '5': 11, '6': '.Tiger', '9': 6, '10': 'tiger', '17': true},
  ],
  '8': [
    {'1': '_butterfly'},
    {'1': '_elephant'},
    {'1': '_monkey'},
    {'1': '_parrot'},
    {'1': '_rhino'},
    {'1': '_snake'},
    {'1': '_tiger'},
  ],
};

/// Descriptor for `Animal`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List animalDescriptor = $convert.base64Decode(
    'CgZBbmltYWwSEgoEbmFtZRgBIAEoCVIEbmFtZRIaCghiaXJ0aGRheRgCIAEoCVIIYmlydGhkYX'
    'kSFAoFaW1hZ2UYAyABKAxSBWltYWdlEhYKBndlaWdodBgEIAEoAVIGd2VpZ2h0EhYKBmhlaWdo'
    'dBgFIAEoAVIGaGVpZ2h0EhIKBHR5cGUYBiABKAlSBHR5cGUSLQoJYnV0dGVyZmx5GBAgASgLMg'
    'ouQnV0dGVyZmx5SABSCWJ1dHRlcmZseYgBARIqCghlbGVwaGFudBgRIAEoCzIJLkVsZXBoYW50'
    'SAFSCGVsZXBoYW50iAEBEiQKBm1vbmtleRgSIAEoCzIHLk1vbmtleUgCUgZtb25rZXmIAQESJA'
    'oGcGFycm90GBMgASgLMgcuUGFycm90SANSBnBhcnJvdIgBARIhCgVyaGlubxgUIAEoCzIGLlJo'
    'aW5vSARSBXJoaW5viAEBEiEKBXNuYWtlGBUgASgLMgYuU25ha2VIBVIFc25ha2WIAQESIQoFdG'
    'lnZXIYFiABKAsyBi5UaWdlckgGUgV0aWdlcogBAUIMCgpfYnV0dGVyZmx5QgsKCV9lbGVwaGFu'
    'dEIJCgdfbW9ua2V5QgkKB19wYXJyb3RCCAoGX3JoaW5vQggKBl9zbmFrZUIICgZfdGlnZXI=');

@$core.Deprecated('Use butterflyDescriptor instead')
const Butterfly$json = {
  '1': 'Butterfly',
  '2': [
    {'1': 'antenna_length', '3': 1, '4': 1, '5': 1, '10': 'antennaLength'},
  ],
};

/// Descriptor for `Butterfly`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List butterflyDescriptor = $convert.base64Decode(
    'CglCdXR0ZXJmbHkSJQoOYW50ZW5uYV9sZW5ndGgYASABKAFSDWFudGVubmFMZW5ndGg=');

@$core.Deprecated('Use elephantDescriptor instead')
const Elephant$json = {
  '1': 'Elephant',
  '2': [
    {'1': 'trunk_length', '3': 1, '4': 1, '5': 1, '10': 'trunkLength'},
  ],
};

/// Descriptor for `Elephant`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List elephantDescriptor = $convert.base64Decode(
    'CghFbGVwaGFudBIhCgx0cnVua19sZW5ndGgYASABKAFSC3RydW5rTGVuZ3Ro');

@$core.Deprecated('Use monkeyDescriptor instead')
const Monkey$json = {
  '1': 'Monkey',
  '2': [
    {'1': 'smart_level', '3': 1, '4': 1, '5': 5, '10': 'smartLevel'},
  ],
};

/// Descriptor for `Monkey`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List monkeyDescriptor = $convert.base64Decode(
    'CgZNb25rZXkSHwoLc21hcnRfbGV2ZWwYASABKAVSCnNtYXJ0TGV2ZWw=');

@$core.Deprecated('Use parrotDescriptor instead')
const Parrot$json = {
  '1': 'Parrot',
  '2': [
    {'1': 'beak_length', '3': 1, '4': 1, '5': 5, '10': 'beakLength'},
  ],
};

/// Descriptor for `Parrot`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List parrotDescriptor = $convert.base64Decode(
    'CgZQYXJyb3QSHwoLYmVha19sZW5ndGgYASABKAVSCmJlYWtMZW5ndGg=');

@$core.Deprecated('Use rhinoDescriptor instead')
const Rhino$json = {
  '1': 'Rhino',
  '2': [
    {'1': 'horn_length', '3': 1, '4': 1, '5': 1, '10': 'hornLength'},
  ],
};

/// Descriptor for `Rhino`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List rhinoDescriptor = $convert.base64Decode(
    'CgVSaGlubxIfCgtob3JuX2xlbmd0aBgBIAEoAVIKaG9ybkxlbmd0aA==');

@$core.Deprecated('Use snakeDescriptor instead')
const Snake$json = {
  '1': 'Snake',
  '2': [
    {'1': 'venom_level', '3': 1, '4': 1, '5': 5, '10': 'venomLevel'},
  ],
};

/// Descriptor for `Snake`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List snakeDescriptor = $convert.base64Decode(
    'CgVTbmFrZRIfCgt2ZW5vbV9sZXZlbBgBIAEoBVIKdmVub21MZXZlbA==');

@$core.Deprecated('Use tigerDescriptor instead')
const Tiger$json = {
  '1': 'Tiger',
  '2': [
    {'1': 'claw_length', '3': 1, '4': 1, '5': 1, '10': 'clawLength'},
  ],
};

/// Descriptor for `Tiger`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List tigerDescriptor = $convert.base64Decode(
    'CgVUaWdlchIfCgtjbGF3X2xlbmd0aBgBIAEoAVIKY2xhd0xlbmd0aA==');

