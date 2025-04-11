using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Services.CountryService
{
    public interface ICountriesService
    {
        public Task<List<Country>?> GetCountries();
        public Task<Country?> GetCountry(Guid id);
        public Task<Country> AddCountry(Country country);
        public Task<Country> UpdateCountry(Country country);
        public Task DeleteCountry(Guid id);
        public Task<List<City>> GetCities(Guid countryId);
        public Task<City> AddCity(City city);
        public Task<City> UpdateCity(City city);
        public Task DeleteCity(Guid id);

    }
}
