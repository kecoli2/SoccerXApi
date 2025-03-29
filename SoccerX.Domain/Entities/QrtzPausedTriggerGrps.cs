using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class QrtzPausedTriggerGrps
{
    public string SchedName { get; set; } = null!;

    public string TriggerGroup { get; set; } = null!;
}
