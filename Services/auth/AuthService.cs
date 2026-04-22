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

    [Obsolete]
    public async Task<ServiceResult<NguoiDungInfo>> DangKyAsync(DangKyRequest req)
    {
        var sdt = req.SoDienThoai.Trim();
        var cmnd = req.Cmnd?.Trim();

        // 1. Kiểm tra tài khoản đã tồn tại chưa
        var userExist = await _db.AppUsers.FirstOrDefaultAsync(u => u.SoDienThoai == sdt);
        if (userExist != null && isVeryfiedCmnd(cmnd, userExist.Cmnd))
            return ServiceResult<NguoiDungInfo>.Fail($"Số điện thoại {sdt} với CMND {cmnd} đã tồn tại", 409);

        using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            // 2. Tạo User (Tài khoản đăng nhập)
            var user = new AppUser
            {
                SoDienThoai = sdt,
                Cmnd = BCrypt.Net.BCrypt.HashString(cmnd ?? string.Empty, workFactor: 12), // Hash CMND để bảo mật, nếu có
                MatKhauHash = BCrypt.Net.BCrypt.HashPassword(req.MatKhau, workFactor: 12),
                NgayTao = DateTimeOffset.UtcNow
            };

            _db.AppUsers.Add(user);
            await _db.SaveChangesAsync();

            // 3. Tìm hoặc tạo hồ sơ bệnh nhân (Nguoibenhdangky)
            // Kiểm tra xem CMND này đã tồn tại trong hệ thống hồ sơ chưa
            Nguoibenhdangky? hoSo = null;
            if (!string.IsNullOrEmpty(cmnd))
            {
                hoSo = await _db.Nguoibenhdangkys
                                .FirstOrDefaultAsync(h => h.Cmnd == cmnd);
            }

            bool laHoSoMoi = false;
            if (hoSo is null)
            {
                laHoSoMoi = true;
                hoSo = new Nguoibenhdangky
                {
                    Holot = req.Holot?.Trim() ?? string.Empty,
                    Ten = req.Ten?.Trim() ?? string.Empty,
                    Sodienthoai = sdt,
                    Cmnd = cmnd,
                };
                _db.Nguoibenhdangkys.Add(hoSo);
                // Lưu lần 2 để lấy hoSo.Id nếu là tạo mới
                await _db.SaveChangesAsync();
            }

            // 4. Tạo liên kết người dùng với hồ sơ
            var lienKet = new AppUserHoSo
            {
                AppUserId = user.Mand, // Đã có giá trị nhờ SaveChanges ở bước 2
                HoSoId = hoSo.Id,      // Đã có giá trị nhờ SaveChanges ở bước 3 (hoặc lấy từ hồ sơ cũ)
                QuanHe = "ban_than",
                LaMacDinh = true,
                NgayLienKet = DateTimeOffset.UtcNow,
            };

            _db.AppUserHoSos.Add(lienKet);
            await _db.SaveChangesAsync();

            // Đích đến cuối cùng: Chốt mọi thay đổi
            await transaction.CommitAsync();

            return ServiceResult<NguoiDungInfo>.Ok(new NguoiDungInfo
            {
                Id = user.Mand,
                SoDienThoai = user.SoDienThoai,
            }, laHoSoMoi ? "Đăng ký tài khoản và tạo hồ sơ mới thành công" : "Đăng ký thành công, đã kết nối với hồ sơ cũ của bạn", 201);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Lỗi khi đăng ký tài khoản cho SĐT {SDT}", sdt);
            return ServiceResult<NguoiDungInfo>.Fail("Đã có lỗi xảy ra, vui lòng thử lại sau.", 500);
        }
    }

    private bool isVeryfiedCmnd(string? cmnd, string? cmndHash)
    {
        if (string.IsNullOrEmpty(cmnd) || string.IsNullOrEmpty(cmndHash))
            return false;

        return BCrypt.Net.BCrypt.Verify(cmnd, cmndHash);
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
                UserId = user.Mand,
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
    
        var user = await _db.AppUsers.FirstOrDefaultAsync(u => u.SoDienThoai == req.SoDienThoai );
        if (user is null || !isVeryfiedCmnd(req.Cmnd, user.Cmnd))
            return ServiceResult<object>.Fail($"Không tìm thấy tài khoản với số {req.SoDienThoai} và CMND {req.Cmnd} này!");

        return ServiceResult<object>.Ok(null!, "Tài khoản hợp lệ");
    }
    public async Task<ServiceResult<object>> QuenMatKhauAsync(QuenMatKhauRequest req)
    {

        var user = await _db.AppUsers.FirstOrDefaultAsync(u => u.SoDienThoai == req.SoDienThoai);
      
        var userToken = await _db.Usertokens
            .FirstOrDefaultAsync(t => t.UserId == user.Mand);
        if (userToken != null)
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
