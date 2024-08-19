using iVertion.Domain.Interfaces;

namespace iVertion.Domain.FiltersDb
{
    public class DeviceFilterDb : PagedBaseRequest
    {
        public string? DeviceName { get; set; }
        public bool AcceptNotifications { get; set; }
        public int PersonId { get; set; }
        public bool Active { get; set; }
    }
}
