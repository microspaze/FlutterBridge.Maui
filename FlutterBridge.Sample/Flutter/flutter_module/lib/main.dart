import 'package:flutter/material.dart';
import 'package:flutter_maui_bridge/flutter_bridge.dart';
import '../animals/animals_page.dart';
import '../counter/counter_page.dart';

void main() async {
  // Configure the bridge mode
  // http://semantic-portal.net/flutter-development-existing-app-running
  // By attaching to Flutter on device, it does not need to set Websocket mode for debugging.
  // VSCode  (ctrl+shift+p: Debug: Attach Flutter on Device or terminal: flutter attach)
  // IntelliJ (click Flutter Attach button)
  // Method 1: manual set mode then runApp
  // FlutterBridgeConfig.mode = BridgeMode.PlatformChannel;
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
        // Try running your application with "flutter run". You'll see the
        // application has a blue toolbar. Then, without quitting the app, try
        // changing the primarySwatch below to Colors.green and then invoke
        // "hot reload" (press "r" in the console where you ran "flutter run",
        // or press Run > Flutter Hot Reload in a Flutter IDE). Notice that the
        // counter didn't reset back to zero; the application is not restarted.
        primarySwatch: Colors.blue,
      ),
      //home: AnimalsPage(title: "Animals Demo"),// home property and route '/' can only exist one
      routes: {
        '/': (context) => CounterPage(title: "Flutter Demo Home"),
        '/counter': (context) => CounterPage(title: "Counter Demo"),
        "/animals": (context) => AnimalsPage(title: "Animals Demo"),
      },
    );
  }
}
