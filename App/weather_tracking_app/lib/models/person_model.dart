class PersonModel {
  final String firstName;
  final String lastName;
  final DateTime birthday;
  final String profilePicture;

  PersonModel({
    required this.firstName,
    required this.lastName,
    required this.birthday,
    required this.profilePicture,
  });

  factory PersonModel.fromJson(Map<String, dynamic> json) {
    return PersonModel(
      firstName: json['data']['firstName'],
      lastName: json['data']['lastName'],
      birthday: DateTime.parse(json['data']['birthday']),
      profilePicture: json['data']['profilePicture'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'firstName': firstName,
      'lastName': lastName,
      'birthday': birthday.toIso8601String(),
      'profilePicture': profilePicture,
    };
  }
}
