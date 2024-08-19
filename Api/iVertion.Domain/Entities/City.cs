
using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public class City : Entity
    {
        public string? Name { get; private set; }
        public int Code { get; private set; }
        public IEnumerable<Address>? Addresses { get; set; }
        public IEnumerable<WeatherCondition>? WeatherConditions { get; set; }
        public IEnumerable<WeatherForecast>? WeatherForecasts { get; set; }
        public IEnumerable<WeatherAlert>? WeatherAlerts { get; set; }


        public City(string name,
                    int code,
                    bool active)
        {
            ValidationDomain(name,
                             code);
            Active = active;            
        }
        public City(int id,
                    string name,
                    int code,
                    bool active)
        {
            DomainExceptionValidation.When(id <= 0,
                                           "Invalid Id, must greater than zero.");
            ValidationDomain(name,
                             code);
            Id      = id;
            Active  = active;            
        }
        public void Update(string name,
                           int code,
                           bool active)
        {
            ValidationDomain(name,
                             code);
            Active = active;            
        }
        private void ValidationDomain(string name,
                                      int code)
        {
            DomainExceptionValidation.When(String.IsNullOrEmpty(name),
                                           "Name cannot be null or empty.");
            DomainExceptionValidation.When(name?.Length < 2,
                                           "Name must have at least 2 characters.");
            DomainExceptionValidation.When(name?.Length > 150,
                                           "Name must have a maximum of 150 characters.");
            DomainExceptionValidation.When(code <= 0,
                                           "Invalid Code, must greater than zero.");
            Name        = name;
            Code        = code;
        }
    }
}
