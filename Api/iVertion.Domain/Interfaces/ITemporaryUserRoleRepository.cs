
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface ITemporaryUserRoleRepository
    {
         Task<PagedBaseResponse<TemporaryUserRole>> GetTemporaryUserRoleAsync(TemporaryUserRoleFilterDb request);
         Task<TemporaryUserRole> GetTemporaryUserRoleByIdAsync(int id);
    }
}