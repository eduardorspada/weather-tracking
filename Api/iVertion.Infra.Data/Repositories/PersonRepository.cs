
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;
using iVertion.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iVertion.Infra.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PagedBaseResponse<Person>> GetPersonAsync(PersonFilterDb request)
        {
            var persons = _context.Persons.AsQueryable();

            if(!String.IsNullOrEmpty(request.FirstName))
                persons = persons.Where(c => c.FirstName.Contains(request.FirstName));
            if(!String.IsNullOrEmpty(request.LastName))
                persons = persons.Where(c => c.LastName.Contains(request.LastName));
            if (request.Active != null)
                persons = persons.Where(c => c.Active == request.Active);
            
            return await PagedBaseResponseHelper
                            .GetResponseAsync<PagedBaseResponse<Person>, Person>(persons, request);
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var person = await _context.Persons.FindAsync(id);
            return person;
        }
    }
}