using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using SoccerX.Application.Commands.UserCommand;
using SoccerX.Application.Services.CountryService;
using SoccerX.Application.Services.CustomerService;
using SoccerX.Application.Validators.User;
using SoccerX.Common.Configuration;
using System.Reflection;

namespace SoccerX.Application.StartUp
{
    public static class StartUpSoccerXApplication
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method

        public static IServiceCollection AddDependcyCollectionApplication(this IServiceCollection service, ApplicationSettings settings)
        {
            return service                
                .AddScoped<IUserService, UserService>()
                .AddScoped<ICountriesService, CountriesService>()        
                .AddDependcyCollectionValidationManager()
                .AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly);
                });
        }
        #endregion

        #region Private Method
        private static IServiceCollection AddDependcyCollectionValidationManager(this IServiceCollection service)
        {
            return service
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssemblyContaining<UserCreateDtoValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
        #endregion
    }
}
