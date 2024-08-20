import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:weather_tracking_app/services/network_service.dart';

class GooglePlacesService {
  final String apiKey;
  final NetworkService _networkService = NetworkService();

  GooglePlacesService(this.apiKey);

  Future<Map<String, String>> getCityAndState(
      double latitude, double longitude) async {
    final url =
        'https://maps.googleapis.com/maps/api/geocode/json?latlng=$latitude,$longitude&key=$apiKey';

    final response = await http.get(Uri.parse(url));
    final json = jsonDecode(response.body);

    if (json['status'] == 'OK') {
      final addressComponents =
          json['results'][0]['address_components'] as List;
      String? city;
      String? state;
      String? stateFull;
      String? country;
      String? countryFull;
      for (var component in addressComponents) {
        final types = component['types'] as List;
        if (types.contains('administrative_area_level_2')) {
          city = component['long_name'];
        } else if (types.contains('administrative_area_level_1')) {
          state = component['short_name'];
          stateFull = component['long_name'];
        } else if (types.contains('country')) {
          country = component['short_name'];
          countryFull = component['long_name'];
        }
      }

      if (city != null &&
          state != null &&
          stateFull != null &&
          country != null &&
          countryFull != null) {
        _networkService.registerAddress(
            city, state, stateFull, country, countryFull);
        return {
          'city': city,
          'state': state,
          'stateFull': stateFull,
          'country': country,
          'countryFull': countryFull
        };
      }
    }

    throw Exception('Failed to get city and state');
  }
}
