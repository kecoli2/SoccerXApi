using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Interfaces.Repository;

public interface IPaymentRepository : IGenericRepository<Payment>
{
    Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(Guid userId);
    Task<Payment?> GetLastSuccessfulPaymentAsync(Guid userId);
}