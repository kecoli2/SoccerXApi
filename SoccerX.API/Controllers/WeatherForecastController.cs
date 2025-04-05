using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Common.Shared.Model;
using SoccerX.Domain.Entities;
using SoccerX.DTO.Dto;
using SoccerX.Persistence.Repositories;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;

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

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ICityRepository cityRepository, IMapper mapper)
    {
        _logger = logger;
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get(int pageNumber, int pageSize)
    {
        //var lst = await _cityRepository.GetAllAsync();
        var paging = await _cityRepository.GetPagedAsync(null, pageNumber, pageSize);

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

        var pagedResult = await _cityRepository.GetPagedByCompositeCursorAsync(predicate, lastCursor, pageSize);

         var dtoResult = _mapper.Map<CursorPagedResultDto<CityDto, CompositeCursorGuid>>(pagedResult);
        return Ok(dtoResult);

    }
}
