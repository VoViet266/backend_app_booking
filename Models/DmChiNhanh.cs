namespace his_backend.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("app_dmchinhanh", Schema = "datlichkham")]
public class DmChiNhanh
{
    [Key]
    [Column("macn")]
    public int Macn { get; set; }
    [Column("tencn")]
    public string Tencn { get; set; } = null!;
    [Column("anhdaidien")]
    public string? Anhdaidien { get; set; }
    [Column("diachi")]
    public string? Diachi { get; set; }
    [Column("sodienthoai")]
    public string? Sodienthoai { get; set; }
    [Column("email")]
    public string? Email { get; set; }
    [Column("lat")]
    public string lat { get; set; }
    [Column("lng")]
    public string lng { get; set; }
}