import 'package:flutter_modular/flutter_modular.dart';
import 'views/home/home_page.dart';
import 'views/auth/login_page.dart';
import 'viewmodels/auth_viewmodel.dart';

class AppModule extends Module {
  @override
  List<Bind> get binds => [
    Bind.singleton((i) => AuthViewModel()),
    // Outros binds...
  ];

  @override
  List<ModularRoute> get routes => [
    ChildRoute('/', child: (_, __) => const LoginPage()),
    ChildRoute('/home', child: (_, __) => const HomePage()),

    // Outras rotas...
  ];
}
