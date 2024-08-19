using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace iVertion.Application.DTOs
{
    public class WeatherForecastDTO : BaseDTO
    {
        [Required(ErrorMessage = "City Name is required.")]
        [DisplayName("City Name")]
        public string? CityName { get; set; }
        [Required(ErrorMessage = "Date is required.")]
        [DisplayName("Date")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Weekday is required.")]
        [DisplayName("Weekday")]
        public string? Weekday { get; set; }
        [Required(ErrorMessage = "Max Temperature is required.")]
        [DisplayName("Max Temperature")]
        public double MaxTemperature { get; set; }
        [Required(ErrorMessage = "Min Temperature is required.")]
        [DisplayName("Min Temperature")]
        public double MinTemperature { get; set; }
        [Required(ErrorMessage = "Humidity is required.")]
        [DisplayName("Humidity")]
        public int Humidity { get; set; }
        [Required(ErrorMessage = "Rain Probability is required.")]
        [DisplayName("Rain Probability")]
        public double RainProbability { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [DisplayName("Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "City Id is required.")]
        [DisplayName("City Id")]
        public int CityId { get; set; }
    }
}
