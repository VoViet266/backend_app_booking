using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace his_backend.Models;

[Table("dmnhanvien", Schema = "benhnhan")]
public class Dmnhanvien
{
    [Key]
    [Column("manv")]
    public string Manv { get; set; }

    [Column("holot")]
    public string Holot { get; set; }

    [Column("ten")]
    public string Ten { get; set; }

    [Column("madv")]
    public string Madv { get; set; }

    [Column("mack")]
    public string Mack { get; set; }

    [Column("macv")]
    public string Macv { get; set; }

    [Column("duockham")]
    public string Duockham { get; set; }

    [Column("trangthai")]
    public string Trangthai { get; set; }

    public Dmchuyenkhoa ChuyenKhoa { get; set; }
    public ICollection<BacsiChuyenKhoa> BacsiChuyenKhoas { get; set; }

}