import 'package:flutter/material.dart';

class WeatherCardWidget extends StatelessWidget {
  final String title;
  final IconData icon;
  final Widget content;

  const WeatherCardWidget({
    required this.title,
    required this.icon,
    required this.content,
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Card(
      color: const Color.fromARGB(255, 151, 112, 173),
      elevation: 5,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(15)),
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              children: [
                Icon(icon, size: 30, color: Colors.white),
                SizedBox(width: 10),
                Text(
                  title,
                  style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold, color: Colors.white),
                ),
              ],
            ),
            SizedBox(height: 10),
            content,
          ],
        ),
      ),
    );
  }
}
