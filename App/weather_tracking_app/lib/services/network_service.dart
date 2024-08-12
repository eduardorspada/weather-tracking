import 'dart:convert';
import 'package:http/http.dart' as http;
import 'auth_service.dart';

class NetworkService {
  static const String baseUrl = 'http://localhost:5001/api/';
  final AuthService _authService = AuthService();

  Future<Map<String, dynamic>> loginUser(String email, String password) async {
    const String url = '${baseUrl}Token/LoginUser';

    final response = await http.post(
      Uri.parse(url),
      headers: {'Content-Type': 'application/json'},
      body: json.encode({'email': email, 'password': password}),
    );

    if (response.statusCode == 200) {
      final data = json.decode(response.body);
      await _authService.saveToken(data['token']);
      return data;
    } else {
      throw Exception('Failed to login');
    }
  }

  Future<Map<String, dynamic>> loginWithGoogle(String code) async {
    const String url = '${baseUrl}Account/google-login';

    final response = await http.get(
      Uri.parse('$url?code=$code'),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      final data = json.decode(response.body);
      await _authService.saveToken(data['token']);
      return data;
    } else {
      throw Exception('Failed to login with Google');
    }
  }
}
