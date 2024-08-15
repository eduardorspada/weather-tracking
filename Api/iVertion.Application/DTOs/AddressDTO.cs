using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace iVertion.Application.DTOs
{
    public class AddressDTO : BaseDTO
    {
        [Required(ErrorMessage = "City Id is required.")]
        [DisplayName("City Id")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "State Id is required.")]
        [DisplayName("State Id")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "Country Id is required.")]
        [DisplayName("Country Id")]
        public int CountryId { get; set; }
    }
}
