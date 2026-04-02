using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace his_backend.Models;

[Table("ctthuocb", Schema = "current")]
public partial class Ctthuocb
{
    public string? Mahh { get; set; }

    public decimal? Soluong { get; set; }

    public string? Chidinh { get; set; }

    public int? Idtoabs { get; set; }

    public string? LieuDung { get; set; }

    public virtual Dmthuoc? MahhNavigation { get; set; }
}
