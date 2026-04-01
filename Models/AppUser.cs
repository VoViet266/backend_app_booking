namespace his_backend.Models;

public class AppUser
{
    public int Mand { get; set; }
    public string SoDienThoai { get; set; } = null!;
    public string MatKhauHash { get; set; } = null!;
    public string? Holot { get; set; }
    public string? Ten { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTimeOffset NgayTao { get; set; } = DateTime.UtcNow;
    public DateTimeOffset? LanDangNhapCuoi { get; set; }
    public string? RefreshToken { get; set; }
    public DateTimeOffset? RefreshTokenHetHan { get; set; }
    public ICollection<AppUserHoSo> HoSoLienKets { get; set; } = new List<AppUserHoSo>();
}
