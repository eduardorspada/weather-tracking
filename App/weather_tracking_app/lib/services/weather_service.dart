import '../models/weather_model.dart';
import 'network_service.dart';

class WeatherService {
  final NetworkService _networkService = NetworkService();

  Future<WeatherModel> fetchWeatherData(String location) async {
    try {
      final apiResponse = await _networkService.GetWeatherCondition(location);

      if (apiResponse != null && apiResponse['isSuccess']) {
        // Extraímos o primeiro item da lista de dados dentro de 'data'
        final weatherData = apiResponse['data']['data'][0];

        // Convertendo o JSON para WeatherModel
        return WeatherModel.fromJson(weatherData);
      } else {
        throw Exception('Failed to fetch weather data');
      }
    } catch (e) {
      throw Exception('Error fetching weather data: $e');
    }
  }
}
