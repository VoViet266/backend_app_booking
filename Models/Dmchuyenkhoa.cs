using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace his_backend.Models;

[Table("app_dmchuyenkhoa", Schema = "datlichkham")]
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