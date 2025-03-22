using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface IAuditLogRepository : IGenericRepository<Auditlog>
    {
        Task<IEnumerable<Auditlog>> GetLogsByEntityNameAsync(string entityName);
        Task<IEnumerable<Auditlog>> GetLogsByUserIdAsync(Guid userId);
    }
}
