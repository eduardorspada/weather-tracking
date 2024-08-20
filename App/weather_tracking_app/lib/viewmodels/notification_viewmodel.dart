import 'package:flutter/material.dart';
import '../services/notification_service.dart';

class NotificationViewModel extends ChangeNotifier {
  final NotificationService _notificationService;

  NotificationViewModel(this._notificationService);

  Future<void> initializeNotifications() async {
    await _notificationService.initialize();
    notifyListeners();
  }

  Future<void> registerDeviceToken(String token, String deviceName) async {
    await _notificationService.registerDeviceToken(token, deviceName);
  }
}
