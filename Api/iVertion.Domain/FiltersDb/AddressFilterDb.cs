
using iVertion.Domain.Interfaces;

namespace iVertion.Domain.FiltersDb
{
    public class AddressFilterDb : PagedBaseRequest
    {
        public string? ZipCode { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public int? NeighborhoodId { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public bool? Active { get; set; }
    }
}