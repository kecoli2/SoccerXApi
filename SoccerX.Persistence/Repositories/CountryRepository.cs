using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interfaces;

namespace SoccerX.Persistence.Repositories
{
    public class CountryRepository(SoccerXDbContext context) : GenericRepository<Countries>(context), ICountryRepository
    {

        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<Countries?> GetByNameAsync(string name)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task<Countries?> GetByCodeAsync(string code)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Countrycode.ToLower() == code.ToLower());
        }
        #endregion

        #region Private Method
        #endregion                   
    }
}
