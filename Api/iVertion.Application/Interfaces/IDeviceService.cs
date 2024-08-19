using iVertion.Application.DTOs;
using iVertion.Application.Services;
using iVertion.Domain.FiltersDb;

namespace iVertion.Application.Interfaces
{
    public interface IDeviceService
    {
        Task<ResultService<PagedBaseResponseDTO<DeviceDTO>>> GetDevicesAsync(DeviceFilterDb deviceFilterDb);
        Task<ResultService<DeviceDTO>> GetDeviceByIdAsync(int id);
        Task CreateDeviceAsync(DeviceDTO deviceDto);
        Task UpdateDeviceAsync(DeviceDTO deviceDto);
        Task RemoveDeviceAsync(int id);
    }
}
