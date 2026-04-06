using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace his_backend.Models;

[Table("app_bacsi_chuyenkhoa", Schema = "benhnhan")]
public class BacsiChuyenKhoa
{
    [Key]
    [Column("id")]
    public required int Id { get; set; }

    [Column("manv")]
    public required string Manv { get; set; }

    [Column("mack")]
    public required string Mack { get; set; }

    public Dmnhanvien? NhanVien { get; set; }

    public Dmchuyenkhoa? ChuyenKhoa { get; set; }
}