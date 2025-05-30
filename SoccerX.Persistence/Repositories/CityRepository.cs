﻿using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using SoccerX.Application.Interfaces.Repository;

namespace SoccerX.Persistence.Repositories
{
    public class CityRepository(SoccerXDbContext context) : GenericRepository<City>(context), ICityRepository
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<IEnumerable<City>> GetCitiesByCountryIdAsync(Guid countryId)
        {
            return await Context.Cities
                .Where(c => c.Countryid == countryId)
                .ToListAsync();
        }

        public async Task<City?> GetByNameAsync(string name)
        {
            return await Context.Cities.FirstOrDefaultAsync(c => c.Name == name);
        }
        #endregion

        #region Private Method
        #endregion
    }
}