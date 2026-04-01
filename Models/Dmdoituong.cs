using System;
using System.Collections.Generic;

namespace his_backend.Models;

public partial class Dmdoituong
{
    public string Madt { get; set; } = null!;

    public string Tendt { get; set; } = null!;

    public decimal? Tienck { get; set; }

    public decimal? Tienckdv { get; set; }

    public decimal? Bhyt { get; set; }

    public decimal? Cothe { get; set; }

    public decimal? Tienskb { get; set; }

    public string? Macls { get; set; }

    public string? MaMedisoft { get; set; }

    public decimal? IdSytHcm { get; set; }

    public decimal? Xoa { get; set; }

    public decimal? Kttck { get; set; }

    public decimal? Ktdongchitra01 { get; set; }

    public decimal? Sdnguonkhac { get; set; }

    public decimal? CccdRequired { get; set; }
}
