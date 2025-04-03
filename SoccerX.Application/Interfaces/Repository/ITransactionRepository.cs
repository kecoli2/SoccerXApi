using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Interfaces.Repository;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId);
    Task<decimal> GetUserBalanceAsync(Guid userId);
}