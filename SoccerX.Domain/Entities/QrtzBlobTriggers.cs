using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class QrtzBlobTriggers
{
    public string SchedName { get; set; } = null!;

    public string TriggerName { get; set; } = null!;

    public string TriggerGroup { get; set; } = null!;

    public byte[]? BlobData { get; set; }

    public virtual QrtzTriggers QrtzTriggers { get; set; } = null!;
}
