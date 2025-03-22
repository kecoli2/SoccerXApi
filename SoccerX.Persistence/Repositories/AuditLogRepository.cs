using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SoccerX.Persistence.Repositories
{
    public class AuditLogRepository(SoccerXDbContext context) : GenericRepository<Auditlog>(context), IAuditLogRepository
    {
        public async Task<IEnumerable<Auditlog>> GetLogsByEntityNameAsync(string entityName)
        {
            return await _context.Auditlog
                .Where(a => a.Entityname == entityName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Auditlog>> GetLogsByUserIdAsync(Guid userId)
        {
            return await _context.Auditlog
                .Where(a => a.Performedby == userId)
                .ToListAsync();
        }
    }
}
