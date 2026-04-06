using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace his_backend.Models;

[Table("app_dmchuyenkhoa", Schema = "datlichkham")]
public class Dmchuyenkhoa
{
    [Key]
    [Column("mack")]
    public string Mack { get; set; } = null!;

    [Column("tenck")]
    public string TenCk { get; set; } = null!;

    [Column("motatrieuchung")]
    public string? MoTaTrieuChung { get; set; }

    [Column("image_url")]
    public string? ImageUrl { get; set; }
}