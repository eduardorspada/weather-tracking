using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace iVertion.Application.DTOs
{
    public class WeatherConditionDTO : BaseDTO
    {
        [Required(ErrorMessage = "City Name is required.")]
        [DisplayName("City Name")]
        public string? CityName { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [DisplayName("Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Temperature is required.")]
        [DisplayName("Temperature")]
        public double Temperature { get; set; }
        [Required(ErrorMessage = "Max Temperature is required.")]
        [DisplayName("Max Temperature")]
        public double MaxTemperature { get; set; }
        [Required(ErrorMessage = "Min Temperature is required.")]
        [DisplayName("Min Temperature")]
        public double MinTemperature { get; set; }
        [Required(ErrorMessage = "Thermal Sensation is required.")]
        [DisplayName("Thermal Sensation")]
        public double ThermalSensation { get; set; }
        [Required(ErrorMessage = "Humidity is required.")]
        [DisplayName("Humidity")]
        public int Humidity { get; set; }
        [Required(ErrorMessage = "Wind Speed is required.")]
        [DisplayName("Wind Speed")]
        public double WindSpeed { get; set; }
        [Required(ErrorMessage = "Wind Direction is required.")]
        [DisplayName("Wind Direction")]
        public string? WindDirection { get; set; }
        [Required(ErrorMessage = "Uv Index is required.")]
        [DisplayName("Uv Index")]
        public int UvIndex { get; set; }
        [Required(ErrorMessage = "Pollen Count is required.")]
        [DisplayName("Pollen Count")]
        public double PollenCount { get; set; }
        [Required(ErrorMessage = "Dust Level is required.")]
        [DisplayName("Dust Level")]
        public double DustLevel { get; set; }
        [Required(ErrorMessage = "Sunrise is required.")]
        [DisplayName("Sunrise")]
        public DateTime? Sunrise { get; set; }
        [Required(ErrorMessage = "Sunset is required.")]
        [DisplayName("Sunset")]
        public DateTime? Sunset { get; set; }
        [Required(ErrorMessage = "City Id is required.")]
        [DisplayName("City Id")]
        public int CityId { get; set; }
    }
}
