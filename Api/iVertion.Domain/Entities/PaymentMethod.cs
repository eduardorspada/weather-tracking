
using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public sealed class PaymentMethod : Entity
    {
        public string? Name { get; private set; }

        public PaymentMethod(string name,
                             bool active)
        {
            ValidationDomain(name);
            Active  = active;
        }
        public PaymentMethod(int id,
                             string name,
                             bool active)
        {
            DomainExceptionValidation.When(id < 0,
                                           "Invalid Id, must greater than zero.");
            ValidationDomain(name);
            Active  = active;
            Id      = id;
        }
        public void Update(string name,
                           bool active)
        {
            ValidationDomain(name);
            Active  = active;
        }

        private void ValidationDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                                           "Invalid Name, must not be null or empty.");
            DomainExceptionValidation.When(name.Length < 2,
                                           "Invalid Name, too short, must be 2 character.");
            DomainExceptionValidation.When(name.Length > 25,
                                           "Invalid Name, too long, must be 25 character.");
            Name    = name;
        }
    }
}