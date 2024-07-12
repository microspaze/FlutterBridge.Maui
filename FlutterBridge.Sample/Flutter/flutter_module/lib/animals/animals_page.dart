import 'dart:typed_data';
import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import '../proto/flutter_module_animals.pb.dart';
import '../service/animals_service.dart';
import '../animals/animal_item.dart';

class AnimalsPage extends StatefulWidget {
  AnimalsPage({Key? key, this.title}) : super(key: key);

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String? title;

  @override
  _AnimalsPageState createState() => _AnimalsPageState();
}

class _AnimalsPageState extends State<AnimalsPage> {
  // Maui Animal Service
  final AnimalsService _animalsService = AnimalsService();

  // Scaffold Key
  final _scaffoldKey = GlobalKey<ScaffoldState>();

  List<Animal> _animals = [];

  void _showSnackbar(
    BuildContext context,
    String message,
    bool isError,
  ) {
    Icon icon = Icon(Icons.check);
    Color background = Colors.green;

    if (isError) {
      icon = Icon(Icons.error);
      background = Colors.red;
    }

    final snackBar = SnackBar(
      duration: const Duration(seconds: 60),
      content: Row(
        children: <Widget>[
          icon,
          SizedBox(
            width: 10,
          ),
          Expanded(
            child: Text(
              message,
              overflow: TextOverflow.ellipsis,
              maxLines: 100,
            ),
          ),
        ],
      ),
      backgroundColor: background,
    );

    // Remove a snackbar if present
    //rootScaffoldMessengerKey.currentState.removeCurrentSnackBar();
    // Show the snackbar
    //rootScaffoldMessengerKey.currentState.showSnackBar(snackBar);
  }

  Future _loadAnimals() async {
    try {
      // Load the animals from the Maui service
      var animals = await _animalsService.getAnimals();

      // Update the UI State using Maui datas
      setState(() {
        _animals = animals.value;
      });
    } catch (ex) {
      print(ex);
      _showSnackbar(context, ex.toString(), true);
    }
  }

  @override
  void initState() {
    super.initState();
    // Load all the animals data after the first frame
    SchedulerBinding.instance.addPostFrameCallback((_) {
      _loadAnimals();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      key: _scaffoldKey,
      appBar: AppBar(
        // Here we take the value from the MyHomePage object that was created by
        // the App.build method, and use it to set our appbar title.
        title: Text(widget.title!),
      ),
      body: _buildContent(context),
      // This trailing comma makes auto-formatting nicer for build methods.
    );
  }

  // Build the contect of the page
  Widget _buildContent(BuildContext context) {
    if (_animals.isEmpty) {
      return Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            CircularProgressIndicator(),
            SizedBox(
              height: 3,
            ),
            Text("Loading Animals from Maui..."),
          ],
        ),
      );
    }

    // Show all the animals on the screen
    if (_animals.length > 0) {
      return ListView.builder(
        itemCount: _animals.length,
        itemBuilder: (context, index) {
          return AnimalItem(
            _animals[index],

            // When we tap the item
            onTap: (animal) async {
              // Get the animal kind from Maui
              try {
                // Show the details dialog
                await showDialog(
                  context: context,
                  builder: (_) => DetailsDialog(
                    animal.image as Uint8List,
                    animal.type,
                  ),
                );
              } catch (ex) {
                _showSnackbar(context, ex.toString(), true);
              }
            },
          );
        },
      );
    }

    return Center(
      child: TextButton(
        child: const Text("No animals to view"),
        onPressed: () => _loadAnimals(),
      ),
    );
  }
}

class DetailsDialog extends StatelessWidget {
  final String animalKind;
  final Uint8List image;

  const DetailsDialog(
    this.image,
    this.animalKind, {
    Key? key,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Dialog(
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          this.image.isEmpty
              ? Container(
                  height: 200,
                  child: Center(
                    child: Icon(
                      Icons.image,
                      size: 35,
                    ),
                  ),
                )
              : Container(
                  height: 200,
                  decoration: BoxDecoration(image: DecorationImage(image: MemoryImage(this.image), fit: BoxFit.cover)),
                ),
          Text(
            this.animalKind,
            style: Theme.of(context).primaryTextTheme.headlineMedium!.copyWith(
                  color: Colors.black,
                ),
          )
        ],
      ),
    );
  }
}
