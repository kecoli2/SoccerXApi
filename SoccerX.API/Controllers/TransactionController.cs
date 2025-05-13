using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SoccerX.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : Controller
    {
        #region Field
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public TransactionController(IMediator mediator, IMapper mapper, ILogger<TransactionController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Private Method
        #endregion
    }
}
