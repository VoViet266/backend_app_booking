using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace his_backend.Models;

[Table("app_bacsi_chuyenkhoa", Schema = "benhnhan")]
public class BacsiChuyenKhoa
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("manv")]
    public string Manv { get; set; } = null!;

    [Column("mack")]
    public string Mack { get; set; } = null!;

    public Dmnhanvien? NhanVien { get; set; }

    public Dmchuyenkhoa? ChuyenKhoa { get; set; }
}