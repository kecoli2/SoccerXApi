using Microsoft.EntityFrameworkCore;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;

namespace SoccerX.Persistence.Repositories
{
    public class CountryRepository(SoccerXDbContext context) : GenericRepository<Country>(context), ICountryRepository
    {

        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<Country?> GetByNameAsync(string name)
        {
            return await Context.Countries.FirstOrDefaultAsync(c => string.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<Country?> GetByCodeAsync(string code)
        {
            return await Context.Countries.FirstOrDefaultAsync(c => string.Equals(c.Countrycode, code, StringComparison.CurrentCultureIgnoreCase));
        }
        #endregion

        #region Private Method
        #endregion                   
    }
}
