
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface INeighborhoodRepository
    {
        Task<PagedBaseResponse<Neighborhood>> GetNeighborhoodAsync(NeighborhoodFilterDb request);
        Task<Neighborhood> GetNeighborhoodByIdAsync(int id);
    }
}