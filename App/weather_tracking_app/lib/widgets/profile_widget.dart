import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'dart:convert';
import 'package:weather_tracking_app/viewmodels/person_viewmodel.dart';

class ProfileWidget extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    // Obtendo o PersonViewModel via Modular
    final viewModel = Modular.get<PersonViewModel>();

    final person = viewModel.person;

    if (person == null) {
      return CircularProgressIndicator(); // ou algum placeholder
    }

    return Card(
      color: const Color.fromARGB(255, 151, 112, 173),
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          children: [
            CircleAvatar(
              radius: 40,
              backgroundImage: MemoryImage(base64Decode(person.profilePicture)),
            ),
            SizedBox(height: 10),
            Text(
              '${person.firstName} ${person.lastName}',
              style: TextStyle(
                color: Colors.white,
                fontSize: 20,
                fontWeight: FontWeight.bold,
              ),
            ),
            SizedBox(height: 5),
            Text(
              'Birthday: ${person.birthday.day}/${person.birthday.month}/${person.birthday.year}',
              style: TextStyle(
                fontSize: 16,
                color: Colors.white,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
