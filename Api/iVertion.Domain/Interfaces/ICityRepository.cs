
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface ICityRepository
    {
        Task<PagedBaseResponse<City>> GetCityAsync(CityFilterDb request);
        Task<City> GetCityById(int id);
    }
}