
using iVertion.Application.DTOs;
using iVertion.Application.Services;
using iVertion.Domain.FiltersDb;

namespace iVertion.Application.Interfaces
{
    public interface ITemporaryUserRoleService
    {
         Task<ResultService<PagedBaseResponseDTO<TemporaryUserRoleDTO>>> GetTemporaryUserRolesAsync(TemporaryUserRoleFilterDb temporaryUserRoleFilterDb);
         Task<ResultService<TemporaryUserRoleDTO>> GetTemporaryUserRoleByIdAsync(int id);
         Task CreateTemporaryUserRoleAsync(TemporaryUserRoleDTO temporaryUserRoleDto);
         Task UpdateTemporaryUserRoleAsync(TemporaryUserRoleDTO temporaryUserRoleDto);
         Task RemoveTemporaryUserRoleAsync(int id);
    }
}