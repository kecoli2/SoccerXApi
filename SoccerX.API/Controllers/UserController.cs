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
    private readonly ILogger<UserController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    #endregion

    #region Constructor
    public UserController(IMediator mediator, IMapper mapper, ILogger<UserController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    #endregion

    #region Actions   
    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserCreateDto dto)
    {
        try
        {
            var result = await _mediator.Send(new CreateUserCommand(dto));
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred while registering user: {Message}", ex.Message);
            throw;
        }        
    }

    [HttpPost("RegisterAdmin")]
    [Authorize(Policy = SoccerXConstants.PolicySoccerX, Roles = SoccerXConstants.RoleAdmin)]
    public async Task<IActionResult> RegisterAdmin([FromBody] UserCreateDto dto)
    {
        try
        {
            var result = await _mediator.Send(new CreateUserCommand(dto));
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred while registering admin user {Message}", ex.Message);
            throw;
        }        
    }

    [HttpPost("VerifyEmail")]
    public async Task<IActionResult> VerifyEmail([FromBody] string code)
    {
        try
        {            
            var result = await _mediator.Send(new UserVerifyEmailCommand(code));
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred while verifying email: {Message}", ex.Message);
            throw;
        }        
    }

    [HttpPost("SendNewVerifyEmail")]
    public async Task<IActionResult> SendNewVerifyEmail([FromBody] string emptyString)
    {
        try
        {
            var result = await _mediator.Send(new UserResendVerifyEmailCodeCommand());
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred while sending new verification email: {Message}", ex.Message);
            throw;
        }        
    }
    #endregion
}
