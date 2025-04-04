using SoccerX.Common.Attributes;
using SoccerX.Common.Base.Quartz.Criteria;
using SoccerX.Common.Enums;
using SoccerX.Infrastructure.Jobs.Base;

namespace SoccerX.Infrastructure.Jobs.Jobs.Test
{
    [JobAttributes(JobKeyEnum.SendVerificationMail, JobCategoryEnum.PublicJob,"SendMail","SendMailDesc",typeof(TestJobCriteria), false)]
    public class TestJob: BaseJob<TestJobCriteria>
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public override Task Executing()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Method
        #endregion
    }
}
