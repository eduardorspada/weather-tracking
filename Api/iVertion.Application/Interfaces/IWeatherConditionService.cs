using iVertion.Application.DTOs;
using iVertion.Application.Services;
using iVertion.Domain.FiltersDb;

namespace iVertion.Application.Interfaces
{
    public interface IWeatherConditionService
    {
        Task<ResultService<PagedBaseResponseDTO<WeatherConditionDTO>>> GetWeatherConditionsAsync(WeatherConditionFilterDb weatherConditionFilterDb);
        Task<ResultService<WeatherConditionDTO>> GetWeatherConditionByIdAsync(int id);
        Task CreateWeatherConditionAsync(WeatherConditionDTO weatherConditionDto);
        Task UpdateWeatherConditionAsync(WeatherConditionDTO weatherConditionDto);
        Task RemoveWeatherConditionAsync(int id);
    }
}
