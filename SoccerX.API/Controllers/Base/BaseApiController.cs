using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace SoccerX.API.Controllers.Base
{
    /// <summary>
    /// API controller base sınıfı. Tüm API controller'ları bundan türetilebilir.
    /// Mediator ve Mapper'a kolay erişim sağlar.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController: ControllerBase
    {
        private IMediator? _mediator;
        /// <summary>
        /// MediatR Mediator örneğine erişim.
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        private ILogger? _logger;
        /// <summary>
        /// Logger örneğine erişim.
        /// </summary>
        protected ILogger Logger => _logger ??= HttpContext.RequestServices
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger(GetType());

        private IMapper? _mapper;
        /// <summary>
        /// AutoMapper örneğine erişim.
        /// </summary>
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();

        /// <summary>
        /// Helper: Ok response with typed result.
        /// </summary>
        protected IActionResult OkResult<T>(T result) => Ok(result);

        /// <summary>
        /// Helper: CreatedAtResponse for newly created resources.
        /// </summary>
        protected CreatedAtActionResult CreatedResult<T>(string actionName, object routeValues, T result) => CreatedAtAction(actionName, routeValues, result);

        /// <summary>
        /// Gelen request header'ından, istenen adıyla bulunan değeri, T tipine dönüştürerek döner.
        /// Enum tipleri de desteklenir. Header bulunamazsa veya dönüştürülemezse defaultValue döner.
        /// </summary>
        protected T GetHeaderValue<T>(string headerName, T defaultValue = default!)
        {
            if (!Request.Headers.TryGetValue(headerName, out var values)) return defaultValue;
            var raw = values.FirstOrDefault();
            if (string.IsNullOrEmpty(raw)) return defaultValue;
            try
            {
                var targetType = typeof(T);
                // Eğer enum tipiyse
                if (targetType.IsEnum)
                {
                    if (Enum.TryParse(targetType, raw, ignoreCase: true, out var enumValue))
                        return (T)enumValue!;
                }
                else
                {
                    // Diğer tipler için TypeConverter kullan
                    var converter = TypeDescriptor.GetConverter(targetType);
                    if (converter != null && converter.IsValid(raw))
                    {
                        return (T)converter.ConvertFromInvariantString(raw)!;
                    }
                }
            }
            catch
            {
                // Dönüşüm başarısız olursa default dönecek
            }
            return defaultValue;
        }
    }
}
