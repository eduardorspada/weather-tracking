using iVertion.Application.DTOs;
using iVertion.Application.Services;
using iVertion.Domain.FiltersDb;

namespace iVertion.Application.Interfaces
{
    public interface IWeatherAlertService
    {
        Task<ResultService<PagedBaseResponseDTO<WeatherAlertDTO>>> GetWeatherAlertsAsync(WeatherAlertFilterDb weatherAlertFilterDb);
        Task<ResultService<WeatherAlertDTO>> GetWeatherAlertByIdAsync(int id);
        Task CreateWeatherAlertAsync(WeatherAlertDTO weatherAlertDto);
        Task UpdateWeatherAlertAsync(WeatherAlertDTO weatherAlertDto);
        Task RemoveWeatherAlertAsync(int id);
    }
}
