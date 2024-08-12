
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface ICountryRepository
    {
        Task<PagedBaseResponse<Country>> GetContryAsync(CountryFilterDb request);
        Task<Country> GetCountryByIdAsync(int id);
    }
}