
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface ICountryRepository
    {
        Task<PagedBaseResponse<Country>> GetCountryAsync(CountryFilterDb request);
        Task<Country> GetCountryByIdAsync(int id);
    }
}