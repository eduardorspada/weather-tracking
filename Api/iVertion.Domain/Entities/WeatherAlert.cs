using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public sealed class WeatherAlert : Entity
    {
        public string? CityName { get; private set; }
        public int SeverityLevel { get; private set; }
        public string? Message { get; private set; }
        public DateTime AlertTime { get; private set; }

        public int CityId { get; private set; }
        public City? City { get; set; }

        public ICollection<WeatherNotification>? WeatherNotifications { get; set; }


        public WeatherAlert(string cityName,
                            int severityLevel,
                            string message,
                            DateTime alertTime,
                            int cityId,
                            bool active)
        {
            ValidationDomain(cityName,
                             message,
                             cityId);
            SeverityLevel = severityLevel;
            AlertTime = alertTime;
            Active = active;
        }
        public WeatherAlert(int id,
                            string cityName,
                            int severityLevel,
                            string message,
                            DateTime alertTime,
                            int cityId,
                            bool active)
        {
            DomainExceptionValidation.When(id < 0,
                                           "Invalid Id, must greater than zero.");
            ValidationDomain(cityName,
                             message,
                             cityId);
            Id = id;
            SeverityLevel = severityLevel;
            AlertTime = alertTime;
            Active = active;
        }

        public void Update(string cityName,
                           int severityLevel,
                           string message,
                           DateTime alertTime,
                           int cityId,
                           bool active)
        {
            ValidationDomain(cityName,
                             message,
                             cityId);
            SeverityLevel = severityLevel;
            AlertTime = alertTime;
            Active = active;
        }

        private void ValidationDomain(string cityName,
                                      string message,
                                      int cityId)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(cityName),
                "Invalid City Name, must not be null or empty.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(message),
                "Invalid Message, must not be null or empty.");
            DomainExceptionValidation.When(cityId < 0,
                                           "Invalid City Id, must greater than zero.");
            CityName = cityName;
            Message = message;
            CityId = cityId;
        }
    }
}
