using SoccerX.Application.Interfaces.Repository;
using SoccerX.Common.Attributes;
using SoccerX.Common.Base.Quartz.Criteria;
using SoccerX.Common.Enums;
using SoccerX.Domain.Entities;
using SoccerX.Infrastructure.Jobs.Base;

namespace SoccerX.Infrastructure.Jobs.Jobs.Test
{
    [JobAttributes(JobKeyEnum.SendVerificationMail, JobCategoryEnum.PublicJob,"SendMail","SendMailDesc",typeof(SendEmailVerifcationCriteria), false)]
    public class TestJob: BaseJob<SendEmailVerifcationCriteria>
    {
        #region Field
        private readonly IEmailVerificationRepository _emailVerificationRepository;

        public TestJob(IEmailVerificationRepository emailVerificationRepository)
        {
            _emailVerificationRepository = emailVerificationRepository;
        }

        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public override Task Executing()
        {
            var code = new Random().Next(100000, 999999).ToString();
            var eMailVerification = new Emailverification
            {
                Code = code,
                Createdate = DateTime.Now,
                Expiresat = DateTime.Now.AddMinutes(5),
                Isused = false,
                Userid = Guid.Parse(JobCriteria!.UserId),
            };
            return Task.CompletedTask;
        }
        #endregion

        #region Private Method
        #endregion
    }
}
