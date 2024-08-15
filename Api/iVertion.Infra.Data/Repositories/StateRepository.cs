using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;
using iVertion.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iVertion.Infra.Data.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly ApplicationDbContext _context;
        public StateRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PagedBaseResponse<State>> GetStateAsync(StateFilterDb request)
        {
            var states = _context.States.AsQueryable();

            if (!String.IsNullOrEmpty(request.Name))
            {
                states = states.Where(c => c.Name.Contains(request.Name));
            }
            if (!String.IsNullOrEmpty(request.Acronym))
            {
                states = states.Where(c => c.Acronym.Contains(request.Acronym));
            }
            if (request.Code != null)
            {
                states = states.Where(a => a.Code == request.Code);
            }

            return await PagedBaseResponseHelper
                            .GetResponseAsync<PagedBaseResponse<State>, State>(states, request);

        }

        public async Task<State> GetStateByIdAsync(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var state = await _context.States.FindAsync(id);
            return state;
        }
    }
}