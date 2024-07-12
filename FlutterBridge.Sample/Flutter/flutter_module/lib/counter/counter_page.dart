import 'dart:async';
import 'package:flutter/material.dart';
import '../proto/flutter_module.pb.dart';
import '../service/counter_service.dart';

class CounterPage extends StatefulWidget {
  CounterPage({Key? key, this.title}) : super(key: key);

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String? title;

  @override
  _CounterPageState createState() => _CounterPageState();
}

class _CounterPageState extends State<CounterPage> {
  // Reference to the native (MAUI) component that holds the business logic
  final CounterService _counterService = CounterService();

  // The current counter value
  int _counterValue = 0;
  String _counterError = "";

  void _load() async {
    // Get the value from MAUI
    try {
      int value = await _counterService.getValue(_counterValue);
      setState(() {
        _counterValue = value;
        _counterError = "got";
      });
    } catch (ex) {
      setState(() {
        _counterError = ex.toString();
      });
    }
  }

  void _increment() async {
    // Increment the value by calling the proper native (MAUI) method
    try {
      await _counterService.increment();
      setState(() {
        _counterError = "added";
      });
    } catch (ex) {
      setState(() {
        _counterError = ex.toString();
      });
    }
  }

  void _decrement() async {
    // Decrement the value by calling the proper native (MAUI) method
    try {
      await _counterService.decrement();
      setState(() {
        _counterError = "subed";
      });
    } catch (ex) {
      setState(() {
        _counterError = ex.toString();
      });
    }
  }

  StreamSubscription<CounterValueResult>? _eventSubscription;

  @override
  void initState() {
    super.initState();
    _load();

    // Subscribe to the native (MAUI) ValueChanged event
    _eventSubscription = _counterService.valueChanged.listen(
      (CounterValueResult args) {
        setState(() {
          _counterValue = args.value;
        });
      },
      cancelOnError: false,
    );
  }

  @override
  void dispose() {
    // IMPORTANT: Cancel the subscription from the event to avoid memory leaks!
    _eventSubscription?.cancel();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title!),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Text(
              // Welcome message from MAUI
              "The current value is $_counterError",
              style: TextStyle(
                fontSize: 20,
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: Text(
                // Welcome message from MAUI
                "$_counterValue",
                style: TextStyle(
                  fontSize: 30,
                  color: Colors.red,
                ),
              ),
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: <Widget>[
                FloatingActionButton(
                  child: Icon(Icons.remove),
                  onPressed: () => _decrement(),
                ),
                FloatingActionButton(
                  child: Icon(Icons.abc_outlined),
                  onPressed: () => _load(),
                ),
                FloatingActionButton(
                  child: Icon(Icons.add),
                  onPressed: () => _increment(),
                ),
              ],
            )
          ],
        ),
      ),
    );
  }
}
