using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace his_backend.Models;

[Table("khambenh", Schema = "current")] 
public partial class Khambenh
{
    public string Mabn { get; set; } = null!;

    public string Makb { get; set; } = null!;

    public string? Maba { get; set; }

    public string? Madv { get; set; }

    public string? Maphong { get; set; }

    public DateTime? Ngaykcb { get; set; }

    public decimal? Sott { get; set; }

    public string? Manv { get; set; }

    public string? Maxt { get; set; }

    public string? Maicd { get; set; }

    public string? Kqcdoan { get; set; }

    public string? Maicdp { get; set; }

    public string? Kqcdoanp { get; set; }

    public string? Mapl { get; set; }

    public decimal? Dakham { get; set; }

    public decimal? Tinhtrang { get; set; }

    public string? Taikhoan { get; set; }

    public string? Tenmay { get; set; }

    public decimal? Xoa { get; set; }

    public DateTime? Ngayxoa { get; set; }

    public string? Thangkt { get; set; }

    public string? Namkt { get; set; }

    public string? Lydo { get; set; }

    public decimal? Ngaybenh { get; set; }

    public string? Mabv { get; set; }

    public string? Sohd { get; set; }

    public decimal? Dain { get; set; }

    public string? Huyetap { get; set; }

    public decimal? Mach { get; set; }

    public decimal? Nhiptho { get; set; }

    public decimal? Chieucao { get; set; }

    public decimal? Vongdau { get; set; }

    public decimal? Vongnguc { get; set; }

    public decimal? Cannang { get; set; }

    public decimal? Nhietdo { get; set; }

    public decimal? Daingiay { get; set; }

    public decimal? Cls { get; set; }

    public DateTime? Giokb { get; set; }

    public decimal? Songaydt { get; set; }

    public string? Khoin { get; set; }

    public decimal? Bant { get; set; }

    public string? Mayhct { get; set; }

    public string? Tenyhct { get; set; }

    public string? ThangQt { get; set; }

    public string? NamQt { get; set; }

    public string? Hb { get; set; }

    public string? Fio2 { get; set; }

    public string? Xutritenga { get; set; }

    public string? Tuvankhac { get; set; }

    public string? ChandoanDautien { get; set; }
}
