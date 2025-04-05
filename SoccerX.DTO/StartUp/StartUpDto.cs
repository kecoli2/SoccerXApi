using Microsoft.Extensions.DependencyInjection;
using System;

namespace SoccerX.DTO.StartUp
{
    public static class StartUpDto
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method

        public static IServiceCollection AddDependcyCollectionDto(this IServiceCollection service)
        {
            return service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
        #endregion

        #region Private Method
        #endregion
    }
}
