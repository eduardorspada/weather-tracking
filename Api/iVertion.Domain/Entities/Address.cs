
using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public class Address : Entity
    {
        public int CityId { get; private set; }
        public City? City { get; set; }
        public int StateId { get; private set; }
        public State? State { get; set; }
        public int CountryId { get; private set; }
        public Country? Country { get; set; }

        public ICollection<PersonAddress> PersonAddresses { get; set; }


        public Address(int cityId,
                       int stateId,
                       int countryId,
                       bool active)
        {
            ValidationDomain(cityId,
                             stateId,
                             countryId);
            Active  = active;
        }
        public Address(int id,
                       int cityId,
                       int stateId,
                       int countryId,
                       bool active)
        {
            DomainExceptionValidation.When(id < 0,
                                           "Invalid Id, must be up to zero.");
            ValidationDomain(cityId,
                             stateId,
                             countryId);
            Id      = id;                             
            Active  = active;
        }
        public void Update(int cityId,
                           int stateId,
                           int countryId,
                           bool active)
        {
            ValidationDomain(cityId,
                             stateId,
                             countryId);
            Active  = active;
        }

        private void ValidationDomain(int cityId,
                                      int stateId,
                                      int countryId)
        {

            DomainExceptionValidation.When(cityId < 0,
                                           "Invalid City Id, must be up to zero.");
            DomainExceptionValidation.When(stateId < 0,
                                           "Invalid State Id, must be up to zero.");
            DomainExceptionValidation.When(countryId < 0,
                                           "Invalid Country Id, must be up to zero.");

            CityId  = cityId;
            StateId  = stateId;
            CountryId  = countryId;
        }
    }
}