using iVertion.Application.DTOs;
using iVertion.Application.Services;
using iVertion.Domain.FiltersDb;

namespace iVertion.Application.Interfaces
{
    public interface IPersonAddressService
    {
        Task<ResultService<PagedBaseResponseDTO<PersonAddressDTO>>> GetPersonAddressesAsync(PersonAddressFilterDb personAddressFilterDb);
        Task<ResultService<PersonAddressDTO>> GetPersonAddressByIdAsync(int id);
        Task CreatePersonAddressAsync(PersonAddressDTO personAddressDto);
        Task UpdatePersonAddressAsync(PersonAddressDTO personAddressDto);
        Task RemovePersonAddressAsync(int id);
    }
}
