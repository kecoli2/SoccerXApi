using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoccerX.Application.Interfaces.Redis;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Common.Constants;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Services.CountryService
{
    public class CountriesService : ICountriesService
    {
        #region Field
        private IRedisCacheService _redisCacheService;
        private ICountryRepository _countryRepository;
        #endregion

        #region Constructor
        public CountriesService(IRedisCacheService redisCacheService, ICountryRepository countryRepository)
        {
            _redisCacheService = redisCacheService;
            _countryRepository = countryRepository;
        }
        #endregion

        #region Public Method
        public async Task<List<Country>?> GetCountries()
        {
            var countryList = await _redisCacheService.GetAsync<List<Country>>(SoccerXConstants.RedisCountries);
            if (countryList != null) return countryList;
            countryList = await _countryRepository.GetAllAsync();
            await _redisCacheService.SetAsync(SoccerXConstants.RedisCountries, countryList);
            return countryList;
        }

        public async Task<Country?> GetCountry(Guid id)
        {
            var countryList = await _redisCacheService.GetAsync<List<Country>>(SoccerXConstants.RedisCountries);
            var country = countryList?.FirstOrDefault(x => x.Id == id);
            if (country != null) return country;

            countryList = await GetCountries();
            var countries = countryList?.FirstOrDefault(x => x.Id == id);
            return countries;
        }

        public async Task<Country> AddCountry(Country country)
        {
            await _countryRepository.AddAsync(country);
            await _countryRepository.SaveChangesAsync();
            await _redisCacheService.AddToFrontOfListAsync(SoccerXConstants.RedisCountries, country);
            return country;
        }

        public async Task<Country> UpdateCountry(Country country)
        {
            _countryRepository.Update(country);
            await _countryRepository.SaveChangesAsync();
            var countryList = await _redisCacheService.GetAsync<List<Country>>(SoccerXConstants.RedisCountries);
            var index = countryList?.FindIndex(x => x.Id == country.Id);
            if (index != null && index != -1)
            {
                countryList?.RemoveAt(index ?? 0);
                countryList?.Add(country);
                await _redisCacheService.RemoveAsync(SoccerXConstants.RedisCountries);
                await _redisCacheService.AddToListAsync(SoccerXConstants.RedisCountries, countryList);
            }

            return country;
        }

        public Task DeleteCountry(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<City>> GetCities(Guid countryId)
        {
            throw new NotImplementedException();
        }

        public Task<City> AddCity(City city)
        {
            throw new NotImplementedException();
        }

        public Task<City> UpdateCity(City city)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCity(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Method
        #endregion
    }
}
