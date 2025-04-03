using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Interfaces.Repository;

public interface ICityRepository : IGenericRepository<City>
{
    Task<IEnumerable<City>> GetCitiesByCountryIdAsync(Guid countryId);
}