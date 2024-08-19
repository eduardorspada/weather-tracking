import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:image_picker/image_picker.dart';
import 'package:weather_tracking_app/models/person_model.dart';
import '../../services/network_service.dart';

class PersonViewModel extends ChangeNotifier {
  final NetworkService _networkService = NetworkService();

  final TextEditingController firstNameController = TextEditingController();
  final TextEditingController lastNameController = TextEditingController();
  final TextEditingController birthdayController = TextEditingController();
  PersonModel? person;
  XFile? image;
  DateTime? selectedDate;

  Future<void> pickImage() async {
    final pickedFile = await ImagePicker().pickImage(source: ImageSource.gallery);
    if (pickedFile != null) {
      image = pickedFile;
      notifyListeners();
    }
  }

  Future<void> selectDate(BuildContext context) async {
    final DateTime? picked = await showDatePicker(
      context: context,
      initialDate: selectedDate ?? DateTime.now(),
      firstDate: DateTime(1900),
      lastDate: DateTime.now(),
    );
    if (picked != null && picked != selectedDate) {
      selectedDate = picked;
      birthdayController.text = "${picked.day}/${picked.month}/${picked.year}";
      notifyListeners();
    }
  }

  Future<void> registerPerson() async {
    try {
      // Fazer upload da imagem e cadastrar o Person
      await _networkService.registerPerson(
        firstNameController.text,
        lastNameController.text,
        selectedDate?.toIso8601String(),
        image,
      );
      Modular.to.pushReplacementNamed('/home');
    } catch (e) {
      print('Erro ao cadastrar pessoa: ${e.toString()}');
    }
  }
  PersonViewModel() {
    loadPersonInformation();
  }

  Future<void> loadPersonInformation() async {
    try {
      person = await _networkService.MyPersonInformation();
      notifyListeners();
    } catch (e) {
      print('Failed to load person information: ${e.toString()}');
    }
  }
}
