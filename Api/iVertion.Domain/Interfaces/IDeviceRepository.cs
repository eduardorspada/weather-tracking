using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;

namespace iVertion.Domain.Interfaces
{
    public interface IDeviceRepository
    {
        Task<PagedBaseResponse<Device>> GetDevicesAsync(DeviceFilterDb request);
        Task<Device> GetDeviceByIdAsync(int id);
    }
}
