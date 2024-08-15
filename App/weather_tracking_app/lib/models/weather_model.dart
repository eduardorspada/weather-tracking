class WeatherModel {
  final String city;
  final String description;
  final double temperature;
  final double humidity;
  final double windSpeed;
  final int uvIndex;
  final double pollenCount;
  final double dustLevel;
  final DateTime sunrise;
  final DateTime sunset;

  WeatherModel({
    required this.city,
    required this.description,
    required this.temperature,
    required this.humidity,
    required this.windSpeed,
    required this.uvIndex,
    required this.pollenCount,
    required this.dustLevel,
    required this.sunrise,
    required this.sunset,
  });

  factory WeatherModel.fromJson(Map<String, dynamic> json) {
    return WeatherModel(
      city: json['city'],
      description: json['description'],
      temperature: json['temperature'].toDouble(),
      humidity: json['humidity'].toDouble(),
      windSpeed: json['windSpeed'].toDouble(),
      uvIndex: json['uvIndex'],
      pollenCount: json['pollenCount'].toDouble(),
      dustLevel: json['dustLevel'].toDouble(),
      sunrise: DateTime.parse(json['sunrise']),
      sunset: DateTime.parse(json['sunset']),
    );
  }
}
