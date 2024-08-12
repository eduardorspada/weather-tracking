
using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public class PersonAddress : Entity
    {
        public int PersonId { get; private set; }
        public IEnumerable<Person>? Persons { get; set; }
        public int AddressId { get; private set; }
        public IEnumerable<Address>? Addresses { get; set; }

        public PersonAddress(int personId,
                             int addressId,
                             bool active)
        {
            ValidationDomain(personId,
                             addressId);
            Active  = active;
        }
        public PersonAddress(int id,
                             int personId,
                             int addressId,
                             bool active)
        {
            DomainExceptionValidation.When(id < 0,
                                           "Invalid Id, must be up to zero.");
            ValidationDomain(personId,
                             addressId);
            Id      = id;
            Active  = active;
        }
        public void Update(int personId,
                           int addressId,
                           bool active)
        {
            ValidationDomain(personId,
                             addressId);
            Active  = active;
        }

        private void ValidationDomain(int personId,
                                      int addressId)
        {
            DomainExceptionValidation.When(personId <= 0,
                                           "Invalid Person Id, must be up to zero.");
            DomainExceptionValidation.When(addressId <= 0,
                                           "Invalid Address Id, must be up to zero.");
            PersonId  = personId;
            AddressId   = addressId;
        }
    }
}