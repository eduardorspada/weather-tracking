import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:weather_tracking_app/firebase_options.dart';
import 'network_service.dart';

class NotificationService {
  final FirebaseMessaging _firebaseMessaging = FirebaseMessaging.instance;
  final NetworkService _networkService = NetworkService();

  Future<void> initialize() async {
    // Inicializa o Firebase
    await Firebase.initializeApp(
      options: DefaultFirebaseOptions.currentPlatform,
    );

    // Solicite permissões para notificações
    NotificationSettings settings = await _firebaseMessaging.requestPermission(
      alert: true,
      announcement: false,
      badge: true,
      carPlay: false,
      criticalAlert: false,
      provisional: false,
      sound: true,
    );

    if (settings.authorizationStatus == AuthorizationStatus.authorized) {
      print('Usuário permitiu notificações');
    } else if (settings.authorizationStatus ==
        AuthorizationStatus.provisional) {
      print('Usuário permitiu notificações provisórias');
    } else {
      print('Usuário não permitiu notificações');
    }

    // Obtenha o token FCM para este dispositivo
    String? token = await _firebaseMessaging.getToken();
    print('FCM Token: $token');

    registerDeviceToken(token, 'deviceName');

    // Adicione um listener para mensagens em segundo plano
    FirebaseMessaging.onBackgroundMessage(_firebaseMessagingBackgroundHandler);
  }

  Future<void> registerDeviceToken(String? token, String deviceName) async {
    await _networkService.addMyDeviceNotification(token, deviceName, true);
    print('Registrar token: $token para o dispositivo: $deviceName');
  }

  static Future<void> _firebaseMessagingBackgroundHandler(
      RemoteMessage message) async {
    await Firebase.initializeApp();
    print("Handling a background message: ${message.messageId}");
  }
}
