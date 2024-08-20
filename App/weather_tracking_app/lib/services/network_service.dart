import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:image_picker/image_picker.dart';
import 'package:weather_tracking_app/models/person_model.dart';
import 'auth_service.dart';

class NetworkService {
  static const String baseUrl = 'http://10.0.2.2:5001/api/';
  final AuthService _authService = AuthService();

  // Metodo que retorna a a weather condition atual
  Future<Map<String, dynamic>> GetWeatherCondition(String cityName) async {
    final String url =
        '${baseUrl}Weather/GetWeatherConditionsAsync?CityName=${cityName}&Active=true';
    final token = await _authService.getToken();

    final response = await http.get(
      Uri.parse(url),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
    );
    if (response.statusCode == 200) {
      final jsonResponse = json.decode(response.body);
      return jsonResponse;
    } else {
      throw Exception('Failed to fetch user information');
    }
  }

  // Método que retorna um Person de um usuário logado
  Future<PersonModel> MyPersonInformation() async {
    const String url = '${baseUrl}Person/MyPersonInformation';

    final token = await _authService.getToken();

    final response = await http.get(
      Uri.parse(url),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
    );
    if (response.statusCode == 200) {
      final jsonResponse = json.decode(response.body);
      return PersonModel.fromJson(jsonResponse);
    } else {
      throw Exception('Failed to fetch user information');
    }
  }

  // Método para registrode novo Address
  Future<void> registerAddress(String? cityName, String? state, String? stateFull, String? country, String? countryFull) async {
    const String url = '${baseUrl}Address/AddMyAddressAsync';

    final token = await _authService.getToken();

    final response = await http.post(
      Uri.parse(url),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
      body: json.encode({
        'cityName': cityName,
        'state': state,
        'stateFull': stateFull,
        'country': country,
        'countryFull': countryFull
      }),
    );

    if (response.statusCode != 200) {
      throw Exception('Failed to register address ${response.reasonPhrase}');
    }
  }
  // Método para registrode novo Person
  Future<void> registerPerson(String firstName, String lastName,
      String? birthday, XFile? profilePicture) async {
    const String url = '${baseUrl}Person';

    final token = await _authService.getToken();

    // Verifica se a imagem foi selecionada
    if (profilePicture != null) {
      // Convertendo a imagem em bytes para enviar na requisição
      final bytes = await profilePicture.readAsBytes();
      final encodedImage = base64Encode(bytes);
      print(encodedImage);

      final response = await http.post(
        Uri.parse(url),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer $token',
        },
        body: json.encode({
          'firstName': firstName,
          'lastName': lastName,
          'birthday': birthday,
          'profilePicture': encodedImage, // Envia a imagem codificada em base64
        }),
      );

      if (response.statusCode != 200) {
        throw Exception('Failed to register person ${response.reasonPhrase}');
      }
    } else {
      throw Exception('No profile picture selected');
    }
  }

  // Método para cadastro de um novo dispositivo
  Future<void> addMyDeviceNotification(String? fcmtoken, String deviceName, bool acceptNotifications) async {
    const String url = '${baseUrl}Device/AddMyDevice';
    final token = await _authService.getToken();
    final response = await http.post(
      Uri.parse(url),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
      body: json.encode({
        'token': fcmtoken,
        'deviceName': deviceName,
        'acceptNotifications': acceptNotifications
      }),
    );

    if (response.statusCode != 200) {
      throw Exception('Failed to add device - ${response.reasonPhrase}');
    }
  }

  // Método para cadastro de novo usuário
  Future<void> signupUser(
      String email, String password, String confirmPassword) async {
    const String url = '${baseUrl}user/signup';

    final response = await http.post(
      Uri.parse(url),
      headers: {'Content-Type': 'application/json'},
      body: json.encode({
        'email': email,
        'password': password,
        'confirmPassword': confirmPassword,
        'userProfileId': 5,
      }),
    );

    if (response.statusCode != 200) {
      throw Exception('Failed to sign up');
    }
  }

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

  Future<Map<String, dynamic>> getMyUserInformation() async {
    final String url = '${baseUrl}User/GetMyUserInformation';

    final token = await _authService.getToken();
    final response = await http.get(
      Uri.parse(url),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception('Failed to fetch user information');
    }
  }
}
