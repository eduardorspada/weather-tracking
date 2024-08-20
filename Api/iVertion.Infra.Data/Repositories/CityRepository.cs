using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;
using iVertion.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iVertion.Infra.Data.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;
        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PagedBaseResponse<City>> GetCityAsync(CityFilterDb request)
        {
            var cities = _context.Cities.AsQueryable();

            if (!String.IsNullOrEmpty(request.Name)) {
                cities = cities.Where(c => c.Name.Contains(request.Name));
            }

            return await PagedBaseResponseHelper
                            .GetResponseAsync<PagedBaseResponse<City>, City>(cities, request);
        }

        public async Task<City> GetCityById(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var city = await _context.Cities.FindAsync(id);
            return city;
        }
    }
}