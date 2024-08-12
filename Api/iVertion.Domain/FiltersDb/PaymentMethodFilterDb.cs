
using iVertion.Domain.Interfaces;

namespace iVertion.Domain.FiltersDb
{
    public class PaymentMethodFilterDb : PagedBaseRequest
    {
        public string? Name { get; set; }
        public bool? Active { get; set; }
    }
}