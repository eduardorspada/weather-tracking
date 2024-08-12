using Microsoft.AspNetCore.Identity;

namespace iVertion.Infra.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsEnabled { get; set; }
        public int UserProfileId { get; set; }
        public int PersonId { get; set; }

    }
}