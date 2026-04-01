using System;
using System.Collections.Generic;

namespace his_backend.Models;

public partial class Dmicd
{
    public string Maicd { get; set; } = null!;

    public string? Tenviet { get; set; }

    public string? Tenanh { get; set; }

    public string? Tenrieng { get; set; }

    public string? Manhom { get; set; }

    public string? Mapl { get; set; }

    public decimal? Ma15 { get; set; }

    public string? ManhomBcByt3970 { get; set; }

    public string? Machuong3970 { get; set; }

    public string? Tenchuong3970 { get; set; }

    public string? Manhom3970 { get; set; }

    public string? Tennhom3970 { get; set; }

    public string? Sttchuong3970 { get; set; }

    public string? Maloai3970 { get; set; }

    public string? Tenloai3970 { get; set; }

    public decimal? Xoa { get; set; }

    public decimal? CdcLoaitru { get; set; }

    public decimal? Capchuyenmon { get; set; }

    public decimal? Giatri1nam { get; set; }

    public decimal? Tt26 { get; set; }

    public string? IdTmd { get; set; }

    public DateTime? NgaytaoTmd { get; set; }

    public DateTime? NgaycapnhatTmd { get; set; }

    public decimal NgayuongMin { get; set; }

    public decimal NgayuongMax { get; set; }
}
