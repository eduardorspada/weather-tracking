using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;
using iVertion.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iVertion.Infra.Data.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;
        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PagedBaseResponse<Country>> GetCountryAsync(CountryFilterDb request)
        {
            var countries = _context.Countries.AsQueryable();

            if (!String.IsNullOrEmpty(request.Name)) {
                countries = countries.Where(c => c.Name.Contains(request.Name));
            }
            if (!String.IsNullOrEmpty(request.Acronym)) {
                countries = countries.Where(c => c.Acronym.Contains(request.Acronym));
            }
            if (request.Code != null) {
                countries = countries.Where(a => a.Code == request.Code);
            }

            return await PagedBaseResponseHelper
                            .GetResponseAsync<PagedBaseResponse<Country>, Country>(countries, request);
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var country = await _context.Countries.FindAsync(id);
            return country;
        }
    }
}