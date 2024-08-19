using AutoMapper;
using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;

namespace iVertion.Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public DeviceService(IDeviceRepository deviceRepository,
                              IRepository repo,
                              IMapper mapper)
        {
            _deviceRepository = deviceRepository ??
                throw new ArgumentNullException(nameof(deviceRepository));
            _repo = repo ??
                throw new ArgumentNullException(nameof(_repo));
            _mapper = mapper;
        }
        public async Task CreateDeviceAsync(DeviceDTO deviceDto)
        {
            var device = _mapper.Map<Device>(deviceDto);
            await _repo.CreateAsync(device);
        }

        public async Task<ResultService<DeviceDTO>> GetDeviceByIdAsync(int id)
        {
            var device = await _deviceRepository.GetDeviceByIdAsync(id);
            return ResultService.OK(_mapper.Map<DeviceDTO>(device));
        }

        public async Task<ResultService<PagedBaseResponseDTO<DeviceDTO>>> GetDevicesAsync(DeviceFilterDb deviceFilterDb)
        {
            var devices = await _deviceRepository.GetDevicesAsync(deviceFilterDb);
            var result = new PagedBaseResponseDTO<DeviceDTO>(
                devices.TotalRegisters,
                _mapper.Map<List<DeviceDTO>>(devices.Data)
                );
            return ResultService.OK(result);
        }

        public async Task RemoveDeviceAsync(int id)
        {
            var deviceEntity = _deviceRepository.GetDeviceByIdAsync(id).Result;
            await _repo.RemoveAsync(deviceEntity);
        }

        public async Task UpdateDeviceAsync(DeviceDTO deviceDto)
        {
            var deviceEntity = _mapper.Map<Device>(deviceDto);
            await _repo.UpdateAsync(deviceEntity);
        }
    }
}
