using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using SoccerX.Common.Base.Quartz.Models;

namespace SoccerX.Application.Interfaces.Quartz
{
    public interface IQuartzManager
    {
        Task StartAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task ShutdownAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task PauseAllJobs(CancellationToken cancellationToken = default(CancellationToken));
        Task PauseJob(string jobKey, string jobGroup, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAllJobs(NameValueCollection jobKeys, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> CheckExistJob(string jobKey, string jobGroup, CancellationToken  cancellationToken = default(CancellationToken));
        Task<JobDetailModel?> GetJobDetail(string jobKey, string jobGroup, CancellationToken cancellationToken = default(CancellationToken));
        Task PauseTrigger(string triggerKey, CancellationToken cancellationToken = default(CancellationToken));
        Task ResumeTrigger(string triggerKey, CancellationToken cancellationToken = default(CancellationToken));
        Task ResumeErrorTrigger(CancellationToken  cancellationToken);
        Task<bool> IsJobGroupPaused(string groupName, CancellationToken cancellationToken = default(CancellationToken));
        public void SetJobFactory(object factory);
    }
}
