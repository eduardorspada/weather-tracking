
using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public class State : Entity
    {
        public string? Name { get; private set; }
        public string? Acronym { get; private set; }
        public int Code { get; private set; }
        public int CountryId { get; private set; }
        public Country? Country { get; set; }
        public IEnumerable<City>? Cities { get; set; }
        public IEnumerable<Address>? Addresses { get; set; }

        public State(string name,
                     string acronym,
                     int code,
                     int countryId,
                     bool active)
        {
            ValidationDomain(name,
                             acronym,
                             code,
                             countryId);
            Active = active;            
        }
        public State(int id,
                     string name,
                     string acronym,
                     int code,
                     int countryId,
                     bool active)
        {
            DomainExceptionValidation.When(id <= 0,
                                           "Invalid Id, must be up to zero.");
            ValidationDomain(name,
                             acronym,
                             code,
                             countryId);
            Id      = id;
            Active  = active;            
        }
        public void Update(string name,
                           string acronym,
                           int code,
                           int countryId,
                           bool active)
        {
            ValidationDomain(name,
                             acronym,
                             code,
                             countryId);
            Active = active;            
        }
        private void ValidationDomain(string name,
                                      string acronym,
                                      int code,
                                      int countryId)
        {
            DomainExceptionValidation.When(String.IsNullOrEmpty(name),
                                           "Name cannot be null or empty.");
            DomainExceptionValidation.When(name?.Length < 2,
                                           "Name must have at least 2 characters.");
            DomainExceptionValidation.When(name?.Length > 150,
                                           "Name must have a maximum of 150 characters.");
            DomainExceptionValidation.When(String.IsNullOrEmpty(acronym),
                                           "Acronym cannot be null or empty.");
            DomainExceptionValidation.When(acronym?.Length < 2,
                                           "Acronym must have at least 2 characters.");
            DomainExceptionValidation.When(acronym?.Length > 5,
                                           "Acronym must have a maximum of 5 characters.");
            DomainExceptionValidation.When(code <= 0,
                                           "Invalid Code, must be up to zero.");
            DomainExceptionValidation.When(countryId <= 0,
                                           "Invalid Country Id, must be up to zero.");
            Name        = name;
            Acronym     = acronym;
            Code        = code;
            CountryId   = countryId;
        }
    }
}