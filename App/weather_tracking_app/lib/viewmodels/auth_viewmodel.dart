import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import '../services/auth_service.dart';
import '../services/network_service.dart';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:google_sign_in/google_sign_in.dart';

class AuthViewModel extends ChangeNotifier {
  final TextEditingController emailController = TextEditingController();
  final TextEditingController passwordController = TextEditingController();
  final TextEditingController confirmPasswordController =
      TextEditingController();
  final NetworkService _networkService = NetworkService();
  final AuthService _authService = AuthService();

  bool isLoading = false;
  String? errorMessage;

  bool _isSignUpMode = false;

  bool get isSignUpMode => _isSignUpMode;

  // Método para alternar entre modos de login e inscrição
  void toggleSignUpMode() {
    _isSignUpMode = !_isSignUpMode;
    notifyListeners();
  }

  // Método de Cadastro (Sign Up)
  Future<void> signUp(BuildContext context) async {
    isLoading = true;
    errorMessage = null;
    notifyListeners();

    try {
      // Realiza o cadastro
      await _networkService.signupUser(emailController.text,
          passwordController.text, confirmPasswordController.text);

      // Se o cadastro for bem-sucedido, faz login automaticamente
      await login(context);
    } catch (e) {
      print('Erro de cadastro: ${e.toString()}');
      errorMessage = 'Sign Up failed. Please try again.';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }

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

      // Verificar userProfileId após login
      final userInfo = await _networkService.getMyUserInformation();

      if (userInfo['personId'] == 0) {
        Modular.to.pushReplacementNamed('/register_person');
      } else {
        Modular.to.pushReplacementNamed('/home');
      }
    } catch (e) {
      print('Erro de login: ${e.toString()}');
      errorMessage = 'Login failed. Please check your credentials.';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }

  Future<void> loginWithGoogle() async {
    try {
      // Inicia o processo de login com Google
      final GoogleSignInAccount? googleUser = await GoogleSignIn().signIn();

      // Se o usuário abortar o login
      if (googleUser == null) {
        print("Google sign-in was aborted.");
        return;
      }

      // Autentica o usuário no Google e obtém os tokens
      final GoogleSignInAuthentication googleAuth =
          await googleUser.authentication;

      // Cria uma credencial com os tokens recebidos
      final OAuthCredential credential = GoogleAuthProvider.credential(
        accessToken: googleAuth.accessToken,
        idToken: googleAuth.idToken,
      );

      // Usa a credencial para autenticar no Firebase
      UserCredential userCredential =
          await FirebaseAuth.instance.signInWithCredential(credential);

      String? idToken = await FirebaseAuth.instance.currentUser?.getIdToken();

      print(idToken);

      final userInfo = await _networkService.loginWithGoogle(idToken);
      print(userInfo['personId']);
      if (userInfo['personId'] == null) {
        Modular.to.pushReplacementNamed('/register_person');
      } else {
        Modular.to.pushReplacementNamed('/home');
      }
      // Usuário autenticado com sucesso
      print("User signed in: ${userCredential.user?.displayName}");
    } catch (e) {
      // Tratamento de erro
      print("Error during Google sign-in: $e");
    }
  }
}
