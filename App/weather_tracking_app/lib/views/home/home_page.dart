import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:flutter_mobx/flutter_mobx.dart';
import '../../viewmodels/weather_viewmodel.dart';
import '../../widgets/profile_widget.dart';
import '../../widgets/weather_card_widget.dart';

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    final weatherViewModel = Modular.get<WeatherViewModel>();

    WidgetsBinding.instance.addPostFrameCallback((_) {
      weatherViewModel.loadWeather();
    });

    return Scaffold(
      backgroundColor: const Color.fromARGB(255, 84, 37, 99),
      drawer: Drawer(
        child: ListView(
          padding: EdgeInsets.zero,
          children: [
            DrawerHeader(
              decoration: BoxDecoration(
                color: const Color.fromARGB(255, 63, 47, 78),
              ),
              child: Text('Saved Locations', style: TextStyle(color: Colors.white, fontSize: 24)),
            ),
            ListTile(
              title: Text('Curitiba, PR'),
              onTap: () {
                weatherViewModel.loadWeather();
                Navigator.pop(context);
              },
            ),
            ListTile(
              title: Text('São Paulo, SP'),
              onTap: () {
                // Load weather for São Paulo, SP
                Navigator.pop(context);
              },
            ),
            ProfileWidget(),
          ],
        ),
      ),
      appBar: AppBar(
        title: Text('Weather App'),
        actions: [
          IconButton(
            icon: Icon(Icons.refresh),
            onPressed: () {
              weatherViewModel.loadWeather();
            },
          ),
        ],
      ),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Observer(
            builder: (_) {
              if (weatherViewModel.isLoading) {
                return Center(child: CircularProgressIndicator(color: Colors.white));
              }

              if (weatherViewModel.weather == null) {
                return Center(child: Text('Unable to load weather data', style: TextStyle(color: Colors.white)));
              }

              final weather = weatherViewModel.weather!;

              return SingleChildScrollView(
                child: Column(
                  children: [
                    WeatherCardWidget(
                      title: 'Current Weather',
                      icon: Icons.wb_sunny,
                      content: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            weather.cityName,
                            style: TextStyle(fontSize: 32, fontWeight: FontWeight.bold, color: Colors.white),
                          ),
                          SizedBox(height: 16),
                          Text(
                            '${weather.temperature.toStringAsFixed(1)}°C',
                            style: TextStyle(fontSize: 64, fontWeight: FontWeight.bold, color: Colors.white),
                          ),
                          SizedBox(height: 10),
                          Text(
                            weather.description,
                            style: TextStyle(fontSize: 24, color: Colors.white),
                          ),
                        ],
                      ),
                    ),
                    SizedBox(height: 20),
                    WeatherCardWidget(
                      title: 'Next Hours',
                      icon: Icons.schedule,
                      content: Text('Temperature forecast for the next hours', style: TextStyle(color: Colors.white)),
                    ),
                    SizedBox(height: 20),
                    WeatherCardWidget(
                      title: 'Next Days',
                      icon: Icons.calendar_today,
                      content: Text('Weather forecast for the next days', style: TextStyle(color: Colors.white)),
                    ),
                    SizedBox(height: 20),
                    WeatherCardWidget(
                      title: 'Sunrise & Sunset',
                      icon: Icons.wb_sunny,
                      content: Text(
                        'Sunrise: ${weather.sunrise.hour}:${weather.sunrise.minute}\nSunset: ${weather.sunset.hour}:${weather.sunset.minute}',
                        style: TextStyle(color: Colors.white),
                      ),
                    ),
                    SizedBox(height: 20),
                    WeatherCardWidget(
                      title: 'Air Quality',
                      icon: Icons.cloud,
                      content: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text('Humidity: ${weather.humidity}%', style: TextStyle(color: Colors.white)),
                          Text('Dust Level: ${weather.dustLevel}', style: TextStyle(color: Colors.white)),
                          Text('Pollen Count: ${weather.pollenCount}', style: TextStyle(color: Colors.white)),
                        ],
                      ),
                    ),
                    SizedBox(height: 20),
                    WeatherCardWidget(
                      title: 'Wind & UV',
                      icon: Icons.waves,
                      content: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text('Wind Speed: ${weather.windSpeed} m/s', style: TextStyle(color: Colors.white)),
                          Text('UV Index: ${weather.uvIndex}', style: TextStyle(color: Colors.white)),
                        ],
                      ),
                    ),
                  ],
                ),
              );
            },
          ),
        ),
      ),
    );
  }
}
