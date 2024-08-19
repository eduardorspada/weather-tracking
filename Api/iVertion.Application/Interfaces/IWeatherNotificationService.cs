using iVertion.Application.DTOs;
using iVertion.Application.Services;
using iVertion.Domain.FiltersDb;

namespace iVertion.Application.Interfaces
{
    public interface IWeatherNotificationService
    {
        Task<ResultService<PagedBaseResponseDTO<WeatherNotificationDTO>>> GetWeatherNotificationsAsync(WeatherNotificationFilterDb weatherNotificationFilterDb);
        Task<ResultService<WeatherNotificationDTO>> GetWeatherNotificationByIdAsync(int id);
        Task CreateWeatherNotificationAsync(WeatherNotificationDTO weatherNotificationDto);
        Task UpdateWeatherNotificationAsync(WeatherNotificationDTO weatherNotificationDto);
        Task RemoveWeatherNotificationAsync(int id);
    }
}
