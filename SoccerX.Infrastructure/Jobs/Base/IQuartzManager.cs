using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Infrastructure.Jobs.Base
{
    public interface IQuartzManager
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task ShutdownAsync(CancellationToken cancellationToken);
        IScheduler GetScheduler();
    }
}
