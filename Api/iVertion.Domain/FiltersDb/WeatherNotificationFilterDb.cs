using iVertion.Domain.Interfaces;

namespace iVertion.Domain.FiltersDb
{
    public class WeatherNotificationFilterDb : PagedBaseRequest
    {
        public bool IsRead { get; set; }
        public int RetryCount { get; set; }
        public DateTime? IntialSentAt { get; set; }
        public DateTime? FinalSentAt { get; set; }
        public DateTime? IntialNextRetryAt { get; set; }
        public DateTime? FinalNextRetryAt { get; set; }
        public int WeatherAlertId { get; set; }
        public int DeviceId { get; set; }
        public bool Active { get; set; }

    }
}
