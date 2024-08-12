
using iVertion.Domain.Interfaces;

namespace iVertion.Domain.FiltersDb
{
    public class PersonFilterDb : PagedBaseRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? Active { get; set; }
        public int CityId { get; set; }
    }
}