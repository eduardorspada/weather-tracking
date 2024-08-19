using iVertion.Domain.Interfaces;

namespace iVertion.Domain.FiltersDb
{
    public class WeatherConditionFilterDb : PagedBaseRequest
    {
        public string? CityName { get; set; }
        public string? Description { get; set; }
        public int CityId { get; set; }
        public DateTime? IntialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public bool Active { get; set; }
    }
}
