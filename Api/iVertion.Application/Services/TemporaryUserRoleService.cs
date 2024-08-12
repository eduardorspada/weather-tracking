
using AutoMapper;
using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;

namespace iVertion.Application.Services
{
    public class TemporaryUserRoleService : ITemporaryUserRoleService
    {
        private readonly ITemporaryUserRoleRepository _temporaryUserRoleRepository;
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public TemporaryUserRoleService(ITemporaryUserRoleRepository temporaryUserRoleRepository,
                                        IRepository repo,
                                        IMapper mapper)
        {
            _temporaryUserRoleRepository = temporaryUserRoleRepository ??
                throw new ArgumentNullException(nameof(temporaryUserRoleRepository));
            _repo = repo ??
                throw new ArgumentNullException(nameof(repo));
            _mapper = mapper;     
        }

        public async Task CreateTemporaryUserRoleAsync(TemporaryUserRoleDTO temporaryUserRoleDto)
        {
            var temporaryUserRoleEntity = _mapper.Map<TemporaryUserRole>(temporaryUserRoleDto);
            await _repo.CreateAsync(temporaryUserRoleEntity);
        }

        public async Task<ResultService<TemporaryUserRoleDTO>> GetTemporaryUserRoleByIdAsync(int id)
        {
            var temporaryUserRole = await _temporaryUserRoleRepository.GetTemporaryUserRoleByIdAsync(id);
            return ResultService.OK(_mapper.Map<TemporaryUserRoleDTO>(temporaryUserRole));
        }

        public async Task<ResultService<PagedBaseResponseDTO<TemporaryUserRoleDTO>>> GetTemporaryUserRolesAsync(TemporaryUserRoleFilterDb temporaryUserRoleFilterDb)
        {
            var temporaryUserRoles = await _temporaryUserRoleRepository.GetTemporaryUserRoleAsync(temporaryUserRoleFilterDb);
            var result = new PagedBaseResponseDTO<TemporaryUserRoleDTO>(
                temporaryUserRoles.TotalRegisters,
                _mapper.Map<List<TemporaryUserRoleDTO>>(temporaryUserRoles.Data)
            );

            return ResultService.OK(result);
        }

        public async Task RemoveTemporaryUserRoleAsync(int id)
        {
            var temporaryUserRole = _temporaryUserRoleRepository.GetTemporaryUserRoleByIdAsync(id).Result;
            await _repo.RemoveAsync(temporaryUserRole);
        }

        public async Task UpdateTemporaryUserRoleAsync(TemporaryUserRoleDTO temporaryUserRoleDto)
        {
            var temporaryUserRoleEntity = _mapper.Map<TemporaryUserRole>(temporaryUserRoleDto);
            await _repo.UpdateAsync(temporaryUserRoleEntity);
        }
    }
}