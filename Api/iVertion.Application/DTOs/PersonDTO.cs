
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace iVertion.Application.DTOs
{
    public class PersonDTO : BaseDTO
    {
        [Required(ErrorMessage = "First Name is required.")]
        [MinLength(2)]
        [DisplayName("First Name")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [MinLength(2)]
        [DisplayName("Last Name")]
        public string? LastName { get; set; }
        [IgnoreDataMember]
        [DisplayName("Full Name")]
        public string? FullName => $"{FirstName} {LastName}";
        [Required(ErrorMessage = "Birthday is required.")]
        [DisplayName("Birthday")]
        public DateTime Birthday { get; set; }
        [DisplayName("Profile Picture")]
        public string? ProfilePicture { get; set; }
    }
}