using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface IWeatherForecastRepository
    {
        Task<PagedBaseResponse<WeatherForecast>> GetWeatherForecastsAsync(WeatherForecastFilterDb request);
        Task<WeatherForecast> GetWeatherForecastByIdAsync(int id);
    }
}
