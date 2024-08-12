
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<PagedBaseResponse<Person>> GetPersonAsync(PersonFilterDb request);
        Task<Person> GetPersonByIdAsync(int id);
    }
}