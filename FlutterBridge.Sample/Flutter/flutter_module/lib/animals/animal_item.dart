import 'dart:typed_data';
import 'package:flutter/material.dart';
import '../proto/flutter_module_animals.pb.dart';

class AnimalItem extends StatelessWidget {
  final Animal animal;
  final void Function(Animal animal)? onTap;

  AnimalItem(
    this.animal, {
    Key? key,
    this.onTap,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    bool animalHaveImage = animal.hasImage();

    return ListTile(
      isThreeLine: true,
      onTap: () => onTap!(animal),
      leading: CircleAvatar(
        backgroundImage: animalHaveImage ? MemoryImage(animal.image as Uint8List) : null,
        child: animalHaveImage == false
            ? ClipOval(
                child: AspectRatio(
                    aspectRatio: 1,
                    child: Center(
                        child: Text(
                      "A",
                      style: TextStyle(fontSize: 22, color: Colors.white),
                    ))),
              )
            : null,
      ),
      title: Text(
        "${animal.name}, born in ${animal.birthday}",
      ),
      subtitle: Row(
        children: [
          Column(
            mainAxisAlignment: MainAxisAlignment.start,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                "${animal.weight} Kg, ${animal.height} cm",
              ),
              _getSpecificInformations(this.animal),
            ],
          ),
        ],
      ),
    );
  }

  Widget _getSpecificInformations(Animal animal) {
    String info = "";

    if (animal.hasButterfly()) {
      info = "Antenna: ${animal.butterfly.antennaLength} cm";
    } else if (animal.hasElephant()) {
      info = "Trunk: ${animal.elephant.trunkLength} cm";
    } else if (animal.hasMonkey()) {
      info = "IQ: ${animal.monkey.smartLevel}";
    } else if (animal.hasParrot()) {
      info = "Beak: ${animal.parrot.beakLength} cm";
    } else if (animal.hasRhino()) {
      info = "Horn: ${animal.rhino.hornLength} cm";
    } else if (animal.hasSnake()) {
      info = "Venom: ${animal.snake.venomLevel} Lv";
    } else if (animal.hasTiger()) {
      info = "Claw: ${animal.tiger.clawLength} cm";
    }

    return Text(info);
  }
}
