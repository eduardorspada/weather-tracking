
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iVertion.Application.DTOs
{
    public class TemporaryUserRoleDTO : BaseDTO
    {
        [Required(ErrorMessage = "Role is required.")]
        [MinLength(5)]
        [MaxLength(25)]
        [DisplayName("Role")]
        public string? Role { get; set; }

        [Required(ErrorMessage = "Target User Id is required.")]
        [DisplayName("Target User Id")]
        public string? TargetUserId { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Expiration Date is required.")]
        [DisplayName("Expiration Date")]
        public DateTime ExpirationDate { get; set; }
    }
}