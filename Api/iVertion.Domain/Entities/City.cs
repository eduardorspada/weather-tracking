
using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public class City : Entity
    {
        public string? Name { get; private set; }
        public int Code { get; private set; }
        public int StateId { get; private set; }
        public State? State { get; set; }
        public IEnumerable<Neighborhood>? Neighborhoods { get; set; }
        public IEnumerable<Address>? Addresses { get; set; }

        public City(string name,
                    int code,
                    int stateId,
                    bool active)
        {
            ValidationDomain(name,
                             code,
                             stateId);
            Active = active;            
        }
        public City(int id,
                    string name,
                    int code,
                    int stateId,
                    bool active)
        {
            DomainExceptionValidation.When(id <= 0,
                                           "Invalid Id, must be up to zero.");
            ValidationDomain(name,
                             code,
                             stateId);
            Id      = id;
            Active  = active;            
        }
        public void Update(string name,
                           int code,
                           int stateId,
                           bool active)
        {
            ValidationDomain(name,
                             code,
                             stateId);
            Active = active;            
        }
        private void ValidationDomain(string name,
                                      int code,
                                      int stateId)
        {
            DomainExceptionValidation.When(String.IsNullOrEmpty(name),
                                           "Name cannot be null or empty.");
            DomainExceptionValidation.When(name?.Length < 2,
                                           "Name must have at least 2 characters.");
            DomainExceptionValidation.When(name?.Length > 150,
                                           "Name must have a maximum of 150 characters.");
            DomainExceptionValidation.When(code <= 0,
                                           "Invalid Code, must be up to zero.");
            DomainExceptionValidation.When(stateId <= 0,
                                           "Invalid State Id, must be up to zero.");
            Name        = name;
            Code        = code;
            StateId     = stateId;
        }
    }
}