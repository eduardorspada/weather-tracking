
namespace iVertion.Domain.Entities
{
    public class PhoneNumber : Entity
    {
        public string? Number { get; private set; }
        public IEnumerable<PersonPhoneNumber>? PersonPhoneNumbers { get; set; }
    }
}