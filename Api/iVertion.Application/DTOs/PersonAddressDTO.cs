using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace iVertion.Application.DTOs
{
    public class PersonAddressDTO : BaseDTO
    {
        [Required(ErrorMessage = "Person Id is required.")]
        [DisplayName("Person Id")]
        public int PersonId { get; set; }
        [Required(ErrorMessage = "Address Id is required.")]
        [DisplayName("Address Id")]
        public int AddressId { get; set; }
    }
}
