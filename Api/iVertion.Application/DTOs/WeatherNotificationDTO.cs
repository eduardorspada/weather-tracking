using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace iVertion.Application.DTOs
{
    public class WeatherNotificationDTO : BaseDTO
    {
        [Required(ErrorMessage = "Is Read value is required.")]
        [DisplayName("Is Read")]
        public bool IsRead { get; set; }
        [Required(ErrorMessage = "Retry Count is required.")]
        [DisplayName("Retry Count")]
        public int RetryCount { get; set; }
        [Required(ErrorMessage = "Sent At is required.")]
        [DisplayName("Sent At")]
        public DateTime SentAt { get; set; }
        [Required(ErrorMessage = "Next Retry At is required.")]
        [DisplayName("Next Retry At")]
        public DateTime NextRetryAt { get; set; }
        [Required(ErrorMessage = "Weather Alert Id is required.")]
        [DisplayName("Weather Alert Id")]
        public int WeatherAlertId { get; set; }
        [Required(ErrorMessage = "Device Id is required.")]
        [DisplayName("Device Id")]
        public int DeviceId { get; set; }
    }
}
