using iVertion.Application.DTOs;
using iVertion.Application.Services;
using iVertion.Domain.FiltersDb;

namespace iVertion.Application.Interfaces
{
    public interface IAddressService
    {
        Task<ResultService<PagedBaseResponseDTO<AddressDTO>>> GetAddressesAsync(AddressFilterDb addressFilterDb);
        Task<ResultService<AddressDTO>> GetAddressByIdAsync(int id);
        Task CreateAddressAsync(AddressDTO addressDto);
        Task UpdateAddressAsync(AddressDTO addressDto);
        Task RemoveAddressAsync(int id);
    }
}
