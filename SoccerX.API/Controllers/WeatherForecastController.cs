using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Common.Shared.Model;
using SoccerX.Domain.Entities;
using SoccerX.DTO.Dto;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SoccerX.Common.Configuration;
using SoccerX.Application.Interfaces.Cache.Redis;
using SoccerX.Application.Interfaces.Quartz;
using SoccerX.Application.Interfaces.Resources;
using SoccerX.Application.Services.CountryService;
using SoccerX.Common.Base.Quartz.Criteria;
using SoccerX.Common.Enums;

namespace SoccerX.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;
    private readonly IRedisCacheService _redisCacheService;
    private readonly ApplicationSettings _settings;
    private readonly ICountriesService _countriesService;
    private readonly IQuartzJobCreater _quartzJobCreater;
    private readonly IResourceManager _resourceManager;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ICityRepository cityRepository, IMapper mapper, IRedisCacheService redisCacheService, ApplicationSettings settings, ICountriesService countriesService, IQuartzJobCreater quartzJobCreater, IResourceManager resourceManager)
    {
        _logger = logger;
        _cityRepository = cityRepository;
        _mapper = mapper;
        _redisCacheService = redisCacheService;
        _settings = settings;
        _countriesService = countriesService;
        _quartzJobCreater = quartzJobCreater;
        _resourceManager = resourceManager;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get(int pageNumber, int pageSize)
    {
        var ss = _resourceManager.GetString("User.Name");

        await _quartzJobCreater.Create(JobKeyEnum.SendVerificationMail)
            .SetCriteria(new SendEmailVerifcationCriteria
            {
                UserId = Guid.NewGuid(),
                ToMailAddress = "salih",
                Culture = _resourceManager.GetCultureKey()
            })
            .SetUserId(Guid.NewGuid())
            .SetDescription("Deneme")
            .Start();


        var country = await _countriesService.GetCountries();

        //var city = await _countriesService.GetCities(country!.First().Id);

        //foreach (var ci in city)
        //{
        //    ci.Name = "S " + ci.Name;
        //    await _countriesService.UpdateCity(ci);
        //}


        var newCity = new City { Name = Guid.NewGuid().ToString(), Countryid = country!.First().Id };
        await _countriesService.AddCity(newCity);

        //var lst = await _cityRepository.GetAllAsync();
        var addKey = _redisCacheService.SetAsync("test", _settings);
        var paging = await _cityRepository.GetPagedAsync(null, null, pageNumber, pageSize);

        var se = await _redisCacheService.GetAsync<ApplicationSettings>("test");



        var dtoResult = _mapper.Map<PagedResultDto<CityDto>>(paging);

        return Ok(dtoResult);

    }

    [HttpGet("GetTestGuid", Name = "GetTestGuid")]
    public async Task<IActionResult> GetTestGuid([FromQuery] DateTime? lastCreatedDate,
        [FromQuery] Guid? lastId,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null)
    {
        // Eðer her iki parametre de saðlanmýþsa composite cursor oluþturulur
        CompositeCursorGuid? lastCursor = null;
        if (lastCreatedDate.HasValue && lastId.HasValue)
        {
            lastCursor = new CompositeCursorGuid { CreateDate = lastCreatedDate.Value, Id = lastId.Value };
        }

        Expression<Func<City, bool>>? predicate = null;
        if (!string.IsNullOrEmpty(searchTerm))
        {
            predicate = u => EF.Functions.ILike(u.Name, $"%{searchTerm}%");
        }

        var pagedResult = await _cityRepository.GetPagedByCompositeCursorAsync(predicate, null, lastCursor, pageSize);

        var dtoResult = _mapper.Map<CursorPagedResultDto<CityDto, CompositeCursorGuid>>(pagedResult);
        return Ok(dtoResult);

    }
}
