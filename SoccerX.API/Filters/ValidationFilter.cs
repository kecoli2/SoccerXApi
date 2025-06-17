using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SoccerX.Application.Exceptions;

namespace SoccerX.API.Filters
{
    public class ValidationFilter : IActionFilter
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
                if (errors != null)
                {
                    throw new ValidationException(errors!, "Validation failed");
                }
            }
        }
        #endregion

        #region Private Method
        #endregion       
    }
}
