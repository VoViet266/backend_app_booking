using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace his_backend.Models;

[Table("dmchuyenkhoa", Schema = "benhnhan")]
public class Dmchuyenkhoa
{
    [Key]
    [Column("mack")]
    public string Mack { get; set; }

    [Column("tenck")]
    public string TenCk { get; set; }

    [Column("motatrieuchung")]
    public string? MoTaTrieuChung { get; set; }

}