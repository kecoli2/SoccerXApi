using SoccerX.Application.Interfaces.Cache.Redis;
using SoccerX.Application.Interfaces.Quartz;
using SoccerX.Application.Interfaces.Repository;

namespace SoccerX.Application.Services.CustomerService
{
    public class CustomerService: ICustomerService
    {
        #region Field
        private readonly IUserRepository _userRepository;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IQuartzJobCreater _quartzJobCreater;
        #endregion

        #region Constructor
        public CustomerService(IUserRepository userRepository, IRedisCacheService redisCacheService, IQuartzJobCreater quartzJobCreater)
        {
            _userRepository = userRepository;
            _redisCacheService = redisCacheService;
            _quartzJobCreater = quartzJobCreater;
        }
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
