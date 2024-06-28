import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter_maui_bridge/flutter_bridge.dart';
import 'package:flutter_module/proto/flutter_module.pb.dart';
import 'package:flutter_module/service/counter_service.dart';

void main() async {
  // Configure the bridge mode
  // http://semantic-portal.net/flutter-development-existing-app-running
  // By attaching to Flutter on device, it does not need to set Websocket mode for debugging.
  // VSCode  (ctrl+shift+p: Debug: Attach Flutter on Device or terminal: flutter attach)
  // IntelliJ (click Flutter Attach button)
  // Method 1: manual set mode then runApp
  // FlutterBridgeConfig.mode = FlutterBridgeMode.PlatformChannel;
  // runApp(MyApp());
  // Method 2: auto init mode then runApp when init completed
  WidgetsFlutterBinding.ensureInitialized();
  await FlutterBridgeConfig.initMode();
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
  // Reference to the native (MAUI) component that holds the business logic
  final CounterService _counterService = CounterService();

  // The current counter value
  int _counterValue = 0;
  String _counterError = "";

  void _load() async {
    // Get the value from MAUI
    try {
      int value = await _counterService.getValue();
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
        _counterError = "increased";
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
        _counterError = "decreased";
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
