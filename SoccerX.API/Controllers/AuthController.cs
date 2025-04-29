using Microsoft.AspNetCore.Mvc;
using SoccerX.API.Controllers.Base;
using SoccerX.Application.Commands.Security;
using SoccerX.Common.Constants;
using SoccerX.Common.Enums;
using SoccerX.DTO.Requests.Security;
using SoccerX.DTO.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace SoccerX.API.Controllers
{
    public class AuthController : BaseApiController
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Actions
        [HttpPost("social-login")]
        [SwaggerResponse(200, type: typeof(AuthResponseDto))]
        public async Task<IActionResult> SocialLogin([FromBody] SocialLoginRequest req)
        {
            try
            {
                var dto = await Mediator.Send(new SocialLoginCommand(req.IdToken, req.Provider, GetHeaderValue<PlatformType>(SoccerXConstants.HeaderXPlatForm, PlatformType.Web)));
                return OkResult(dto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unhandled exception in action SocialLogin");
                throw;
            }
        }

        [HttpPost("local-login")]
        [SwaggerResponse(200, type: typeof(AuthResponseDto))]
        public async Task<IActionResult> LocalLogin([FromBody] LocalLoginRequest req)
        {
            try
            {
                var dto = await Mediator.Send(new LocalLoginCommand(req.EmailOrUserName, req.Password, GetHeaderValue<PlatformType>(SoccerXConstants.HeaderXPlatForm, PlatformType.Web)));
                return OkResult(dto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unhandled exception in action SocialLogin");
                throw;
            }
        }
        #endregion

        #region Private Method
        #endregion
    }
}
