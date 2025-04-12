using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SoccerX.Application.Interfaces.Cache.Redis;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Common.Constants;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Services.CountryService
{
    public class CountriesService : ICountriesService
    {
        #region Field
        private readonly IRedisCacheService _redisCacheService;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        #endregion

        #region Constructor
        public CountriesService(IRedisCacheService redisCacheService, ICountryRepository countryRepository, ICityRepository cityRepository)
        {
            _redisCacheService = redisCacheService;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
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
            if (index is null or -1) return country;
            countryList?.RemoveAt((int)index);
            countryList?.Add(country);
            await _redisCacheService.RemoveAsync(SoccerXConstants.RedisCountries);
            await _redisCacheService.AddToListAsync(SoccerXConstants.RedisCountries, countryList);

            return country;
        }

        public async Task DeleteCountry(Guid id)
        {
            var entity = new Country { Id = id };
            _countryRepository.Remove(entity);
            await _countryRepository.SaveChangesAsync();
            await RemoveCountryCache(id);
        }

        public async Task<List<City>> GetCities(Guid countryId)
        {
            var cityList =
                await _redisCacheService.GetAsync<List<City>>(string.Format(SoccerXConstants.RedisCountryKeyCties, countryId));
            if (cityList != null && cityList.Count != 0) return cityList;
            Expression<Func<City, bool>>? predicate = u => u.Countryid == countryId;
            cityList = await _cityRepository.FindAsync(predicate);
            if (cityList is { Count: > 0 })
            {
                await _redisCacheService.SetAsync(string.Format(SoccerXConstants.RedisCountryKeyCties, countryId),
                    cityList);
            }
            return cityList;
        }

        public async Task<City> AddCity(City city)
        {
            await _cityRepository.AddAsync(city);
            await _cityRepository.SaveChangesAsync();
            var cacheKey = string.Format(SoccerXConstants.RedisCountryKeyCties, city.Countryid);
            var cityList = await _redisCacheService.GetAsync<List<City>>(cacheKey);
            await _redisCacheService.AddToFrontOfListAsync(cacheKey, city);
            return city;
        }

        public async Task<City> UpdateCity(City city)
        {
            var cacheKey = string.Format(SoccerXConstants.RedisCountryKeyCties, city.Countryid);
            _cityRepository.Update(city);
            await _cityRepository.SaveChangesAsync();
            await RemoveCityCache(city.Id, cacheKey);
            return city;
        }

        public async Task DeleteCity(Guid id)
        {
            Expression<Func<City, bool>>? predicate = u => u.Id == id;
            var city = await _cityRepository.FindAsync(predicate);
            if (city.Count == 0)
            {
                return;
            }
            var cacheKey = string.Format(SoccerXConstants.RedisCountryKeyCties, city.First().Countryid);
            await RemoveCityCache(city.First().Id, cacheKey);
        }
        #endregion

        #region Private Method

        private async Task<int> RemoveCountryCache(Guid id)
        {
            var countryList = await _redisCacheService.GetAsync<List<Country>>(SoccerXConstants.RedisCountries);
            var index = countryList?.FindIndex(x => x.Id == id);
            if (index is null or -1) return -1;

            countryList?.RemoveAt((int)index);
            await _redisCacheService.RemoveAsync(SoccerXConstants.RedisCountries);
            await _redisCacheService.SetAsync(SoccerXConstants.RedisCountries, countryList);
            return (int)index;
        }

        private Task<int> RemoveCityCache(Guid id, string cacheKey)
        {
            return Task.FromResult(-1);
            //var cityList = await _redisCacheService.GetAsync<List<City>>(cacheKey);
            //var index = cityList?.FindIndex(x => x.Id == id);
            //if (index is null or -1) return -1;

            //cityList?.RemoveAt((int)index);
            //await _redisCacheService.RemoveAsync(cacheKey);
            //await _redisCacheService.SetAsync(cacheKey, cityList);
            //return (int)index;
        }
        #endregion
    }
}
