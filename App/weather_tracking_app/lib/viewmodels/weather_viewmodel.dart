import 'package:geolocator/geolocator.dart';
import 'package:permission_handler/permission_handler.dart';
import 'package:mobx/mobx.dart';
import '../models/weather_model.dart';
import '../services/weather_service.dart';

part 'weather_viewmodel.g.dart';

class WeatherViewModel = _WeatherViewModelBase with _$WeatherViewModel;

abstract class _WeatherViewModelBase with Store {
  final WeatherService _weatherService = WeatherService();

  @observable
  WeatherModel? weather;

  @observable
  bool isLoading = false;

  @action
  Future<void> loadWeather() async {
    isLoading = true;

    // Solicitar permissão para acessar a localização
    final status = await Permission.location.request();

    if (status.isGranted) {
      try {
        // Obter a localização do usuário
        Position position = await Geolocator.getCurrentPosition(desiredAccuracy: LocationAccuracy.high);
        final lat = position.latitude;
        final lon = position.longitude;

        // Aqui você pode usar uma API de geocodificação para converter lat/lon em nome da cidade
        // Exemplo fictício de nome da cidade
        final city = 'Curitiba, PR'; // Substitua com a cidade real obtida da geocodificação

        weather = await _weatherService.fetchWeatherData(city);
      } catch (e) {
        // Lidar com erros, como falhas na obtenção da localização
        print('Error getting location: $e');
      }
    } else {
      // Lidar com o caso em que a permissão é negada
      print('Location permission denied');
    }

    isLoading = false;
  }
}
