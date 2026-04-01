using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace his_backend.Models;

[Table("bacsi_chuyenkhoa", Schema = "benhnhan")]
public class BacsiChuyenKhoa
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("manv")]
    public string Manv { get; set; }

    [Column("mack")]
    public string Mack { get; set; }

    public Dmnhanvien? NhanVien { get; set; }

    public Dmchuyenkhoa? ChuyenKhoa { get; set; }
}