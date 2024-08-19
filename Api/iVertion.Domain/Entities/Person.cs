using System.Runtime.Serialization;
using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public sealed class Person : Entity
    {
        // Obrigatórios
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }

        [IgnoreDataMember]
        public string FullName => $"{FirstName} {LastName}";

        public DateTime? Birthday { get; private set; }

        // Opcionais
        public string? ProfilePicture { get; private set; }


        public ICollection<PersonAddress> PersonAddresses { get; set; }
        public ICollection<Device>? Devices { get; set; }

        // Construtores
        public Person(string firstName, string lastName, DateTime? birthday, string? profilePicture)
        {
            ValidateDomain(firstName, lastName, birthday);
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            ProfilePicture = profilePicture;
            Active = true;
        }

        public Person(int id, string firstName, string lastName, DateTime? birthday, string? profilePicture)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id, must greater than zero.");
            ValidateDomain(firstName, lastName, birthday);
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            ProfilePicture = profilePicture;
            Active = true;
        }

        // Métodos de atualização
        public void Update(string firstName, string lastName, DateTime? birthday, string? profilePicture)
        {
            ValidateDomain(firstName, lastName, birthday);
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            ProfilePicture = profilePicture;
        }

        // Validações de domínio
        private void ValidateDomain(string firstName, string lastName, DateTime? birthday)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(firstName), "First Name is required.");
            DomainExceptionValidation.When(firstName.Length < 2, "First Name must be at least 2 characters long.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(lastName), "Last Name is required.");
            DomainExceptionValidation.When(lastName.Length < 2, "Last Name must be at least 2 characters long.");
            DomainExceptionValidation.When(birthday == null || birthday > DateTime.UtcNow, "Invalid Birthday.");
        }
    }
}
