using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public sealed class WeatherForecast : Entity
    {
        public string? CityName { get; private set; }
        public DateTime Date { get; private set; }
        public string? Weekday { get; private set; }
        public double MaxTemperature { get; private set; }
        public double MinTemperature { get; private set; }
        public int Humidity { get; private set; }
        public double RainProbability { get; private set; }
        public string? Description { get; private set; }

        public int CityId { get; private set; }
        public City? City { get; set; }

        public WeatherForecast(string cityName,
                               DateTime date,
                               string weekday,
                               double maxTemperature,
                               double minTemperature,
                               int humidity,
                               double rainProbability,
                               string description,
                               int cityId,
                               bool active)
        {
            ValidationDomain(cityName,
                             description,
                             cityId);
            Date = date;
            Weekday = weekday;
            MaxTemperature = maxTemperature;
            MinTemperature = minTemperature;
            Humidity = humidity;
            RainProbability = rainProbability;
            Active = active;
        }
        public WeatherForecast(int id,
                               string cityName,
                               DateTime date,
                               string weekday,
                               double maxTemperature,
                               double minTemperature,
                               int humidity,
                               double rainProbability,
                               string description,
                               int cityId,
                               bool active)
        {
            DomainExceptionValidation.When(id < 0,
                                           "Invalid Id, must greater than zero.");
            ValidationDomain(cityName,
                             description,
                             cityId);
            Id = id;
            Date = date;
            Weekday = weekday;
            MaxTemperature = maxTemperature;
            MinTemperature = minTemperature;
            Humidity = humidity;
            RainProbability = rainProbability;
            Active = active;
        }

        public void Update(string cityName,
                           DateTime date,
                           string weekday,
                           double maxTemperature,
                           double minTemperature,
                           int humidity,
                           double rainProbability,
                           string description,
                           int cityId,
                           bool active)
        {
            ValidationDomain(cityName,
                             description,
                             cityId);
            Date = date;
            Weekday = weekday;
            MaxTemperature = maxTemperature;
            MinTemperature = minTemperature;
            Humidity = humidity;
            RainProbability = rainProbability;
            Active = active;
        }

        private void ValidationDomain(string cityName,
                                      string description,
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
