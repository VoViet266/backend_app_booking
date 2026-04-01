using System;
using System.Collections.Generic;

namespace his_backend.Models;

public partial class Ctthuocb
{
    public string? Mahh { get; set; }

    public decimal? Soluong { get; set; }

    public string? Chidinh { get; set; }

    public int? Idtoabs { get; set; }

    public string? LieuDung { get; set; }

    public virtual Dmthuoc? MahhNavigation { get; set; }
}
