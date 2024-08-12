using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public sealed class WeatherForecast : Entity
    {
        public DateTime Date { get; private set; }
        public string? Weekday { get; private set; }
        public int MaxTemperature { get; private set; }
        public int MinTemperature { get; private set; }
        public int Humidity { get; private set; }
        public int Cloudiness { get; private set; }
        public double RainProbability { get; private set; }
        public string? Description { get; private set; }
        public string? ConditionSlug { get; private set; }

        public WeatherForecast(
            DateTime date,
            string weekday,
            int maxTemperature,
            int minTemperature,
            int humidity,
            int cloudiness,
            double rainProbability,
            string description,
            string conditionSlug,
            bool active)
        {
            ValidationDomain(description, conditionSlug);
            Date = date;
            Weekday = weekday;
            MaxTemperature = maxTemperature;
            MinTemperature = minTemperature;
            Humidity = humidity;
            Cloudiness = cloudiness;
            RainProbability = rainProbability;
            Description = description;
            ConditionSlug = conditionSlug;
            Active = active;
        }

        public void Update(
            DateTime date,
            string weekday,
            int maxTemperature,
            int minTemperature,
            int humidity,
            int cloudiness,
            double rainProbability,
            string description,
            string conditionSlug,
            bool active)
        {
            ValidationDomain(description, conditionSlug);
            Date = date;
            Weekday = weekday;
            MaxTemperature = maxTemperature;
            MinTemperature = minTemperature;
            Humidity = humidity;
            Cloudiness = cloudiness;
            RainProbability = rainProbability;
            Description = description;
            ConditionSlug = conditionSlug;
            Active = active;
        }

        private void ValidationDomain(string description, string conditionSlug)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(description),
                "Invalid Description, must not be null or empty.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(conditionSlug),
                "Invalid Condition Slug, must not be null or empty.");
        }
    }
}
