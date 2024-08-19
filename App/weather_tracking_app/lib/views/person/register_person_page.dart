import 'dart:io';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../viewmodels/person_viewmodel.dart';
import '../../widgets/custom_text_field.dart';

class RegisterPersonPage extends StatelessWidget {
  const RegisterPersonPage({super.key});

  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider(
      create: (_) => PersonViewModel(),
      child: Consumer<PersonViewModel>(
        builder: (context, viewModel, child) {
          return Scaffold(
            backgroundColor: const Color.fromARGB(255, 84, 37, 99),
            appBar: AppBar(
              title: const Text('Complete Your Profile'),
            ),
            body: Padding(
              padding: const EdgeInsets.all(16.0),
              child: Column(
                children: [
                  GestureDetector(
                    onTap: viewModel.pickImage,
                    child: CircleAvatar(
                      radius: 50,
                      backgroundImage: viewModel.image != null
                          ? FileImage(File(viewModel.image!.path))
                          : null,
                      child: viewModel.image == null
                          ? const Icon(Icons.add_a_photo)
                          : null,
                    ),
                  ),
                  const SizedBox(height: 20),
                  CustomTextField(
                    controller: viewModel.firstNameController,
                    label: 'First Name',
                  ),
                  const SizedBox(height: 20),
                  CustomTextField(
                    controller: viewModel.lastNameController,
                    label: 'Last Name',
                  ),
                  const SizedBox(height: 20),
                  GestureDetector(
                    onTap: () => viewModel.selectDate(context),
                    child: AbsorbPointer(
                      child: CustomTextField(
                        controller: viewModel.birthdayController,
                        label: 'Birthday',
                      ),
                    ),
                  ),
                  const SizedBox(height: 20),
                      ElevatedButton(
                        onPressed: viewModel.registerPerson,
                        style: ElevatedButton.styleFrom(
                          backgroundColor: const Color.fromARGB(255, 40, 89, 163),
                          padding: const EdgeInsets.symmetric(horizontal: 50, vertical: 15),
                        ),
                        child: const Text(
                          'Register',
                          style: TextStyle(
                            fontSize: 18,
                            fontWeight: FontWeight.bold,
                            color: Colors.white,
                            shadows: [
                              Shadow(
                                offset: Offset(2.0, 2.0),
                                blurRadius: 3.0,
                                color: Colors.black,
                              ),
                            ],
                          ),
                        ),
                      ),
                ],
              ),
            ),
          );
        },
      ),
    );
  }
}
