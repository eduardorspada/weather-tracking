
using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public class PersonPhoneNumber : Entity
    {
        public int PersonId { get; private set; }
        public IEnumerable<Person>? Persons { get; set; }
        public int PhoneNumberId { get; private set; }
        public IEnumerable<PhoneNumber>? PhoneNumberes { get; set; }

        public PersonPhoneNumber(int personId,
                             int phoneNumberId,
                             bool active)
        {
            ValidationDomain(personId,
                             phoneNumberId);
            Active  = active;
        }
        public PersonPhoneNumber(int id,
                             int personId,
                             int phoneNumberId,
                             bool active)
        {
            DomainExceptionValidation.When(id < 0,
                                           "Invalid Id, must greater than zero.");
            ValidationDomain(personId,
                             phoneNumberId);
            Id      = id;
            Active  = active;
        }
        public void Update(int personId,
                           int phoneNumberId,
                           bool active)
        {
            ValidationDomain(personId,
                             phoneNumberId);
            Active  = active;
        }

        private void ValidationDomain(int personId,
                                      int phoneNumberId)
        {
            DomainExceptionValidation.When(personId <= 0,
                                           "Invalid Person Id, must greater than zero.");
            DomainExceptionValidation.When(phoneNumberId <= 0,
                                           "Invalid PhoneNumber Id, must greater than zero.");
            PersonId  = personId;
            PhoneNumberId   = phoneNumberId;
        }
    }
}