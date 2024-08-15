import 'package:flutter_modular/flutter_modular.dart';
import 'services/weather_service.dart';
import 'viewmodels/weather_viewmodel.dart';
import 'views/home/home_page.dart';
import 'views/auth/login_page.dart';
import 'viewmodels/auth_viewmodel.dart';

class AppModule extends Module {
  @override
  List<Bind> get binds => [
    Bind.singleton((i) => AuthViewModel()),
    Bind.singleton((i) => WeatherViewModel()),
    Bind.singleton((i) => WeatherService()),
    // Outros binds...
  ];

  @override
  List<ModularRoute> get routes => [
    ChildRoute('/', child: (_, __) => const LoginPage()),
    ChildRoute('/home', child: (_, __) => const HomePage()),

    // Outras rotas...
  ];
}
