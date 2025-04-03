using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Interfaces.Repository;

public interface IAuditLogRepository : IGenericRepository<Auditlog>
{
    Task<IEnumerable<Auditlog>> GetLogsByEntityNameAsync(string entityName);
    Task<IEnumerable<Auditlog>> GetLogsByUserIdAsync(Guid userId);
}