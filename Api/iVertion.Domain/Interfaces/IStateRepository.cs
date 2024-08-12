
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface IStateRepository
    {
        Task<PagedBaseResponse<State>> GetStateAsync(StateFilterDb request);
        Task<State> GetStateByIdAsync(int id);
    }
}