using iVertion.Application.DTOs;
using iVertion.Application.Services;
using iVertion.Domain.FiltersDb;

namespace iVertion.Application.Interfaces
{
    public interface IStateService
    {
        Task<ResultService<PagedBaseResponseDTO<StateDTO>>> GetStatesAsync(StateFilterDb stateFilterDb);
        Task<ResultService<StateDTO>> GetStateByIdAsync(int id);
        Task CreateStateAsync(StateDTO stateDto);
        Task UpdateStateAsync(StateDTO stateDto);
        Task RemoveStateAsync(int id);
    }
}
