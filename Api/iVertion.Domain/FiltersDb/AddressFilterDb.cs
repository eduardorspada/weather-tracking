
using iVertion.Domain.Interfaces;

namespace iVertion.Domain.FiltersDb
{
    public class AddressFilterDb : PagedBaseRequest
    {
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public bool? Active { get; set; }
    }
}