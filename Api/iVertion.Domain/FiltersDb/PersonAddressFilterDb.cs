using iVertion.Domain.Interfaces;

namespace iVertion.Domain.FiltersDb
{
    public class PersonAddressFilterDb : PagedBaseRequest
    {
        public int PersonId { get; set; }
        public int AddressId { get; set; }
        public bool Active { get; set; }
    }
}
