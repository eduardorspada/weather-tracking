
using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public class City : Entity
    {
        public string? Name { get; private set; }
        public IEnumerable<Address>? Addresses { get; set; }
        public IEnumerable<WeatherCondition>? WeatherConditions { get; set; }
        public IEnumerable<WeatherForecast>? WeatherForecasts { get; set; }
        public IEnumerable<WeatherAlert>? WeatherAlerts { get; set; }


        public City(string name,
                    bool active)
        {
            ValidationDomain(name);
            Active = active;            
        }
        public City(int id,
                    string name,
                    bool active)
        {
            DomainExceptionValidation.When(id < 0,
                                           "Invalid Id, must greater than zero.");
            ValidationDomain(name);
            Id      = id;
            Active  = active;            
        }
        public void Update(string name,
                           bool active)
        {
            ValidationDomain(name);
            Active = active;            
        }
        private void ValidationDomain(string name)
        {
            DomainExceptionValidation.When(String.IsNullOrEmpty(name),
                                           "Name cannot be null or empty.");
            DomainExceptionValidation.When(name?.Length < 2,
                                           "Name must have at least 2 characters.");
            DomainExceptionValidation.When(name?.Length > 150,
                                           "Name must have a maximum of 150 characters.");
            Name        = name;
        }
    }
}
