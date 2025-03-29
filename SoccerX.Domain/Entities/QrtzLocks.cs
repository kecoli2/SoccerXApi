using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class QrtzLocks
{
    public string SchedName { get; set; } = null!;

    public string LockName { get; set; } = null!;
}
