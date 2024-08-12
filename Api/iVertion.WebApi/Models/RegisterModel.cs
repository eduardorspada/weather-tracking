using System.ComponentModel.DataAnnotations;

namespace iVertion.WebApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string? ConfirmPassword { get; set;}
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int UserProfileId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int PersonId { get; set; }
    }
}