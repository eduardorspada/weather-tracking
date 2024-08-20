import 'package:mobx/mobx.dart';
import '../models/weather_model.dart';
import '../services/weather_service.dart';
import '../services/google_places_service.dart';
import 'package:geolocator/geolocator.dart';
import 'package:permission_handler/permission_handler.dart';

part 'weather_viewmodel.g.dart';

class WeatherViewModel = _WeatherViewModelBase with _$WeatherViewModel;

abstract class _WeatherViewModelBase with Store {
  final WeatherService _weatherService = WeatherService();
  final GooglePlacesService _googlePlacesService;

  _WeatherViewModelBase(this._googlePlacesService);

  @observable
  WeatherModel? weather;

  @observable
  bool isLoading = false;

  @action
  Future<void> loadWeather() async {
    isLoading = true;

    final status = await Permission.location.request();

    if (status.isGranted) {
      try {
        Position position = await Geolocator.getCurrentPosition(
            desiredAccuracy: LocationAccuracy.high);
        final lat = position.latitude;
        final lon = position.longitude;

        // Obter cidade e estado usando Google Places API
        final cityState = await _googlePlacesService.getCityAndState(lat, lon);
        
        final cityName = '${cityState['city']}, ${cityState['state']}';
        print(cityName);

        weather = await _weatherService.fetchWeatherData(cityName);
      } catch (e) {
        print('Error getting location or weather: $e');
      }
    } else {
      print('Location permission denied');
    }

    isLoading = false;
  }
}
