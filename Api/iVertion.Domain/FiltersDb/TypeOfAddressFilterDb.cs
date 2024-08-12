
using iVertion.Domain.Interfaces;

namespace iVertion.Domain.FiltersDb
{
    public class TypeOfAddressFilterDb : PagedBaseRequest
    {
        public string? Name { get; set; }
        public bool? Active { get; set; }
    }
}