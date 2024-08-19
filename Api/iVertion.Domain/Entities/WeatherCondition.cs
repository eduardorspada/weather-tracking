using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public sealed class WeatherCondition : Entity
    {
        public string? CityName { get; private set; }
        public string? Description { get; private set; }
        public double Temperature { get; private set; }
        public double MaxTemperature { get; private set; }
        public double MinTemperature { get; private set; }
        public double ThermalSensation { get; private set; }
        public int Humidity { get; private set; }
        public double WindSpeed { get; private set; }
        public string? WindDirection { get; private set; }
        public int UvIndex { get; private set; }
        public double PollenCount { get; private set; }
        public double DustLevel { get; private set; }
        public DateTime? Sunrise { get; private set; }
        public DateTime? Sunset { get; private set; }

        public int CityId { get; private set; }
        public City? City { get; set; }

        public WeatherCondition(string? cityName,
                                string? description,
                                double temperature,
                                double maxTemperature,
                                double minTemperature,
                                double thermalSensation,
                                int humidity,
                                double windSpeed,
                                string? windDirection,
                                int uvIndex,
                                double pollenCount,
                                double dustLevel,
                                DateTime? sunrise,
                                DateTime? sunset,
                                int cityId)
        {

            ValidationDomain(cityName,
                             description,
                             cityId);
            Temperature = temperature;
            MaxTemperature = maxTemperature;
            MinTemperature = minTemperature;
            ThermalSensation = thermalSensation;
            Humidity = humidity;
            WindSpeed = windSpeed;
            WindDirection = windDirection;
            UvIndex = uvIndex;
            PollenCount = pollenCount;
            DustLevel = dustLevel;
            Sunrise = sunrise;
            Sunset = sunset;
            Active = true;
        }

        public WeatherCondition(int id,
                                string? cityName,
                                string? description,
                                double temperature,
                                double maxTemperature,
                                double minTemperature,
                                double thermalSensation,
                                int humidity,
                                double windSpeed,
                                string? windDirection,
                                int uvIndex,
                                double pollenCount,
                                double dustLevel,
                                DateTime? sunrise,
                                DateTime? sunset,
                                int cityId)
        {
            DomainExceptionValidation.When(id < 0,
                                           "Invalid Id, must greater than zero.");
            ValidationDomain(cityName,
                             description,
                             cityId);
            Id = id;
            Temperature = temperature;
            MaxTemperature = maxTemperature;
            MinTemperature = minTemperature;
            ThermalSensation = thermalSensation;
            Humidity = humidity;
            WindSpeed = windSpeed;
            WindDirection = windDirection;
            UvIndex = uvIndex;
            PollenCount = pollenCount;
            DustLevel = dustLevel;
            Sunrise = sunrise;
            Sunset = sunset;
            Active = true;
        }

        public void Update(string? cityName,
                           string? description,
                           double temperature,
                           double maxTemperature,
                           double minTemperature,
                           double thermalSensation,
                           int humidity,
                           double windSpeed,
                           string? windDirection,
                           int uvIndex,
                           double pollenCount,
                           double dustLevel,
                           DateTime? sunrise,
                           DateTime? sunset,
                           int cityId,
                           bool active)
        {
            ValidationDomain(cityName,
                             description,
                             cityId);
            Temperature = temperature;
            MaxTemperature = maxTemperature;
            MinTemperature = minTemperature;
            ThermalSensation = thermalSensation;
            Humidity = humidity;
            WindSpeed = windSpeed;
            WindDirection = windDirection;
            UvIndex = uvIndex;
            PollenCount = pollenCount;
            DustLevel = dustLevel;
            Sunrise = sunrise;
            Sunset = sunset;
            Active = active;
        }

        private void ValidationDomain(string? cityName,
                                      string? description,
                                      int cityId)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(cityName),
                "Invalid City Name, must not be null or empty.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(description),
                "Invalid Description, must not be null or empty.");
            DomainExceptionValidation.When(cityId < 0,
                                           "Invalid City Id, must greater than zero.");
            CityName = cityName;
            Description = description;
            CityId = cityId;
        }
    }
}
