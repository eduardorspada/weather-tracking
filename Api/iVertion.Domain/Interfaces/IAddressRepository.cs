using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface IAddressRepository
    {
        Task<PagedBaseResponse<Address>> GetAddressAsync(AddressFilterDb request);
        Task<Address> GetAddressByIdAsync(int id);
    }
}