using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public sealed class Device : Entity
    {
        public string? Token { get; private set; }
        public string? DeviceName { get; private set; }
        public bool AcceptNotifications { get; private set; }
        public int PersonId { get; private set; }
        public Person? Person { get; set; }
        public ICollection<WeatherNotification>? WeatherNotifications { get; set; }

        public Device(string token,
                      string deviceName,
                      bool acceptNotifications,
                      int personId,
                      bool active)
        {
            ValidationDomain(token,
                             deviceName,
                             personId);
            AcceptNotifications = acceptNotifications;
            Active = active;
            
        }

        public Device(int id,
                      string token,
                      string deviceName,
                      bool acceptNotifications,
                      int personId,
                      bool active)
        {
            DomainExceptionValidation.When(id < 0,
                                           "Invalid Id, must greater than zero.");
            ValidationDomain(token,
                             deviceName,
                             personId);
            Id = id;
            AcceptNotifications = acceptNotifications;
            Active = active;

        }
        public void Update(string token,
                           string deviceName,
                           bool acceptNotifications,
                           int personId,
                           bool active)
        {
            ValidationDomain(token,
                             deviceName,
                             personId);
            AcceptNotifications = acceptNotifications;
            Active = active;

        }

        private void ValidationDomain(string token,
                                      string deviceName,
                                      int personId)
        {
            DomainExceptionValidation.When(String.IsNullOrEmpty(token),
                                           "Invalid Token, must not be null or empty.");
            DomainExceptionValidation.When(String.IsNullOrEmpty(deviceName),
                                           "Invalid Device Name, must not be null or empty.");
            DomainExceptionValidation.When(personId <= 0,
                                           "Invalid Person Id, must greater than zero.");
            Token = token;
            DeviceName = deviceName;
            PersonId = personId;
        }

    }
}
