using System;
using System.Threading.Tasks;
using SoccerX.Common.Base.Quartz;
using SoccerX.Common.Base.Quartz.Models;
using SoccerX.Common.Enums;

namespace SoccerX.Application.Interfaces.Quartz
{
    public interface IQuartzJobCreater
    {
        IQuartzJobCreaterExtension CreateJob<T>() where T : IBaseJob;
        IQuartzJobCreaterExtension Create(Type jobType);
        IQuartzJobCreaterExtension Create(JobKeyEnum jobKeyEnum);
        Task<JobDetailModel> Start();
    }
}
