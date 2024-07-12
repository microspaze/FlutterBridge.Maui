//
//  Generated code. Do not modify.
//  source: proto/flutter_module_animals.proto
//
// @dart = 2.12

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names, library_prefixes
// ignore_for_file: non_constant_identifier_names, prefer_final_fields
// ignore_for_file: unnecessary_import, unnecessary_this, unused_import

import 'dart:core' as $core;

import 'package:protobuf/protobuf.dart' as $pb;

class AnimalListValue extends $pb.GeneratedMessage {
  factory AnimalListValue({
    $core.Iterable<Animal>? value,
  }) {
    final $result = create();
    if (value != null) {
      $result.value.addAll(value);
    }
    return $result;
  }
  AnimalListValue._() : super();
  factory AnimalListValue.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory AnimalListValue.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'AnimalListValue', createEmptyInstance: create)
    ..pc<Animal>(1, _omitFieldNames ? '' : 'value', $pb.PbFieldType.PM, subBuilder: Animal.create)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  AnimalListValue clone() => AnimalListValue()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  AnimalListValue copyWith(void Function(AnimalListValue) updates) => super.copyWith((message) => updates(message as AnimalListValue)) as AnimalListValue;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static AnimalListValue create() => AnimalListValue._();
  AnimalListValue createEmptyInstance() => create();
  static $pb.PbList<AnimalListValue> createRepeated() => $pb.PbList<AnimalListValue>();
  @$core.pragma('dart2js:noInline')
  static AnimalListValue getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<AnimalListValue>(create);
  static AnimalListValue? _defaultInstance;

  @$pb.TagNumber(1)
  $core.List<Animal> get value => $_getList(0);
}

