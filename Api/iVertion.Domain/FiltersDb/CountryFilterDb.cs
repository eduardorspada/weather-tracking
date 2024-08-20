
using iVertion.Domain.Interfaces;

namespace iVertion.Domain.FiltersDb
{
    public class CountryFilterDb : PagedBaseRequest
    {
        public string? Name { get; set; }
        public string? Acronym { get; set; }
        public bool? Active { get; set; }
    }
}