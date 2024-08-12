using System.ComponentModel.DataAnnotations;

namespace iVertion.WebApi.Models
{
    /// <summary>
    /// Register Person Model
    /// </summary>
    public class RegisterPerson
    {

        /// <summary>
        /// First Name, string, Is Required
        /// </summary>
        [Required]
        public string? FirstName { get; set; }
        /// <summary>
        /// Last Name, string, Is Required
        /// </summary>
        [Required]
        public string? LastName { get; set; }
        /// <summary>
        /// Birthday, Datetime, Is Required
        /// </summary>
        [Required]
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// Profile Picture, string, Is Required
        /// </summary>
        [Required]
        public string? ProfilePicture { get; set; }
    }
}