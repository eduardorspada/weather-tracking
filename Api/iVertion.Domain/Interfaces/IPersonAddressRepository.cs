using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface IPersonAddressRepository
    {
        Task<PagedBaseResponse<PersonAddress>> GetPersonAddressesAsync(PersonAddressFilterDb request);
        Task<PersonAddress> GetPersonAddressByIdAsync(int id);
    }
}
