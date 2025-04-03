using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Interfaces.Repository;

public interface IReferralRewardRepository : IGenericRepository<Referralreward>
{
    Task<IEnumerable<Referralreward>> GetRewardsByReferrerAsync(Guid referrerId);
}