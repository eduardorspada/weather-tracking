
using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public class Neighborhood : Entity
    {
        public string? Name { get; private set; }
        public int Code { get; private set; }
        public int CityId { get; private set; }
        public City? City { get; set; }
        public IEnumerable<Address>? Addresses { get; set; }

        public Neighborhood(string name,
                            int code,
                            int cityId,
                            bool active)
        {
            ValidationDomain(name,
                             code,
                             cityId);
            Active = active;            
        }
        public Neighborhood(int id,
                            string name,
                            int code,
                            int cityId,
                            bool active)
        {
            DomainExceptionValidation.When(id <= 0,
                                           "Invalid Id, must be up to zero.");
            ValidationDomain(name,
                             code,
                             cityId);
            Id      = id;
            Active  = active;            
        }
        public void Update(string name,
                           int code,
                           int cityId,
                           bool active)
        {
            ValidationDomain(name,
                             code,
                             cityId);
            Active = active;            
        }
        private void ValidationDomain(string name,
                                      int code,
                                      int cityId)
        {
            DomainExceptionValidation.When(String.IsNullOrEmpty(name),
                                           "Name cannot be null or empty.");
            DomainExceptionValidation.When(name?.Length < 2,
                                           "Name must have at least 2 characters.");
            DomainExceptionValidation.When(name?.Length > 150,
                                           "Name must have a maximum of 150 characters.");
            DomainExceptionValidation.When(code <= 0,
                                           "Invalid Code, must be up to zero.");
            DomainExceptionValidation.When(cityId <= 0,
                                           "Invalid City Id, must be up to zero.");
            Name        = name;
            Code        = code;
            CityId      = cityId;
        }
    }
}