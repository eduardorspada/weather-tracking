using iVertion.Application.DTOs;
using iVertion.Application.Services;
using iVertion.Domain.FiltersDb;

namespace iVertion.Application.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<ResultService<PagedBaseResponseDTO<WeatherForecastDTO>>> GetWeatherForecastsAsync(WeatherForecastFilterDb weatherForecastFilterDb);
        Task<ResultService<WeatherForecastDTO>> GetWeatherForecastByIdAsync(int id);
        Task CreateWeatherForecastAsync(WeatherForecastDTO weatherForecastDto);
        Task UpdateWeatherForecastAsync(WeatherForecastDTO weatherForecastDto);
        Task RemoveWeatherForecastAsync(int id);
    }
}
