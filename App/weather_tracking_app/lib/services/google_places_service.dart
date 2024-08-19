import 'dart:convert';
import 'package:http/http.dart' as http;

class GooglePlacesService {
  final String apiKey;

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
      for (var component in addressComponents) {
        final types = component['types'] as List;
        if (types.contains('administrative_area_level_2')) {
          city = component['long_name'];
        } else if (types.contains('administrative_area_level_1')) {
          state = component['short_name'];
        }
      }

      if (city != null && state != null) {
        return {'city': city, 'state': state};
      }
    }

    throw Exception('Failed to get city and state');
  }
}
