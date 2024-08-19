using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace iVertion.Application.DTOs
{
    public class WeatherAlertDTO : BaseDTO
    {
        [Required(ErrorMessage = "City Name is required.")]
        [DisplayName("City Name")]
        public string? CityName { get; set; }
        [Required(ErrorMessage = "Severity Level is required.")]
        [DisplayName("Severity Level")]
        public int SeverityLevel { get; set; }
        [Required(ErrorMessage = "Message is required.")]
        [DisplayName("Message")]
        public string? Message { get; set; }
        [Required(ErrorMessage = "Alert Time is required.")]
        [DisplayName("Alert Time")]
        public DateTime AlertTime { get; set; }
        [Required(ErrorMessage = "City Id is required.")]
        [DisplayName("City Id")]
        public int CityId { get; set; }
    }
}
