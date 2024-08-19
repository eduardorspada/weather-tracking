using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;
using iVertion.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iVertion.Infra.Data.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDbContext _context;
        public DeviceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Device> GetDeviceByIdAsync(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var device = await _context.Devices.FindAsync(id);
            return device;
        }

        public async Task<PagedBaseResponse<Device>> GetDevicesAsync(DeviceFilterDb request)
        {
            var devices = _context.Devices.AsQueryable();

            if (request.DeviceName != null)
                devices = devices.Where(d => d.DeviceName.Contains(request.DeviceName));
            if (request.AcceptNotifications != null)
                devices = devices.Where(d => d.AcceptNotifications ==  request.AcceptNotifications);
            if (request.PersonId > 0)
                devices = devices.Where(d => d.PersonId == request.PersonId);
            if (request.Active != null)
                devices = devices.Where(d => d.Active == request.Active);

            return await PagedBaseResponseHelper
                            .GetResponseAsync<PagedBaseResponse<Device>, Device>(devices, request);

        }
    }
}
