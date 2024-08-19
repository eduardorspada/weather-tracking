class UserModel {
  final bool isEnabled;
  final int userProfileId;
  final int personId;
  final String id;
  final String userName;
  final String email;
  final bool emailConfirmed;
  final String? phoneNumber;
  final bool phoneNumberConfirmed;
  final bool twoFactorEnabled;
  final bool lockoutEnabled;
  final int accessFailedCount;

  UserModel({
    required this.isEnabled,
    required this.userProfileId,
    required this.personId,
    required this.id,
    required this.userName,
    required this.email,
    required this.emailConfirmed,
    this.phoneNumber,
    required this.phoneNumberConfirmed,
    required this.twoFactorEnabled,
    required this.lockoutEnabled,
    required this.accessFailedCount,
  });

  factory UserModel.fromJson(Map<String, dynamic> json) {
    return UserModel(
      isEnabled: json['isEnabled'],
      userProfileId: json['userProfileId'],
      personId: json['personId'],
      id: json['id'],
      userName: json['userName'],
      email: json['email'],
      emailConfirmed: json['emailConfirmed'],
      phoneNumber: json['phoneNumber'],
      phoneNumberConfirmed: json['phoneNumberConfirmed'],
      twoFactorEnabled: json['twoFactorEnabled'],
      lockoutEnabled: json['lockoutEnabled'],
      accessFailedCount: json['accessFailedCount'],
    );
  }
}
