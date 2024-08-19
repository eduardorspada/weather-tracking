class WeatherModel {
  final String cityName;
  final String description;
  final double temperature;
  final double maxTemperature;
  final double minTemperature;
  final double thermalSensation;
  final double humidity;
  final double windSpeed;
  final String windDirection;
  final int uvIndex;
  final double pollenCount;
  final double dustLevel;
  final DateTime sunrise;
  final DateTime sunset;
  final int cityId;

  WeatherModel({
    required this.cityName,
    required this.description,
    required this.temperature,
    required this.maxTemperature,
    required this.minTemperature,
    required this.thermalSensation,
    required this.humidity,
    required this.windSpeed,
    required this.windDirection,
    required this.uvIndex,
    required this.pollenCount,
    required this.dustLevel,
    required this.sunrise,
    required this.sunset,
    required this.cityId,
  });

  factory WeatherModel.fromJson(Map<String, dynamic> json) {
    return WeatherModel(
      cityName: json['cityName'],
      description: json['description'],
      temperature: json['temperature'].toDouble(),
      maxTemperature: json['maxTemperature'].toDouble(),
      minTemperature: json['minTemperature'].toDouble(),
      thermalSensation: json['thermalSensation'].toDouble(),
      humidity: json['humidity'].toDouble(),
      windSpeed: json['windSpeed'].toDouble(),
      windDirection: json['windDirection'],
      uvIndex: json['uvIndex'],
      pollenCount: json['pollenCount'].toDouble(),
      dustLevel: json['dustLevel'].toDouble(),
      sunrise: DateTime.parse(json['sunrise']),
      sunset: DateTime.parse(json['sunset']),
      cityId: json['cityId'],
    );
  }
}
