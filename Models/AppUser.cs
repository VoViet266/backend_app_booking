namespace his_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

[Table("app_users", Schema = "datlichkham")]
public class AppUser
{
    public int? Mand { get; set; }
    public required string SoDienThoai { get; set; } 
    public required string MatKhauHash { get; set; } 
    public required string Holot { get; set; } 
    public required string Ten { get; set; } 
    public bool IsActive { get; set; } = true;
    public DateTimeOffset NgayTao { get; set; } = DateTime.UtcNow;
    public DateTimeOffset? LanDangNhapCuoi { get; set; }
    public ICollection<AppUserHoSo> HoSoLienKets { get; set; } = new List<AppUserHoSo>();
}
