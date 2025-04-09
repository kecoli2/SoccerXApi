using SoccerX.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace SoccerX.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        #region Field
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        #endregion

        #region Constructor
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        #endregion

        #region Public Method
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // request’i devam ettir
            }
            catch (BaseException ex)
            {
                _logger.LogWarning(ex, "Handled exception: {Message}", ex.Message);

                context.Response.StatusCode = (int)ex.StatusCode;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    message = ex.Message,
                    errors = ex.PropertyErrors // null olabilir
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred!");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    message = "Beklenmeyen bir hata oluştu."
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        }
        #endregion

        #region Private Method
        #endregion
    }
}
