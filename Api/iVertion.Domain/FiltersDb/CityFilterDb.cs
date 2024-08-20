
using iVertion.Domain.Interfaces;

namespace iVertion.Domain.FiltersDb
{
    public class CityFilterDb : PagedBaseRequest
    {
        public string? Name { get; set; }
        public bool? Active { get; set; }
    }
}