﻿using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using SoccerX.Application.Interfaces.Repository;

namespace SoccerX.Persistence.Repositories
{
    public class TeamRepository(SoccerXDbContext context) : GenericRepository<Team>(context), ITeamRepository
    {

        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<IEnumerable<Team>> GetTeamsByTagAsync(string tagKey, string tagValue)
        {
            return await Context.Teams
                .Where(t => t.Tags != null &&
                            EF.Functions.JsonContains(t.Tags, $@"{{ ""{tagKey}"": ""{tagValue}"" }}"))
                .ToListAsync();
        }
        #endregion

        #region Private Method
        #endregion                   
    }
}
