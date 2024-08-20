import 'package:flutter_modular/flutter_modular.dart';
import 'services/google_places_service.dart';
import 'services/weather_service.dart';
import 'viewmodels/weather_viewmodel.dart';
import 'viewmodels/person_viewmodel.dart';
import 'viewmodels/auth_viewmodel.dart';
import 'views/auth/register_page.dart';
import 'views/person/register_person_page.dart';
import 'views/home/home_page.dart';
import 'views/auth/login_page.dart';
import 'services/notification_service.dart';
import 'viewmodels/notification_viewmodel.dart';

class AppModule extends Module {
  @override
  List<Bind> get binds => [
    Bind.singleton((i) => AuthViewModel()),
    Bind.singleton((i) => WeatherViewModel(i.get<GooglePlacesService>())),
    Bind.singleton((i) => GooglePlacesService("AIzaSyAhsOqXhmitfN_w1NTm7CyOlPvNkEX80bE")),
    Bind.singleton((i) => PersonViewModel()),
    Bind.singleton((i) => WeatherService()),
    Bind.singleton((i) => NotificationService()),
    Bind.singleton((i) => NotificationViewModel(i<NotificationService>())),
  ];

  @override
  List<ModularRoute> get routes => [
    ChildRoute('/', child: (_, __) => const LoginPage()),
    ChildRoute('/register', child: (_, __) => const RegisterPage()),
    ChildRoute('/register_person', child: (_, __) => const RegisterPersonPage()),
    ChildRoute('/home', child: (_, __) => const HomePage()),
  ];
}
