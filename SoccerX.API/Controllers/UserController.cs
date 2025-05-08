using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoccerX.Application.Commands.UserCommand;
using SoccerX.Common.Constants;
using SoccerX.DTO.Dto.User;

namespace SoccerX.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : Controller
{
    #region Field
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    #endregion

    #region Actions   
    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserCreateDto dto)
    {        
        var result = await _mediator.Send(new CreateUserCommand(dto));
        return Ok(result);
    }

    [HttpPost("RegisterAdmin")]
    [Authorize(Policy = SoccerXConstants.PolicySoccerX, Roles = SoccerXConstants.RoleAdmin)]
    public async Task<IActionResult> RegisterAdmin([FromBody] UserCreateDto dto)
    {
        var result = await _mediator.Send(new CreateUserCommand(dto));
        return Ok(result);
    }
    #endregion
}
