using AutoMapper;
using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;

namespace iVertion.Application.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public StateService(IStateRepository stateRepository,
                            IRepository repo,
                            IMapper mapper)
        {
            _stateRepository = stateRepository ?? 
                throw new ArgumentNullException(nameof(stateRepository));
            _repo = repo ??
                throw new ArgumentNullException(nameof(_repo));
            _mapper = mapper;
        }
        public async Task CreateStateAsync(StateDTO stateDto)
        {
            var state = _mapper.Map<State>(stateDto);
            await _repo.CreateAsync(state);
        }

        public async Task<ResultService<PagedBaseResponseDTO<StateDTO>>> GetStatesAsync(StateFilterDb stateFilterDb)
        {
            var states = await _stateRepository.GetStateAsync(stateFilterDb);
            var result = new PagedBaseResponseDTO<StateDTO>(
                states.TotalRegisters,
                _mapper.Map<List<StateDTO>>(states.Data)
                );
            return ResultService.OK(result);
        }

        public async Task<ResultService<StateDTO>> GetStateByIdAsync(int id)
        {
            var state = await _stateRepository.GetStateByIdAsync(id);
            return ResultService.OK(_mapper.Map<StateDTO>(state));
        }

        public async Task RemoveStateAsync(int id)
        {
            var stateEntity = _stateRepository.GetStateByIdAsync(id).Result;
            await _repo.RemoveAsync(stateEntity);
        }

        public async Task UpdateStateAsync(StateDTO stateDto)
        {
            var stateEntity = _mapper.Map<State>(stateDto);
            await _repo.UpdateAsync(stateEntity);
        }
    }
}
