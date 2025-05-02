using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SoccerX.Application.Commands.UserCommand;
using SoccerX.Application.Services.CountryService;
using SoccerX.Application.Services.CustomerService;
using SoccerX.Application.Validators.User;
using SoccerX.Common.Configuration;

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
                .AddValidatorsFromAssembly(typeof(UserCreateDtoValidator).Assembly)
                .AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly);
                });
        }
        #endregion

        #region Private Method
        #endregion
    }
}
