
using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public sealed class Country : Entity
    {
        public string? Name { get; private set; }
        public string? Acronym { get; private set; }
        public IEnumerable<Address>? Addresses { get; set; }

        public Country(string name,
                       string acronym,
                       bool active)
        {
            ValidationDomain(name,
                             acronym);
            Active = active;            
        }
        public Country(int id,
                       string name,
                       string acronym,
                       bool active)
        {
            DomainExceptionValidation.When(id < 0,
                                           "Invalid Id, must greater than zero.");
            ValidationDomain(name,
                             acronym);
            Id      = id;
            Active  = active;            
        }
        public void Update(string name,
                           string acronym,
                           bool active)
        {
            ValidationDomain(name,
                             acronym);
            Active = active;            
        }
        private void ValidationDomain(string name,
                                      string acronym)
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
            Name    = name;
            Acronym = acronym;
        }
    }
}
