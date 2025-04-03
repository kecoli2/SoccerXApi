using System;
using System.Threading.Tasks;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Interfaces.Repository;

public interface ILikeRepository : IGenericRepository<Like>
{
    Task<int> CountLikesByBetSlipIdAsync(Guid betSlipId);
}