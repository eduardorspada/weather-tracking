// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'weather_viewmodel.dart';

// **************************************************************************
// StoreGenerator
// **************************************************************************

// ignore_for_file: non_constant_identifier_names, unnecessary_brace_in_string_interps, unnecessary_lambdas, prefer_expression_function_bodies, lines_longer_than_80_chars, avoid_as, avoid_annotating_with_dynamic, no_leading_underscores_for_local_identifiers

mixin _$WeatherViewModel on _WeatherViewModelBase, Store {
  late final _$weatherAtom =
      Atom(name: '_WeatherViewModelBase.weather', context: context);

  @override
  WeatherModel? get weather {
    _$weatherAtom.reportRead();
    return super.weather;
  }

  @override
  set weather(WeatherModel? value) {
    _$weatherAtom.reportWrite(value, super.weather, () {
      super.weather = value;
    });
  }

  late final _$isLoadingAtom =
      Atom(name: '_WeatherViewModelBase.isLoading', context: context);

  @override
  bool get isLoading {
    _$isLoadingAtom.reportRead();
    return super.isLoading;
  }

  @override
  set isLoading(bool value) {
    _$isLoadingAtom.reportWrite(value, super.isLoading, () {
      super.isLoading = value;
    });
  }

  late final _$loadWeatherAsyncAction =
      AsyncAction('_WeatherViewModelBase.loadWeather', context: context);

  @override
  Future<void> loadWeather() {
    return _$loadWeatherAsyncAction.run(() => super.loadWeather());
  }

  @override
  String toString() {
    return '''
weather: ${weather},
isLoading: ${isLoading}
    ''';
  }
}
