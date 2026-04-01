using his_backend.DTOs;
using his_backend.Models;
using his_backend.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace his_backend.Services;


public class AuthService : IAuthService
{
    private readonly AppDbContext            _db;
    private readonly IJwtService             _jwt;
    private readonly ILogger<AuthService>    _logger;

    private const int REFRESH_TOKEN_NGAY      = 30;
    private const int ACCESS_TOKEN_GIAY       = 60 * 60; 

    public AuthService(AppDbContext db, IJwtService jwt, ILogger<AuthService> logger)
    {
        _db     = db;
        _jwt    = jwt;
        _logger = logger;
    }

    public async Task<ServiceResult<NguoiDungInfo>> DangKyAsync(DangKyRequest req)
    {
        var sdt = req.SoDienThoai.Trim();

        bool daTonTai = await _db.AppUsers.AnyAsync(u => u.SoDienThoai == sdt);
        if (daTonTai)
            return ServiceResult<NguoiDungInfo>.Fail(
                $"Số điện thoại {sdt} đã được đăng ký", 409);

        using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            // 1. Tạo User
            var user = new AppUser
            {
                SoDienThoai  = sdt,
                MatKhauHash  = BCrypt.Net.BCrypt.HashPassword(req.MatKhau, workFactor: 12),
                Holot        = req.Holot?.Trim(),
                Ten          = req.Ten?.Trim(),
                
            };

            _db.AppUsers.Add(user);
            await _db.SaveChangesAsync(); 

            // Tìm hoặc tạo hồ sơ bệnh nhân mới dựa vào CMND
            Nguoibenhdangky? hoSo = null;
            bool hoSoCu = false;

        
            if (hoSo is null)
            {
                hoSo = new Nguoibenhdangky
                {
                    Holot        = req.Holot?.Trim() ?? string.Empty,
                    Ten          = req.Ten?.Trim() ?? string.Empty,
                    Sodienthoai  = sdt,
                };  
                _db.Nguoibenhdangkys.Add(hoSo);
            }

            // Tạo liên kết người dùng với hồ sơ
            var lienKet = new AppUserHoSo
            {
                AppUserId    = user.Mand,
                HoSo         = hoSo,
                QuanHe       = "ban_than",
                LaMacDinh    = true,  // Đặt làm thông tin mặc định khi sử dụng app
                NgayLienKet  = DateTimeOffset.UtcNow,
            };

            if (hoSoCu)
            {
                lienKet.HoSoId = hoSo.Id; // Nếu là hồ sơ cũ, ta gán Id luôn
            }

            _db.AppUserHoSos.Add(lienKet);
            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            return ServiceResult<NguoiDungInfo>.Ok(new NguoiDungInfo
            {
                Id              = user.Mand,
                SoDienThoai     = user.SoDienThoai,
                NgayTao         = user.NgayTao,
                LanDangNhapCuoi = user.LanDangNhapCuoi,
                Holot           = user.Holot ?? string.Empty,
                Ten             = user.Ten ?? string.Empty,
                

            }, "Đăng ký tài khoản và tạo hồ sơ thành công", 201);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Lỗi khi đăng ký tài khoản cho SĐT {SDT}", sdt);
            return ServiceResult<NguoiDungInfo>.Fail("Đã có lỗi xảy ra, vui lòng thử lại sau.", 500);
        }
    }
  

    public async Task<ServiceResult<DangNhapResponse>> DangNhapAsync(DangNhapRequest req)
    {
        var sdt  = req.SoDienThoai.Trim();
        var user = await _db.AppUsers.FirstOrDefaultAsync(u => u.SoDienThoai == sdt);

        if (user is null || !BCrypt.Net.BCrypt.Verify(req.MatKhau, user.MatKhauHash))
            return ServiceResult<DangNhapResponse>.Fail(
                "Số điện thoại hoặc mật khẩu không đúng", 401);

        if (!user.IsActive)
            return ServiceResult<DangNhapResponse>.Fail(
                "Tài khoản đã bị khoá, vui lòng liên hệ quản trị viên", 401);

        var accessToken  = _jwt.TaoAccessToken(user);
        var refreshToken = _jwt.TaoRefreshToken();

        user.RefreshToken       = refreshToken;
        user.RefreshTokenHetHan = DateTimeOffset.UtcNow.AddDays(REFRESH_TOKEN_NGAY);
        user.LanDangNhapCuoi    = DateTimeOffset.UtcNow;
        await _db.SaveChangesAsync();

        _logger.LogInformation("Đăng nhập: {SDT} (userId={Id})", sdt, user.Mand);

        return ServiceResult<DangNhapResponse>.Ok(BuildDangNhapResponse(user, accessToken, refreshToken));
    }

    public async Task<ServiceResult<DangNhapResponse>> LamMoiTokenAsync(string refreshToken)
    {
        var user = await _db.AppUsers.FirstOrDefaultAsync(u =>
            u.RefreshToken == refreshToken && u.RefreshTokenHetHan > DateTimeOffset.UtcNow);

        if (user is null)
            return ServiceResult<DangNhapResponse>.Fail(
                "Refresh token không hợp lệ hoặc đã hết hạn, vui lòng đăng nhập lại", 401);

        if (!user.IsActive)
            return ServiceResult<DangNhapResponse>.Fail("Tài khoản đã bị khoá", 401);

        var newAccess  = _jwt.TaoAccessToken(user);
        var newRefresh = _jwt.TaoRefreshToken();

        user.RefreshToken       = newRefresh;
        user.RefreshTokenHetHan = DateTimeOffset.UtcNow.AddDays(REFRESH_TOKEN_NGAY);
        await _db.SaveChangesAsync();

        return ServiceResult<DangNhapResponse>.Ok(BuildDangNhapResponse(user, newAccess, newRefresh));
    }

    public async Task<ServiceResult<object>> DangXuatAsync(int userId)
    {
        var user = await _db.AppUsers.FindAsync(userId);
        if (user is not null)
        {
            user.RefreshToken       = null;
            user.RefreshTokenHetHan = null;
            await _db.SaveChangesAsync();
        }

        _logger.LogInformation("Đăng xuất: userId={Id}", userId);
        return ServiceResult<object>.Ok(null!, "Đăng xuất thành công");
    }


    public async Task<ServiceResult<object>> DoiMatKhauAsync(int userId, DoiMatKhauRequest req)
    {
        var user = await _db.AppUsers.FindAsync(userId);
        if (user is null)
            return ServiceResult<object>.Fail("Không xác định được người dùng", 401);

        if (!BCrypt.Net.BCrypt.Verify(req.MatKhauCu, user.MatKhauHash))
            return ServiceResult<object>.Fail("Mật khẩu cũ không đúng");

        user.MatKhauHash        = BCrypt.Net.BCrypt.HashPassword(req.MatKhauMoi, workFactor: 12);
        user.RefreshToken       = null;
        user.RefreshTokenHetHan = null;
        await _db.SaveChangesAsync();


        return ServiceResult<object>.Ok(null!, "Đổi mật khẩu thành công, vui lòng đăng nhập lại");
    }



    private DangNhapResponse BuildDangNhapResponse(AppUser user, string access, string refresh) =>
        new()
        {
            AccessToken  = access,
            RefreshToken = refresh,
            ExpiresIn    = ACCESS_TOKEN_GIAY,
            NguoiDung    =  new NguoiDungInfo
            {
                Id              = user.Mand,
                SoDienThoai     = user.SoDienThoai,
                NgayTao         = user.NgayTao,
                LanDangNhapCuoi = user.LanDangNhapCuoi,
                Holot           = user.Holot ?? string.Empty,
                Ten             = user.Ten ?? string.Empty,
                
                
            }
        };
}
