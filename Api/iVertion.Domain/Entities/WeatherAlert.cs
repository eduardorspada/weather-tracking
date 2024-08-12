using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public sealed class WeatherAlert : Entity
    {
        public int SeverityLevel { get; private set; }
        public string? Message { get; private set; }
        public DateTime AlertTime { get; private set; }

        public WeatherAlert(
            int severityLevel, 
            string message, 
            DateTime alertTime, 
            bool active)
        {
            ValidationDomain(message);
            SeverityLevel = severityLevel;
            Message = message;
            AlertTime = alertTime;
            Active = active;
        }

        public void Update(
            int severityLevel,
            string message,
            DateTime alertTime,
            bool active)
        {
            ValidationDomain(message);
            SeverityLevel = severityLevel;
            Message = message;
            AlertTime = alertTime;
            Active = active;
        }

        private void ValidationDomain(string message)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(message),
                "Invalid Message, must not be null or empty.");
        }
    }
}
