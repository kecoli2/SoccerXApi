using SoccerX.Common.Base.Quartz.Models;
using SoccerX.Common.Enums;
using System;
using System.Threading.Tasks;

namespace SoccerX.Application.Interfaces.Quartz
{
    public interface IQuartzJobCreaterExtension
    {
        IQuartzJobCreaterExtension SetJobKey(string key);
        IQuartzJobCreaterExtension SetJobKey(string key, JobCategoryEnum category);
        IQuartzJobCreaterExtension SetCriteria(object criteria);
        IQuartzJobCreaterExtension SetCriteria(string key, string value);
        IQuartzJobCreaterExtension SetCriteria(string key, int value);
        IQuartzJobCreaterExtension SetCriteria(string key, bool value);
        IQuartzJobCreaterExtension SetCriteria(string key, float value);
        IQuartzJobCreaterExtension SetCriteria(string key, double value);
        IQuartzJobCreaterExtension SetCriteria(string key, long value);
        IQuartzJobCreaterExtension SetDescription(string description);
        IQuartzJobCreaterExtension SetRecovry(bool recovry);
        IQuartzJobCreaterExtension SetDurably(bool durably);
        IQuartzJobCreaterExtension SetPriority(TriggerPriorityEnum priority);
        IQuartzJobCreaterExtension SetCulture(string? culture);
        IQuartzJobCreaterExtension SetTriggerKey(string triggerKeyName);
        IQuartzJobCreaterExtension SetTriggerKey(string triggerKeyName, string group);
        IQuartzJobCreaterExtension SetUserId(Guid? id);
        IQuartzJobCreaterExtension StartDate(DateTimeOffset startDate);
        IQuartzJobCreaterExtension EndDate(DateTimeOffset endDate);
        IQuartzJobCreaterExtension SetCronExpression(string cronExpression);
        Task<JobDetailModel> Start();
    }
}
