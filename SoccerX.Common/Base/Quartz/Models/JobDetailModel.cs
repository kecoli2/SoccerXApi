namespace SoccerX.Common.Base.Quartz.Models
{
    public class JobDetailModel
    {
        #region Field
        public string JobKey { get; }
        public string JobGroup { get; }
        public string? Description { get; }
        public IDictionary<string, object>? JobMap { get; }
        public bool IsDurable { get; set; }
        public bool PersistJobDataAfterExecution { get; }
        public bool ConcurrentExecutionDisallowed { get; }
        public bool RequestsRecovery { get; }
        public DateTimeOffset? StartingDateTimeOffset { get; }
        #endregion

        #region Constructor

        public JobDetailModel(string jobKey, string jobGroup, string? description, IDictionary<string, object>? jobMap, bool isDurable, bool persistJobDataAfterExecution, bool concurrentExecutionDisallowed, bool requestsRecovery, DateTimeOffset? startingDateTimeOffset = null)
        {
            JobKey = jobKey;
            JobGroup = jobGroup;
            Description = description;
            JobMap = jobMap;
            IsDurable = isDurable;
            PersistJobDataAfterExecution = persistJobDataAfterExecution;
            ConcurrentExecutionDisallowed = concurrentExecutionDisallowed;
            RequestsRecovery = requestsRecovery;
            StartingDateTimeOffset = startingDateTimeOffset;
        }
        #endregion
    }
}