class Animal extends $pb.GeneratedMessage {
  factory Animal({
    $core.String? name,
    $core.String? birthday,
    $core.List<$core.int>? image,
    $core.double? weight,
    $core.double? height,
    $core.String? type,
    Butterfly? butterfly,
    Elephant? elephant,
    Monkey? monkey,
    Parrot? parrot,
    Rhino? rhino,
    Snake? snake,
    Tiger? tiger,
  }) {
    final $result = create();
    if (name != null) {
      $result.name = name;
    }
    if (birthday != null) {
      $result.birthday = birthday;
    }
    if (image != null) {
      $result.image = image;
    }
    if (weight != null) {
      $result.weight = weight;
    }
    if (height != null) {
      $result.height = height;
    }
    if (type != null) {
      $result.type = type;
    }
    if (butterfly != null) {
      $result.butterfly = butterfly;
    }
    if (elephant != null) {
      $result.elephant = elephant;
    }
    if (monkey != null) {
      $result.monkey = monkey;
    }
    if (parrot != null) {
      $result.parrot = parrot;
    }
    if (rhino != null) {
      $result.rhino = rhino;
    }
    if (snake != null) {
      $result.snake = snake;
    }
    if (tiger != null) {
      $result.tiger = tiger;
    }
    return $result;
  }
  Animal._() : super();
  factory Animal.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Animal.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Animal', createEmptyInstance: create)
    ..aOS(1, _omitFieldNames ? '' : 'name')
    ..aOS(2, _omitFieldNames ? '' : 'birthday')
    ..a<$core.List<$core.int>>(3, _omitFieldNames ? '' : 'image', $pb.PbFieldType.OY)
    ..a<$core.double>(4, _omitFieldNames ? '' : 'weight', $pb.PbFieldType.OD)
    ..a<$core.double>(5, _omitFieldNames ? '' : 'height', $pb.PbFieldType.OD)
    ..aOS(6, _omitFieldNames ? '' : 'type')
    ..aOM<Butterfly>(16, _omitFieldNames ? '' : 'butterfly', subBuilder: Butterfly.create)
    ..aOM<Elephant>(17, _omitFieldNames ? '' : 'elephant', subBuilder: Elephant.create)
    ..aOM<Monkey>(18, _omitFieldNames ? '' : 'monkey', subBuilder: Monkey.create)
    ..aOM<Parrot>(19, _omitFieldNames ? '' : 'parrot', subBuilder: Parrot.create)
    ..aOM<Rhino>(20, _omitFieldNames ? '' : 'rhino', subBuilder: Rhino.create)
    ..aOM<Snake>(21, _omitFieldNames ? '' : 'snake', subBuilder: Snake.create)
    ..aOM<Tiger>(22, _omitFieldNames ? '' : 'tiger', subBuilder: Tiger.create)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Animal clone() => Animal()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Animal copyWith(void Function(Animal) updates) => super.copyWith((message) => updates(message as Animal)) as Animal;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Animal create() => Animal._();
  Animal createEmptyInstance() => create();
  static $pb.PbList<Animal> createRepeated() => $pb.PbList<Animal>();
  @$core.pragma('dart2js:noInline')
  static Animal getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Animal>(create);
  static Animal? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get name => $_getSZ(0);
  @$pb.TagNumber(1)
  set name($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasName() => $_has(0);
  @$pb.TagNumber(1)
  void clearName() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get birthday => $_getSZ(1);
  @$pb.TagNumber(2)
  set birthday($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasBirthday() => $_has(1);
  @$pb.TagNumber(2)
  void clearBirthday() => clearField(2);

  @$pb.TagNumber(3)
  $core.List<$core.int> get image => $_getN(2);
  @$pb.TagNumber(3)
  set image($core.List<$core.int> v) { $_setBytes(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasImage() => $_has(2);
  @$pb.TagNumber(3)
  void clearImage() => clearField(3);

  @$pb.TagNumber(4)
  $core.double get weight => $_getN(3);
  @$pb.TagNumber(4)
  set weight($core.double v) { $_setDouble(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasWeight() => $_has(3);
  @$pb.TagNumber(4)
  void clearWeight() => clearField(4);

  @$pb.TagNumber(5)
  $core.double get height => $_getN(4);
  @$pb.TagNumber(5)
  set height($core.double v) { $_setDouble(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasHeight() => $_has(4);
  @$pb.TagNumber(5)
  void clearHeight() => clearField(5);

  @$pb.TagNumber(6)
  $core.String get type => $_getSZ(5);
  @$pb.TagNumber(6)
  set type($core.String v) { $_setString(5, v); }
  @$pb.TagNumber(6)
  $core.bool hasType() => $_has(5);
  @$pb.TagNumber(6)
  void clearType() => clearField(6);

  @$pb.TagNumber(16)
  Butterfly get butterfly => $_getN(6);
  @$pb.TagNumber(16)
  set butterfly(Butterfly v) { setField(16, v); }
  @$pb.TagNumber(16)
  $core.bool hasButterfly() => $_has(6);
  @$pb.TagNumber(16)
  void clearButterfly() => clearField(16);
  @$pb.TagNumber(16)
  Butterfly ensureButterfly() => $_ensure(6);

  @$pb.TagNumber(17)
  Elephant get elephant => $_getN(7);
  @$pb.TagNumber(17)
  set elephant(Elephant v) { setField(17, v); }
  @$pb.TagNumber(17)
  $core.bool hasElephant() => $_has(7);
  @$pb.TagNumber(17)
  void clearElephant() => clearField(17);
  @$pb.TagNumber(17)
  Elephant ensureElephant() => $_ensure(7);

  @$pb.TagNumber(18)
  Monkey get monkey => $_getN(8);
  @$pb.TagNumber(18)
  set monkey(Monkey v) { setField(18, v); }
  @$pb.TagNumber(18)
  $core.bool hasMonkey() => $_has(8);
  @$pb.TagNumber(18)
  void clearMonkey() => clearField(18);
  @$pb.TagNumber(18)
  Monkey ensureMonkey() => $_ensure(8);

  @$pb.TagNumber(19)
  Parrot get parrot => $_getN(9);
  @$pb.TagNumber(19)
  set parrot(Parrot v) { setField(19, v); }
  @$pb.TagNumber(19)
  $core.bool hasParrot() => $_has(9);
  @$pb.TagNumber(19)
  void clearParrot() => clearField(19);
  @$pb.TagNumber(19)
  Parrot ensureParrot() => $_ensure(9);

  @$pb.TagNumber(20)
  Rhino get rhino => $_getN(10);
  @$pb.TagNumber(20)
  set rhino(Rhino v) { setField(20, v); }
  @$pb.TagNumber(20)
  $core.bool hasRhino() => $_has(10);
  @$pb.TagNumber(20)
  void clearRhino() => clearField(20);
  @$pb.TagNumber(20)
  Rhino ensureRhino() => $_ensure(10);

  @$pb.TagNumber(21)
  Snake get snake => $_getN(11);
  @$pb.TagNumber(21)
  set snake(Snake v) { setField(21, v); }
  @$pb.TagNumber(21)
  $core.bool hasSnake() => $_has(11);
  @$pb.TagNumber(21)
  void clearSnake() => clearField(21);
  @$pb.TagNumber(21)
  Snake ensureSnake() => $_ensure(11);

  @$pb.TagNumber(22)
  Tiger get tiger => $_getN(12);
  @$pb.TagNumber(22)
  set tiger(Tiger v) { setField(22, v); }
  @$pb.TagNumber(22)
  $core.bool hasTiger() => $_has(12);
  @$pb.TagNumber(22)
  void clearTiger() => clearField(22);
  @$pb.TagNumber(22)
  Tiger ensureTiger() => $_ensure(12);
}

class Butterfly extends $pb.GeneratedMessage {
  factory Butterfly({
    $core.double? antennaLength,
  }) {
    final $result = create();
    if (antennaLength != null) {
      $result.antennaLength = antennaLength;
    }
    return $result;
  }
  Butterfly._() : super();
  factory Butterfly.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Butterfly.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Butterfly', createEmptyInstance: create)
    ..a<$core.double>(1, _omitFieldNames ? '' : 'antennaLength', $pb.PbFieldType.OD)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Butterfly clone() => Butterfly()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Butterfly copyWith(void Function(Butterfly) updates) => super.copyWith((message) => updates(message as Butterfly)) as Butterfly;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Butterfly create() => Butterfly._();
  Butterfly createEmptyInstance() => create();
  static $pb.PbList<Butterfly> createRepeated() => $pb.PbList<Butterfly>();
  @$core.pragma('dart2js:noInline')
  static Butterfly getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Butterfly>(create);
  static Butterfly? _defaultInstance;

  @$pb.TagNumber(1)
  $core.double get antennaLength => $_getN(0);
  @$pb.TagNumber(1)
  set antennaLength($core.double v) { $_setDouble(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasAntennaLength() => $_has(0);
  @$pb.TagNumber(1)
  void clearAntennaLength() => clearField(1);
}

class Elephant extends $pb.GeneratedMessage {
  factory Elephant({
    $core.double? trunkLength,
  }) {
    final $result = create();
    if (trunkLength != null) {
      $result.trunkLength = trunkLength;
    }
    return $result;
  }
  Elephant._() : super();
  factory Elephant.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Elephant.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Elephant', createEmptyInstance: create)
    ..a<$core.double>(1, _omitFieldNames ? '' : 'trunkLength', $pb.PbFieldType.OD)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Elephant clone() => Elephant()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Elephant copyWith(void Function(Elephant) updates) => super.copyWith((message) => updates(message as Elephant)) as Elephant;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Elephant create() => Elephant._();
  Elephant createEmptyInstance() => create();
  static $pb.PbList<Elephant> createRepeated() => $pb.PbList<Elephant>();
  @$core.pragma('dart2js:noInline')
  static Elephant getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Elephant>(create);
  static Elephant? _defaultInstance;

  @$pb.TagNumber(1)
  $core.double get trunkLength => $_getN(0);
  @$pb.TagNumber(1)
  set trunkLength($core.double v) { $_setDouble(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasTrunkLength() => $_has(0);
  @$pb.TagNumber(1)
  void clearTrunkLength() => clearField(1);
}

class Monkey extends $pb.GeneratedMessage {
  factory Monkey({
    $core.int? smartLevel,
  }) {
    final $result = create();
    if (smartLevel != null) {
      $result.smartLevel = smartLevel;
    }
    return $result;
  }
  Monkey._() : super();
  factory Monkey.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Monkey.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Monkey', createEmptyInstance: create)
    ..a<$core.int>(1, _omitFieldNames ? '' : 'smartLevel', $pb.PbFieldType.O3)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Monkey clone() => Monkey()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Monkey copyWith(void Function(Monkey) updates) => super.copyWith((message) => updates(message as Monkey)) as Monkey;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Monkey create() => Monkey._();
  Monkey createEmptyInstance() => create();
  static $pb.PbList<Monkey> createRepeated() => $pb.PbList<Monkey>();
  @$core.pragma('dart2js:noInline')
  static Monkey getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Monkey>(create);
  static Monkey? _defaultInstance;

  @$pb.TagNumber(1)
  $core.int get smartLevel => $_getIZ(0);
  @$pb.TagNumber(1)
  set smartLevel($core.int v) { $_setSignedInt32(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasSmartLevel() => $_has(0);
  @$pb.TagNumber(1)
  void clearSmartLevel() => clearField(1);
}

class Parrot extends $pb.GeneratedMessage {
  factory Parrot({
    $core.int? beakLength,
  }) {
    final $result = create();
    if (beakLength != null) {
      $result.beakLength = beakLength;
    }
    return $result;
  }
  Parrot._() : super();
  factory Parrot.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Parrot.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Parrot', createEmptyInstance: create)
    ..a<$core.int>(1, _omitFieldNames ? '' : 'beakLength', $pb.PbFieldType.O3)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Parrot clone() => Parrot()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Parrot copyWith(void Function(Parrot) updates) => super.copyWith((message) => updates(message as Parrot)) as Parrot;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Parrot create() => Parrot._();
  Parrot createEmptyInstance() => create();
  static $pb.PbList<Parrot> createRepeated() => $pb.PbList<Parrot>();
  @$core.pragma('dart2js:noInline')
  static Parrot getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Parrot>(create);
  static Parrot? _defaultInstance;

  @$pb.TagNumber(1)
  $core.int get beakLength => $_getIZ(0);
  @$pb.TagNumber(1)
  set beakLength($core.int v) { $_setSignedInt32(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasBeakLength() => $_has(0);
  @$pb.TagNumber(1)
  void clearBeakLength() => clearField(1);
}

class Rhino extends $pb.GeneratedMessage {
  factory Rhino({
    $core.double? hornLength,
  }) {
    final $result = create();
    if (hornLength != null) {
      $result.hornLength = hornLength;
    }
    return $result;
  }
  Rhino._() : super();
  factory Rhino.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Rhino.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Rhino', createEmptyInstance: create)
    ..a<$core.double>(1, _omitFieldNames ? '' : 'hornLength', $pb.PbFieldType.OD)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Rhino clone() => Rhino()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Rhino copyWith(void Function(Rhino) updates) => super.copyWith((message) => updates(message as Rhino)) as Rhino;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Rhino create() => Rhino._();
  Rhino createEmptyInstance() => create();
  static $pb.PbList<Rhino> createRepeated() => $pb.PbList<Rhino>();
  @$core.pragma('dart2js:noInline')
  static Rhino getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Rhino>(create);
  static Rhino? _defaultInstance;

  @$pb.TagNumber(1)
  $core.double get hornLength => $_getN(0);
  @$pb.TagNumber(1)
  set hornLength($core.double v) { $_setDouble(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasHornLength() => $_has(0);
  @$pb.TagNumber(1)
  void clearHornLength() => clearField(1);
}

class Snake extends $pb.GeneratedMessage {
  factory Snake({
    $core.int? venomLevel,
  }) {
    final $result = create();
    if (venomLevel != null) {
      $result.venomLevel = venomLevel;
    }
    return $result;
  }
  Snake._() : super();
  factory Snake.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Snake.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Snake', createEmptyInstance: create)
    ..a<$core.int>(1, _omitFieldNames ? '' : 'venomLevel', $pb.PbFieldType.O3)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Snake clone() => Snake()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Snake copyWith(void Function(Snake) updates) => super.copyWith((message) => updates(message as Snake)) as Snake;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Snake create() => Snake._();
  Snake createEmptyInstance() => create();
  static $pb.PbList<Snake> createRepeated() => $pb.PbList<Snake>();
  @$core.pragma('dart2js:noInline')
  static Snake getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Snake>(create);
  static Snake? _defaultInstance;

  @$pb.TagNumber(1)
  $core.int get venomLevel => $_getIZ(0);
  @$pb.TagNumber(1)
  set venomLevel($core.int v) { $_setSignedInt32(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasVenomLevel() => $_has(0);
  @$pb.TagNumber(1)
  void clearVenomLevel() => clearField(1);
}

class Tiger extends $pb.GeneratedMessage {
  factory Tiger({
    $core.double? clawLength,
  }) {
    final $result = create();
    if (clawLength != null) {
      $result.clawLength = clawLength;
    }
    return $result;
  }
  Tiger._() : super();
  factory Tiger.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Tiger.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Tiger', createEmptyInstance: create)
    ..a<$core.double>(1, _omitFieldNames ? '' : 'clawLength', $pb.PbFieldType.OD)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Tiger clone() => Tiger()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Tiger copyWith(void Function(Tiger) updates) => super.copyWith((message) => updates(message as Tiger)) as Tiger;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Tiger create() => Tiger._();
  Tiger createEmptyInstance() => create();
  static $pb.PbList<Tiger> createRepeated() => $pb.PbList<Tiger>();
  @$core.pragma('dart2js:noInline')
  static Tiger getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Tiger>(create);
  static Tiger? _defaultInstance;

  @$pb.TagNumber(1)
  $core.double get clawLength => $_getN(0);
  @$pb.TagNumber(1)
  set clawLength($core.double v) { $_setDouble(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasClawLength() => $_has(0);
  @$pb.TagNumber(1)
  void clearClawLength() => clearField(1);
}


const _omitFieldNames = $core.bool.fromEnvironment('protobuf.omit_field_names');
const _omitMessageNames = $core.bool.fromEnvironment('protobuf.omit_message_names');
