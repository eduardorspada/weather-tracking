
using iVertion.Domain.Validation;

namespace iVertion.Domain.Entities
{
    public sealed class WeatherNotification : Entity
    {
        public bool IsRead { get; private set; }
        public int RetryCount { get; private set; }
        public DateTime SentAt { get; private set; }
        public DateTime NextRetryAt { get; private set; }
        public int WeatherAlertId { get; private set; }
        public WeatherAlert? WeatherAlert { get; set; }
        public int DeviceId { get; private set; }
        public Device? Device { get; set; }

        public WeatherNotification(bool isRead,
                                   int retryCount,
                                   DateTime sentAt,
                                   DateTime nextRetryAt,
                                   int weatherAlertId,
                                   int deviceId,
                                   bool active)
        {
            ValidationDomain(retryCount, 
                             weatherAlertId,
                             deviceId);
            IsRead = isRead;
            SentAt = sentAt;
            NextRetryAt = nextRetryAt;
            Active = active;
        }
        public WeatherNotification(int id,
                                   bool isRead,
                                   int retryCount,
                                   DateTime sentAt,
                                   DateTime nextRetryAt,
                                   int weatherAlertId,
                                   int deviceId,
                                   bool active)
        {
            DomainExceptionValidation.When(id < 0,
                                           "Invalid Id, must greater than zero.");
            ValidationDomain(retryCount, 
                             weatherAlertId,
                             deviceId);
            Id = id;
            IsRead = isRead;
            SentAt = sentAt;
            NextRetryAt = nextRetryAt;
            Active = active;
        }
        public void Update(bool isRead,
                           int retryCount,
                           DateTime sentAt,
                           DateTime nextRetryAt,
                           int weatherAlertId,
                           int deviceId,
                           bool active)
        {
            ValidationDomain(retryCount, 
                             weatherAlertId,
                             deviceId);
            IsRead = isRead;
            SentAt = sentAt;
            NextRetryAt = nextRetryAt;
            Active = active;
        }

        private void ValidationDomain(int retryCount,
                                      int weatherAlertId,
                                      int deviceId)
        {
            DomainExceptionValidation.When(retryCount < 0,
                                           "Invalid Retry Count, must greater than zero.");
            DomainExceptionValidation.When(retryCount > 10,
                                           "Invalid Retry Count, must be up to 10.");
            DomainExceptionValidation.When(weatherAlertId < 0,
                                           "Invalid Weather Alert Id, must greater than zero.");
            DomainExceptionValidation.When(deviceId < 0,
                                           "Invalid Device Id, must greater than zero.");
            RetryCount = retryCount;
            WeatherAlertId = weatherAlertId;
            DeviceId = deviceId;
        }
    }
}
