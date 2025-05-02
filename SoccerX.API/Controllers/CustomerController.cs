using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoccerX.Application.Commands.UserCommand;
using SoccerX.Common.Constants;
using SoccerX.DTO.Dto.User;

namespace SoccerX.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CustomerController : Controller
{
    #region Field
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public CustomerController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    #endregion

    #region Actions   
    [HttpPost("Create")]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
    {        
        var result = await _mediator.Send(new CreateUserCommand(dto));
        return Ok(result);
    }

    [HttpPost("Create2")]
    [Authorize(Policy = SoccerXConstants.PolicySoccerX, Roles = SoccerXConstants.RoleAdmin)]
    public async Task<IActionResult> Create2([FromBody] UserCreateDto dto)
    {
        var result = await _mediator.Send(new CreateUserCommand(dto));
        return Ok(result);
    }
    #endregion
}
