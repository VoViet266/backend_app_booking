using Microsoft.EntityFrameworkCore;
using his_backend.Models;
using his_backend.DTOs;
using his_backend.Common;

namespace his_backend.Services;

public interface IUserService
{
    Task<ServiceResult<NguoiDungInfo>> LayThongTinAsync(int userId);
    Task<ServiceResult<NguoiDungInfo>> LayUser(int userId);
}

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    private readonly ILogger<UserService> _logger;

    public UserService(AppDbContext db, ILogger<UserService> logger)
    {
        _db     = db;
        _logger = logger;
    }

    public async Task<ServiceResult<NguoiDungInfo>> LayThongTinAsync(int userId)
    {
       var user = await _db.AppUserHoSos
        .Where(x => x.AppUserId == userId && x.LaMacDinh == true)
        .Join(
            _db.Nguoibenhdangkys,
            auh => auh.HoSoId,
            n => n.Id,
            (auh, n) => new
            {
                n.Holot,
                n.Ten,
                n.Sodienthoai
            }
        )
        .FirstOrDefaultAsync(); 
        if (user is null)
            return ServiceResult<NguoiDungInfo>.Fail("Không tìm thấy người dùng", 404);

        return ServiceResult<NguoiDungInfo>.Ok(new NguoiDungInfo
        {
            SoDienThoai     = user.Sodienthoai,
            Holot           = user.Holot ?? string.Empty,
            Ten             = user.Ten ?? string.Empty,
        });
    }

    public async Task<ServiceResult<NguoiDungInfo>> LayUser(int userId)
    {
        var user = await _db.AppUsers
        .Where(x => x.Mand == userId)
        .FirstOrDefaultAsync();
        if (user is null)
            return ServiceResult<NguoiDungInfo>.Fail("Không tìm thấy người dùng", 404);

        return ServiceResult<NguoiDungInfo>.Ok(new NguoiDungInfo
        {
            SoDienThoai     = user.SoDienThoai,
            Holot           = user.Holot ?? string.Empty,
            Ten             = user.Ten ?? string.Empty,
        });
    }

}