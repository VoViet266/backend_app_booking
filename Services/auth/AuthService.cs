using his_backend.DTOs;
using his_backend.Models;
using his_backend.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace his_backend.Services;


public class AuthService(AppDbContext db, IJwtService jwt, ILogger<AuthService> logger) : IAuthService
{
    private readonly AppDbContext _db = db;
    private readonly IJwtService _jwt = jwt;
    private readonly ILogger<AuthService> _logger = logger;

    private const int REFRESH_TOKEN_NGAY = 30;
    //năm phút
    private const int ACCESS_TOKEN_GIAY = 5 * 60; //300 giây = 5 phút

    public async Task<ServiceResult<NguoiDungInfo>> DangKyAsync(DangKyRequest req)
    {
        var sdt = req.SoDienThoai.Trim();

        bool userExist = await _db.AppUsers.AnyAsync(u => u.SoDienThoai == sdt);
        if (userExist)
            return ServiceResult<NguoiDungInfo>.Fail(
                $"Số điện thoại {sdt} đã được đăng ký", 409);

        using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            // 1. Tạo User
            var user = new AppUser
            {

                SoDienThoai = sdt,
                Cmnd = req.Cmnd.Trim(),
                MatKhauHash = BCrypt.Net.BCrypt.HashPassword(req.MatKhau, workFactor: 12),
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
                    Holot = req.Holot?.Trim() ?? string.Empty,
                    Ten = req.Ten?.Trim() ?? string.Empty,
                    Sodienthoai = sdt,
                    Cmnd = req.Cmnd?.Trim(),
                };
                _db.Nguoibenhdangkys.Add(hoSo);
            }

            // Tạo liên kết người dùng với hồ sơ
            var lienKet = new AppUserHoSo
            {
                AppUserId = user.Mand ?? 0,
                HoSo = hoSo,
                QuanHe = "ban_than",
                LaMacDinh = true,  // Đặt làm thông tin mặc định khi sử dụng app
                NgayLienKet = DateTimeOffset.UtcNow,
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
                Id = user.Mand,
                SoDienThoai = user.SoDienThoai,
                LanDangNhapCuoi = user.LanDangNhapCuoi,



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
        var sdt = req.SoDienThoai.Trim();
        var user = await _db.AppUsers.FirstOrDefaultAsync(u => u.SoDienThoai == sdt);

        if (user is null || !BCrypt.Net.BCrypt.Verify(req.MatKhau, user.MatKhauHash))
            return ServiceResult<DangNhapResponse>.Fail(
                "Số điện thoại hoặc mật khẩu không đúng", 401);

        if (!user.IsActive)
            return ServiceResult<DangNhapResponse>.Fail(
                "Tài khoản đã bị khoá, vui lòng liên hệ quản trị viên", 401);

        var accessToken = _jwt.TaoAccessToken(user);
        var refreshToken = _jwt.TaoRefreshToken();

        // Nếu thiết bị đã có token → cập nhật, không tạo mới
        var token = await _db.Usertokens
            .FirstOrDefaultAsync(t => t.UserId == user.Mand && t.DeviceId == req.DeviceId);

        if (token != null)
        {
            token.FcmToken = req.FcmToken;
            token.RefreshToken = refreshToken;
            token.RefreshTokenHetHan = DateTimeOffset.UtcNow.AddDays(REFRESH_TOKEN_NGAY);
            token.CreatedAt = DateTime.UtcNow;
        }
        else
        {
            _db.Usertokens.Add(new Usertoken
            {
                UserId = user.Mand ?? 0,
                DeviceId = req.DeviceId,
                FcmToken = req.FcmToken,
                RefreshToken = refreshToken,
                RefreshTokenHetHan = DateTimeOffset.UtcNow.AddDays(REFRESH_TOKEN_NGAY),
                CreatedAt = DateTime.UtcNow,
            });
        }

        await _db.SaveChangesAsync();

        var activeTokens = await _db.Usertokens
            .Where(t => t.UserId == user.Mand)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        if (activeTokens.Count > 4)
        {
            _db.Usertokens.RemoveRange(activeTokens.Skip(4));
            await _db.SaveChangesAsync();
        }

        return ServiceResult<DangNhapResponse>.Ok(BuildDangNhapResponse(user, accessToken, refreshToken));
    }
    public async Task SaveDeviceTokenAsync(int userId, string deviceId, string tokenfcm)
    {
        if (string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(tokenfcm))
            return;

        var existing = await _db.Usertokens
            .FirstOrDefaultAsync(t => t.UserId == userId && t.DeviceId == deviceId);

        if (existing != null)
        {
            existing.FcmToken = tokenfcm;
        }
        else
        {
            _db.Usertokens.Add(new Usertoken
            {
                UserId = userId,
                DeviceId = deviceId,
                FcmToken = tokenfcm
            });
        }

        await _db.SaveChangesAsync();
    }
    public async Task<ServiceResult<DangNhapResponse>> LamMoiTokenAsync(string refreshToken)
    {
        var user = await _db.Usertokens.FirstOrDefaultAsync(u =>
            u.RefreshToken == refreshToken && u.RefreshTokenHetHan > DateTimeOffset.UtcNow);

        if (user is null)
            return ServiceResult<DangNhapResponse>.Fail(
                "Refresh token không hợp lệ hoặc đã hết hạn, vui lòng đăng nhập lại", 401);

        var userApp = await _db.AppUsers.FindAsync(user.UserId);
        if (userApp is null || !userApp.IsActive)
            return ServiceResult<DangNhapResponse>.Fail("Tài khoản đã bị khoá hoặc không tồn tại", 401);

        var newAccess = _jwt.TaoAccessToken(userApp);
        var newRefresh = _jwt.TaoRefreshToken();

        user.RefreshToken = newRefresh;
        user.RefreshTokenHetHan = DateTimeOffset.UtcNow.AddDays(REFRESH_TOKEN_NGAY);
        await _db.SaveChangesAsync();

        return ServiceResult<DangNhapResponse>.Ok(BuildDangNhapResponse(userApp, newAccess, newRefresh));
    }

