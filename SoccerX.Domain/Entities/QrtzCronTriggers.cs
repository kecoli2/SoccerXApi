using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class QrtzCronTriggers
{
    public string SchedName { get; set; } = null!;

    public string TriggerName { get; set; } = null!;

    public string TriggerGroup { get; set; } = null!;

    public string CronExpression { get; set; } = null!;

    public string? TimeZoneId { get; set; }

    public virtual QrtzTriggers QrtzTriggers { get; set; } = null!;
}
