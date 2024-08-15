import 'package:flutter/material.dart';

class WeatherBackground extends StatelessWidget {
  const WeatherBackground({super.key});

  @override
  Widget build(BuildContext context) {
    return CustomPaint(
      size: MediaQuery.of(context).size,
      painter: WeatherPainter(),
    );
  }
}

class WeatherPainter extends CustomPainter {
  @override
  void paint(Canvas canvas, Size size) {
    final Paint paint = Paint()
      ..color = Colors.blueAccent
      ..style = PaintingStyle.fill;

    // Desenhar um fundo moderno com gradiente
    final Rect rect = Rect.fromLTRB(0, 0, size.width, size.height);
    final Gradient gradient = LinearGradient(
      colors: [Colors.blueAccent, Colors.lightBlueAccent],
      begin: Alignment.topCenter,
      end: Alignment.bottomCenter,
    );

    paint.shader = gradient.createShader(rect);
    canvas.drawRect(rect, paint);

    // Desenhar um círculo como representação do sol
    final Paint circlePaint = Paint()
      ..color = Colors.orangeAccent
      ..style = PaintingStyle.fill;

    canvas.drawCircle(
        Offset(size.width * 0.5, size.height * 0.3), 80, circlePaint);
  }

  @override
  bool shouldRepaint(covariant CustomPainter oldDelegate) {
    return false;
  }
}
