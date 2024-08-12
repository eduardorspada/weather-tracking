
using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public sealed class TemporaryUserRole : Entity
    {
        public string? Role { get; private set; }
        public string? TargetUserId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }

        public TemporaryUserRole(string role,
                                 string targetUserId, 
                                 DateTime startDate, 
                                 DateTime expirationDate,
                                 bool active)
        {
            ValidationDomain(role, 
                             targetUserId, 
                             startDate, 
                             expirationDate);
            Active = active;
        }
        public TemporaryUserRole(int id,
                                 string role,
                                 string targetUserId, 
                                 DateTime startDate, 
                                 DateTime expirationDate,
                                 bool active)
        {
            DomainExceptionValidation.When(id < 0,
                                           "Invalid Id, must be up to zero."); 
            ValidationDomain(role, 
                             targetUserId, 
                             startDate, 
                             expirationDate);
            Id = id;
            Active = active;
        }
        public void Update(string role,
                           string targetUserId, 
                           DateTime startDate, 
                           DateTime expirationDate,
                           bool active)
        {
            ValidationDomain(role, 
                             targetUserId, 
                             startDate, 
                             expirationDate);
            Active = active;
        }

        private void ValidationDomain(string role,
                                      string targetUserId,
                                      DateTime startDate,
                                      DateTime expirationDate)
        {
            DomainExceptionValidation.When(String.IsNullOrEmpty(role),
                                           "Invalid Role, must not be null or empty.");
            DomainExceptionValidation.When(role.Length < 5,
                                           "Invalid Role, too short, must be at least 5 characters long.");
            DomainExceptionValidation.When(role.Length > 25,
                                           "Invalid Role, too long, max 25 characters.");
            DomainExceptionValidation.When(String.IsNullOrEmpty(targetUserId),
                                           "Invalid Target User Id, must not be null or empty.");
            DomainExceptionValidation.When(expirationDate < startDate,
                                           "Invalid Expiration Date, must be greater than the Start Date.");

            Role = role;
            TargetUserId = targetUserId;
            StartDate = startDate;
            ExpirationDate = expirationDate;
            
        }
    }
}