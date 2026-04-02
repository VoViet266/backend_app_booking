using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace his_backend.Models;

[Table("dmbo", Schema = "current")]
public partial class Dmbo
{
    public string Mabo { get; set; } = null!;

    public string? Tenbo { get; set; }

    public string? Madvhd { get; set; }
}
