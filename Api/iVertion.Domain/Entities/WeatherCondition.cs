using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public sealed class WeatherCondition : Entity
    {
        public string? CityName { get; private set; }
        public double Temperature { get; private set; }
        public string? Description { get; private set; }
        public string? ConditionSlug { get; private set; }
        public int Humidity { get; private set; }
        public int Cloudiness { get; private set; }
        public double WindSpeed { get; private set; }
        public string? WindDirection { get; private set; }
        public DateTime? Sunrise { get; private set; }
        public DateTime? Sunset { get; private set; }

        public WeatherCondition(
            string cityName, 
            double temperature, 
            string description, 
            string conditionSlug,
            int humidity,
            int cloudiness,
            double windSpeed,
            string windDirection,
            DateTime? sunrise,
            DateTime? sunset,
            bool active)
        {
            ValidationDomain(cityName, description);
            CityName = cityName;
            Temperature = temperature;
            Description = description;
            ConditionSlug = conditionSlug;
            Humidity = humidity;
            Cloudiness = cloudiness;
            WindSpeed = windSpeed;
            WindDirection = windDirection;
            Sunrise = sunrise;
            Sunset = sunset;
            Active = active;
        }

        public void Update(
            string cityName,
            double temperature,
            string description,
            string conditionSlug,
            int humidity,
            int cloudiness,
            double windSpeed,
            string windDirection,
            DateTime? sunrise,
            DateTime? sunset,
            bool active)
        {
            ValidationDomain(cityName, description);
            CityName = cityName;
            Temperature = temperature;
            Description = description;
            ConditionSlug = conditionSlug;
            Humidity = humidity;
            Cloudiness = cloudiness;
            WindSpeed = windSpeed;
            WindDirection = windDirection;
            Sunrise = sunrise;
            Sunset = sunset;
            Active = active;
        }

        private void ValidationDomain(string cityName, string description)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(cityName),
                "Invalid City Name, must not be null or empty.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(description),
                "Invalid Description, must not be null or empty.");
        }
    }
}
