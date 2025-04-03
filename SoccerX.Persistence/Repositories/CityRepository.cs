using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Cities
                .Where(c => c.Countryid == countryId)
                .ToListAsync();
        }

        public async Task<City?> GetByNameAsync(string name)
        {
            return await _context.Cities.FirstOrDefaultAsync(c => c.Name == name);
        }
        #endregion

        #region Private Method
        #endregion
    }
}