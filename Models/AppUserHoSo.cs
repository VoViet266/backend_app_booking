namespace his_backend.Models;

/// <summary>
/// Bảng liên kết nhiều-nhiều giữa AppUser và Nguoibenhdangky.
/// 1 user có thể liên kết nhiều hồ sơ bệnh nhân (bản thân + người thân).
/// 1 hồ sơ có thể chia sẻ giữa nhiều tài khoản.
/// </summary>
public class AppUserHoSo
{
    public int AppUserId { get; set; }
    
    public int HoSoId { get; set; }

    /// <summary>Quan hệ với bệnh nhân: "ban_than", "vo_chong", "con", "cha_me", "anh_chi_em", "khac"</summary>
    public string QuanHe { get; set; } = "ban_than";

    /// <summary>Hồ sơ mặc định của user này không?</summary>
    public bool LaMacDinh { get; set; } = false;

    /// <summary>Ngày user này liên kết hồ sơ</summary>
    public DateTimeOffset NgayLienKet { get; set; } = DateTimeOffset.UtcNow;

    // Navigation properties
    public AppUser AppUser { get; set; } = null!;
    public Nguoibenhdangky HoSo { get; set; } = null!;
}