    public async Task<ServiceResult<object>> DangXuatAsync(int userId)
    {
        var user = await _db.Usertokens
            .FirstOrDefaultAsync(t => t.UserId == userId);
        if (user is not null)
        {
            _db.Usertokens.Remove(user);
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

        // Kiểm tra mật khẩu cũ trước để xác thực quyền sở hữu
        if (!BCrypt.Net.BCrypt.Verify(req.MatKhauCu, user.MatKhauHash))
            return ServiceResult<object>.Fail("Mật khẩu cũ không đúng", 401);

        // Kiểm tra mật khẩu mới có trùng cũ không
        if (BCrypt.Net.BCrypt.Verify(req.MatKhauMoi, user.MatKhauHash))
            return ServiceResult<object>.Fail("Mật khẩu mới không được trùng với mật khẩu cũ", 400);

        // Xóa toàn bộ Token để bắt buộc đăng nhập lại trên mọi thiết bị
        await _db.Usertokens
            .Where(t => t.UserId == userId)
            .ExecuteDeleteAsync();

        user.MatKhauHash = BCrypt.Net.BCrypt.HashPassword(req.MatKhauMoi, workFactor: 12);

        await _db.SaveChangesAsync();

        return ServiceResult<object>.Ok(null!, "Đổi mật khẩu thành công, vui lòng đăng nhập lại");
    }
    public async Task<ServiceResult<object>> verifyAccountAsync(VerifyAccountRequest req)
    {
        var user = await _db.AppUsers.FirstOrDefaultAsync(u => u.SoDienThoai == req.SoDienThoai && u.Cmnd == req.Cmnd);
        if (user is null)
            return ServiceResult<object>.Fail("Không tìm thấy tài khoản với số điện thoại và CMND này");

        return ServiceResult<object>.Ok(null!, "Tài khoản hợp lệ");
    }
    public async Task<ServiceResult<object>> QuenMatKhauAsync(QuenMatKhauRequest req)
    {
        var user = await _db.AppUsers.FirstOrDefaultAsync(u => u.SoDienThoai == req.SoDienThoai && u.Cmnd == req.Cmnd);
        if (user is null)
            return ServiceResult<object>.Fail("Không tìm thấy tài khoản với số điện thoại và CMND này");

        var userToken = await _db.Usertokens
            .FirstOrDefaultAsync(t => t.UserId == user.Mand);
        if (userToken is not null)
        {
            _db.Usertokens.Remove(userToken);
            await _db.SaveChangesAsync();
        }
        user.MatKhauHash = BCrypt.Net.BCrypt.HashPassword(req.MatKhauMoi, workFactor: 12);
        await _db.SaveChangesAsync();


        return ServiceResult<object>.Ok(null!, "Đổi mật khẩu thành công, vui lòng đăng nhập lại");
    }

    private static DangNhapResponse BuildDangNhapResponse(AppUser user, string access, string refresh) =>
        new()
        {
            AccessToken = access,
            RefreshToken = refresh,
            ExpiresIn = ACCESS_TOKEN_GIAY,
            NguoiDung = new NguoiDungInfo
            {
                Id = user.Mand,
                SoDienThoai = user.SoDienThoai,
                LanDangNhapCuoi = user.LanDangNhapCuoi,

            }
        };
}
