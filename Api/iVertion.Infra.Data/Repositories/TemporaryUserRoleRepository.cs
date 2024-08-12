
using iVertion.Domain.Entities;
using iVertion.Domain.FiltersDb;
using iVertion.Domain.Interfaces;
using iVertion.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iVertion.Infra.Data.Repositories
{
    public class TemporaryUserRoleRepository : ITemporaryUserRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public TemporaryUserRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedBaseResponse<TemporaryUserRole>> GetTemporaryUserRoleAsync(TemporaryUserRoleFilterDb request)
        {
            var temporaryUserRoles = _context.TemporaryUserRoles.AsQueryable();

            if(!String.IsNullOrEmpty(request.Role))
                temporaryUserRoles = temporaryUserRoles.Where(c => c.Role.Contains(request.Role));

            if(!String.IsNullOrEmpty(request.TargetUserId))
                temporaryUserRoles = temporaryUserRoles.Where(c => c.TargetUserId == request.TargetUserId);

            // Time interval.

            if(request.StartDate != null)
                temporaryUserRoles = temporaryUserRoles.Where(c => c.StartDate <= request.StartDate);

            if(request.ExpirationDate != null)
                temporaryUserRoles = temporaryUserRoles.Where(c => c.ExpirationDate >= request.ExpirationDate);

            // UserId
            if (!String.IsNullOrEmpty(request.UserId))
                temporaryUserRoles = temporaryUserRoles.Where(p => p.UserId.Contains(request.UserId));
            return await PagedBaseResponseHelper
                            .GetResponseAsync<PagedBaseResponse<TemporaryUserRole>, TemporaryUserRole>(temporaryUserRoles, request);
        }

        public async Task<TemporaryUserRole> GetTemporaryUserRoleByIdAsync(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var  temporaryUserRole = await _context.TemporaryUserRoles.FindAsync(id);
            return temporaryUserRole;
        }
    }
}