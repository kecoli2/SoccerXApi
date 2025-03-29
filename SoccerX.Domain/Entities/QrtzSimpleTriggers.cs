using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class QrtzSimpleTriggers
{
    public string SchedName { get; set; } = null!;

    public string TriggerName { get; set; } = null!;

    public string TriggerGroup { get; set; } = null!;

    public long RepeatCount { get; set; }

    public long RepeatInterval { get; set; }

    public long TimesTriggered { get; set; }

    public virtual QrtzTriggers QrtzTriggers { get; set; } = null!;
}
