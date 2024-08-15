import 'dart:convert';

import '../models/weather_model.dart';

class WeatherService {
  Future<WeatherModel> fetchWeatherData(String location) async {
    // Verifica se a localização é Curitiba–PR para retornar dados mockados
    if (location.toLowerCase() == 'curitiba, pr') {
      // Dados mockados para Curitiba–PR
      final mockResponse = {
        "city": "Curitiba, PR",
        "description": "Partly Cloudy",
        "temperature": 22.5,
        "humidity": 78,
        "windSpeed": 15.2,
        "uvIndex": 5,
        "pollenCount": 10,
        "dustLevel": 15,
        "sunrise": "2024-08-14T06:30:00Z",
        "sunset": "2024-08-14T17:45:00Z",
      };

      // Converte o mockResponse para JSON e depois para WeatherModel
      return WeatherModel.fromJson(mockResponse);
    } else {
      // Se não for Curitiba–PR, retorna uma exceção (ou você pode implementar uma chamada real à API)
      throw Exception('Location not supported in mock');
    }
  }
}
