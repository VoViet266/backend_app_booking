using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace his_backend.Models;

[Table("dmnhanvien", Schema = "current")]
public class Dmnhanvien
{
    [Key]
    [Column("manv")]
    public string Manv { get; set; } = null!;

    [Column("holot")]
    public string Holot { get; set; } = null!;

    [Column("ten")]
    public string Ten { get; set; } = null!;

    [Column("gioitinh")]
    public decimal? Gioitinh { get; set; }

    [Column("madv")]
    public string Madv { get; set; } = null!;

    [Column("mack")]
    public string Mack { get; set; } = null!;

    [Column("macv")]
    public string Macv { get; set; } = null!;

    [Column("duockham")]
    public string Duockham { get; set; } = null!;

    [Column("trangthai")]
    public string Trangthai { get; set; } = null!;

    public Dmchuyenkhoa ChuyenKhoa { get; set; } = null!;
    public ICollection<BacsiChuyenKhoa> BacsiChuyenKhoas { get; set; } = null!;

}