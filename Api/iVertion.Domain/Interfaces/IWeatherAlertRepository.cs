using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface IWeatherAlertRepository
    {
        Task<PagedBaseResponse<WeatherAlert>> GetWeatherAlertsAsync(WeatherAlertFilterDb request);
        Task<WeatherAlert> GetWeatherAlertByIdAsync(int id);
    }
}
