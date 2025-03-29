using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class Schemaversions
{
    public int Schemaversionsid { get; set; }

    public string Scriptname { get; set; } = null!;

    public DateTime Applied { get; set; }
}
