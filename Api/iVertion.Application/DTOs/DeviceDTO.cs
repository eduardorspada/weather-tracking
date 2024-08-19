using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iVertion.Application.DTOs
{
    public class DeviceDTO : BaseDTO
    {
        [Required(ErrorMessage = "Token is required.")]
        [DisplayName("Token")]
        public string? Token { get; set; }
        [Required(ErrorMessage = "Device Name is required.")]
        [DisplayName("Device Name")]
        public string? DeviceName { get; set; }
        [Required(ErrorMessage = "Accepted Notification value is required.")]
        [DisplayName("Accepted Notification")]
        public bool AcceptNotifications { get; set; }
        [Required(ErrorMessage = "Person Id is required.")]
        [DisplayName("Person Id")]
        public int PersonId { get; set; }
    }
}
