
using iVertion.Domain.Interfaces;

namespace iVertion.Domain.FiltersDb
{
    public class NeighborhoodFilterDb : PagedBaseRequest
    {
        public string? Name { get; set; }
        public int? Code { get; set; }
        public int? CityId { get; set; }
        public bool? Active { get; set; }
    }
}