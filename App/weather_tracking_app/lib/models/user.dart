class User {
  final String email;
  final String token;
  final DateTime expiration;

  User({
    required this.email,
    required this.token,
    required this.expiration,
  });

  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      email: json['email'],
      token: json['token'],
      expiration: DateTime.parse(json['expiration']),
    );
  }
}
