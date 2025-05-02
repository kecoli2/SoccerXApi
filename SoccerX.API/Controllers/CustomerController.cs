using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoccerX.Common.Constants;

namespace SoccerX.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(policy: SoccerXConstants.PolicySoccerX)]
[AllowAnonymous]
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
    #endregion
}
