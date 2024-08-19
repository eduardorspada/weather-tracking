using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface IWeatherNotificationRepository
    {
        Task<PagedBaseResponse<WeatherNotification>> GetWeatherNotificationsAsync(WeatherNotificationFilterDb request);
        Task<WeatherNotification> GetWeatherNotificationByIdAsync(int id);
    }
}
