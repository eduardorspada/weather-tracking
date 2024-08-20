using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace iVertion.Application.DTOs
{
    public class StateDTO : BaseDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(2)]
        [MaxLength(150)]
        [DisplayName("Name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Acronym is required.")]
        [MinLength(2)]
        [MaxLength(5)]
        [DisplayName("Acronym")]
        public string? Acronym { get; set; }
    }
}
