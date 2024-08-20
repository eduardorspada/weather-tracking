using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;
using iVertion.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iVertion.Infra.Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedBaseResponse<Address>> GetAddressAsync(AddressFilterDb request)
        {
            var addresses = _context.Addresses.AsQueryable();

            if (request.CityId != null) {
                addresses = addresses.Where(a => a.CityId == request.CityId);
            }
            if (request.StateId != null) {
                addresses = addresses.Where(a => a.StateId == request.StateId);
            }
            if (request.CountryId != null) {
                addresses = addresses.Where(a => a.CountryId == request.CountryId);
            }
            if (request.UserId != null) {
                addresses = addresses.Where(a => a.UserId == request.UserId);
            }
            addresses = addresses.Where(a => a.Active == request.Active);

            return await PagedBaseResponseHelper
                            .GetResponseAsync<PagedBaseResponse<Address>, Address>(addresses, request);
        }

        public async Task<Address> GetAddressByIdAsync(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var address = await _context.Addresses.FindAsync(id);
            return address;
        }
    }
}