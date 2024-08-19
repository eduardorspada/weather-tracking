using iVertion.Domain.Interfaces;

namespace iVertion.Domain.FiltersDb
{
    public class WeatherAlertFilterDb : PagedBaseRequest
    {
        public string? CityName { get; set; }
        public string? Message { get; set; }
        public int CityId { get; set; }
        public DateTime? IntialAlertTime { get; set; }
        public DateTime? FinalAlertTime { get; set; }
        public bool Active { get; set; }
    }
}
