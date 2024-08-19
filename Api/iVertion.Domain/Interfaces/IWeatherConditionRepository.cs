using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface IWeatherConditionRepository
    {
        Task<PagedBaseResponse<WeatherCondition>> GetWeatherConditionsAsync(WeatherConditionFilterDb request);
        Task<WeatherCondition> GetWeatherConditionByIdAsync(int id);
    }
}
