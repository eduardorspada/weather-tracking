using iVertion.Application.DTOs;
using iVertion.Application.Services;
using iVertion.Domain.FiltersDb;

namespace iVertion.Application.Interfaces
{
    public interface ICityService
    {
        Task<ResultService<PagedBaseResponseDTO<CityDTO>>> GetCitiesAsync(CityFilterDb cityFilterDb);
        Task<ResultService<CityDTO>> GetCityByIdAsync(int id);
        Task CreateCityAsync(CityDTO cityDto);
        Task UpdateCityAsync(CityDTO cityDto);
        Task RemoveCityAsync(int id);
    }
}
