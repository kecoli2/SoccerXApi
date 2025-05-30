﻿using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using SoccerX.Application.Interfaces.Repository;

namespace SoccerX.Persistence.Repositories
{
    public class AuditLogRepository(SoccerXDbContext context) : GenericRepository<Auditlog>(context), IAuditLogRepository
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<IEnumerable<Auditlog>> GetLogsByEntityNameAsync(string entityName)
        {
            return await Context.Auditlogs
                .Where(a => a.Entityname == entityName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Auditlog>> GetLogsByUserIdAsync(Guid userId)
        {
            return await Context.Auditlogs
                .Where(a => a.Performedby == userId)
                .ToListAsync();
        }
        #endregion

        #region Private Method
        #endregion

    }
}
