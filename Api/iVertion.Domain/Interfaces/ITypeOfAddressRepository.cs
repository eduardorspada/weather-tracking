
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface ITypeOfAddressRepository
    {
        Task<PagedBaseResponse<TypeOfAddress>> GetTypeOfAddressAsync(TypeOfAddressFilterDb request);
        Task<TypeOfAddress> GetTypeOfAddressByIdAsync(int id);
    }
}