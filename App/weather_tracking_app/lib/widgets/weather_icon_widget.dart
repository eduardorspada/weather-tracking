import 'package:flutter/material.dart';

class WeatherIconWidget extends StatelessWidget {
  final String description;

  const WeatherIconWidget({required this.description, super.key});

  @override
  Widget build(BuildContext context) {
    IconData icon;

    switch (description.toLowerCase()) {
      case 'clear':
        icon = Icons.wb_sunny;
        break;
      case 'cloudy':
        icon = Icons.wb_cloudy;
        break;
      case 'rain':
        icon = Icons.grain;
        break;
      default:
        icon = Icons.help_outline;
    }

    return Icon(icon, size: 100, color: Colors.white);
  }
}
