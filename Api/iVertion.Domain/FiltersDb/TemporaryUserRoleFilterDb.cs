
using iVertion.Domain.Interfaces;

namespace iVertion.Domain.FiltersDb
{
    public class TemporaryUserRoleFilterDb: PagedBaseRequest
    {
        public string? Role { get; set; }
        public string? TargetUserId { get; set; }
        public string? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}