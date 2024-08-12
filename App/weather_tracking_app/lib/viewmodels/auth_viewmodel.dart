import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:url_launcher/url_launcher.dart';
import '../services/auth_service.dart';
import '../services/network_service.dart';

class AuthViewModel extends ChangeNotifier {
  final TextEditingController emailController = TextEditingController();
  final TextEditingController passwordController = TextEditingController();
  final NetworkService _networkService = NetworkService();
  final AuthService _authService = AuthService();

  bool isLoading = false;
  String? errorMessage;

  Future<void> checkLoginStatus(BuildContext context) async {
    final String? token = await _authService.getToken();
    if (token != null) {
      Modular.to.navigate('/home');
    }
  }

  Future<void> login(BuildContext context) async {
    isLoading = true;
    errorMessage = null;
    notifyListeners();

    try {
      await _networkService.loginUser(
        emailController.text,
        passwordController.text,
      );
      Modular.to.pushReplacementNamed('/home');
    } catch (e) {
      print('Erro de login: ${e.toString()}');
      errorMessage = 'Login failed. Please check your credentials.';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }

  Future<void> loginWithGoogle(BuildContext context) async {
    isLoading = true;
    errorMessage = null;
    notifyListeners();

    try {
      final Uri googleAuthUrl = Uri.parse(
          'https://accounts.google.com/o/oauth2/auth?client_id=887198971078-fr2b8n2o950jk8sesdh6qo3vqhdrfb13.apps.googleusercontent.com&redirect_uri=http://localhost:5001/api/Account/google-login&response_type=code&scope=profile email');

      // Abre o navegador para o usuário obter o código de autorização
      if (await canLaunchUrl(googleAuthUrl)) {
        await launchUrl(googleAuthUrl);
      } else {
        throw 'Não foi possível abrir o navegador';
      }
    } catch (e) {
      print('Erro de login com Google: ${e.toString()}');
      errorMessage = 'Google login failed. Please try again.';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }
}
