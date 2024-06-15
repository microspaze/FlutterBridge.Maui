import 'dart:async';

import 'package:flutter/material.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // Try running your application with "flutter run". You'll see the
        // application has a blue toolbar. Then, without quitting the app, try
        // changing the primarySwatch below to Colors.green and then invoke
        // "hot reload" (press "r" in the console where you ran "flutter run",
        // or press Run > Flutter Hot Reload in a Flutter IDE). Notice that the
        // counter didn't reset back to zero; the application is not restarted.
        primarySwatch: Colors.blue,
      ),
      home: MyHomePage(title: 'Flutter Demo Home Page'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  MyHomePage({Key? key, this.title}) : super(key: key);

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String? title;

  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  // Reference to the native (Xamarin) component that holds the business logic
  //final CounterService _counterService = CounterService("counter_service");

  // The current counter value
  int _counterValue = 0;
  String _counterError = "";

  void _load() async {
    // Get the value from xamarin
    try {
      //int value = await _counterService.getValue();
      setState(() {
        //_counterValue = value;
        _counterError = "got";
      });
    } catch (ex) {
      setState(() {
        _counterError = ex.toString();
      });
    }
  }

  void _increment() async {
    // Increment the value by calling the proper native (Xamarin) method
    try {
      //await _counterService.increment();
      setState(() {
        _counterValue++;
        _counterError = "increased";
      });
    } catch (ex) {
      setState(() {
        _counterError = ex.toString();
      });
    }
  }

  void _decrement() async {
    // Decrement the value by calling the proper native (Xamarin) method
    try {
      //await _counterService.decrement();
      setState(() {
        _counterValue--;
        _counterError = "decreased";
      });
    } catch (ex) {
      setState(() {
        _counterError = ex.toString();
      });
    }
  }

  //StreamSubscription<ValueChangedEventArgs>? _eventSubscription;

  @override
  void initState() {
    super.initState();
    _load();

    // Subscribe to the native (Xamarin) ValueChanged event
    // _eventSubscription = _counterService.valueChanged.listen(
    //   (ValueChangedEventArgs args) {
    //     setState(() {
    //       _counterValue = args.value;
    //     });
    //   },
    //   cancelOnError: false,
    // );
  }

  @override
  void dispose() {
    // IMPORTANT: Cancel the subscription from the event to avoid memory leaks!
    //_eventSubscription?.cancel();
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
              // Welcome message from xamarin
              "The current value is $_counterError",
              style: TextStyle(
                fontSize: 20,
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: Text(
                // Welcome message from xamarin
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
