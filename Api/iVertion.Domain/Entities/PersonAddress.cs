
using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public class PersonAddress : Entity
    {
        public int PersonId { get; private set; }
        public Person Person { get; set; }
        public int AddressId { get; private set; }
        public Address Address { get; set; }

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
                                           "Invalid Id, must greater than zero.");
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
                                           "Invalid Person Id, must greater than zero.");
            DomainExceptionValidation.When(addressId <= 0,
                                           "Invalid Address Id, must greater than zero.");
            PersonId  = personId;
            AddressId   = addressId;
        }
    }
}
