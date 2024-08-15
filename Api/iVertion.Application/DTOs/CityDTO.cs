using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace iVertion.Application.DTOs
{
    public class CityDTO : BaseDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(2)]
        [MaxLength(150)]
        [DisplayName("Name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Code is required.")]
        [DisplayName("Code")]
        public int Code { get; set; }
    }
}
