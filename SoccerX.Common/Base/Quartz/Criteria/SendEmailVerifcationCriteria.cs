using SoccerX.Common.Base.Quartz.Criteria.Base;

namespace SoccerX.Common.Base.Quartz.Criteria
{
    public class SendEmailVerifcationCriteria: JobBaseCriteria
    {
        #region Field

        public string ToMailAddress { get; set; } = null!;
        public string UserId { get; set; } = null!;

        #endregion
    }
}
