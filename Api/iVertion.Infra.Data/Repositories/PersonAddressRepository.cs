using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;
using iVertion.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iVertion.Infra.Data.Repositories
{
    public class PersonAddressRepository : IPersonAddressRepository
    {
        private readonly ApplicationDbContext _context;
        public PersonAddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedBaseResponse<PersonAddress>> GetPersonAddressesAsync(PersonAddressFilterDb request)
        {
            var personAddresses = _context.PersonAddresses.AsQueryable();
            if (request.PersonId > 0) {
                personAddresses = personAddresses.Where(pa => pa.PersonId == request.PersonId);
            }
            if (request.AddressId > 0) {
                personAddresses = personAddresses.Where(pa => pa.AddressId == request.AddressId);
            }
            personAddresses = personAddresses.Where(pa => pa.Active == request.Active);

            return await PagedBaseResponseHelper
                        .GetResponseAsync<PagedBaseResponse<PersonAddress>, PersonAddress>(personAddresses, request);
        }
        public async Task<PersonAddress> GetPersonAddressByIdAsync(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var personAddress = await _context.PersonAddresses.FindAsync(id);
            return personAddress;
        }

    }
}
