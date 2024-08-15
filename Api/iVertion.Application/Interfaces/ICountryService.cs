using iVertion.Application.DTOs;
using iVertion.Application.Services;
using iVertion.Domain.FiltersDb;

namespace iVertion.Application.Interfaces
{
    public interface ICountryService
    {
        Task<ResultService<PagedBaseResponseDTO<CountryDTO>>> GetCountriesAsync(CountryFilterDb countryFilterDb);
        Task<ResultService<CountryDTO>> GetCountryByIdAsync(int id);
        Task CreateCountryAsync(CountryDTO countryDto);
        Task UpdateCountryAsync(CountryDTO countryDto);
        Task RemoveCountryAsync(int id);
    }
}
