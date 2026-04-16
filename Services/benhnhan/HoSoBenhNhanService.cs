using Microsoft.EntityFrameworkCore;
using his_backend.Models;
using his_backend.DTOs;
using his_backend.Common;
using System.Transactions;

namespace his_backend.Services;


public class HoSoBenhNhanService(AppDbContext db, ILogger<HoSoBenhNhanService> logger) : IHoSoBenhNhanService
{
    private readonly AppDbContext _db = db;
    private readonly ILogger<HoSoBenhNhanService> _logger = logger;

    public async Task<ServiceResult<List<HoSoBenhNhanResponse>>> LayDanhSachAsync(int userId)
    {
        var lienKets = await _db.AppUserHoSos
            .AsNoTracking()
            .Include(lk => lk.HoSo)
            .Where(lk => lk.AppUserId == userId)
            .OrderByDescending(lk => lk.LaMacDinh)
            .ThenBy(lk => lk.NgayLienKet)
            .ToListAsync();

        var result = lienKets.Select(lk => MapToResponse(lk)).ToList();
        return ServiceResult<List<HoSoBenhNhanResponse>>.Ok(result);
    }


    public async Task<ServiceResult<HoSoBenhNhanResponse>> LayChiTietAsync(int userId, int hoSoId)
    {
        var lienKet = await _db.AppUserHoSos
            .AsNoTracking()
            .Include(lk => lk.HoSo)
            .FirstOrDefaultAsync(lk => lk.AppUserId == userId && lk.HoSoId == hoSoId);

        if (lienKet is null)
            return ServiceResult<HoSoBenhNhanResponse>.Fail("Không tìm thấy hồ sơ", 404);

        return ServiceResult<HoSoBenhNhanResponse>.Ok(MapToResponse(lienKet));
    }

    /// <summary>
    /// Logic chính:
    /// Nếu user cung cấp CMND và CMND đó đã có trong DB -> liên kết tới hồ sơ cũ (không tạo mới)
    /// Nếu chưa có -> tạo hồ sơ mới rồi liên kết
    /// Nếu user đã liên kết hồ sơ này rồi -> trả về lỗi "đã thêm"
    /// </summary>
    public async Task<ServiceResult<HoSoBenhNhan>> ThemHoSoAsync(int userId, ThemHosoRequest req)
    {
        var userExist = await _db.AppUsers.AnyAsync(u => u.Mand == userId);
        if (!userExist)
            return ServiceResult<HoSoBenhNhan>.Fail("Không tìm thấy tài khoản", 404);

        Nguoibenhdangky hoSo;
        bool hoSoCu = false;

        // Tìm hồ sơ theo CMND
        if (!string.IsNullOrWhiteSpace(req.Cmnd))
        {
            var cmnd = req.Cmnd.Trim();

            hoSo = await _db.Nguoibenhdangkys.FirstOrDefaultAsync(x => x.Cmnd == cmnd);

            if (hoSo != null)
            {
                hoSoCu = true;
                // Kiểm tra user đã liên kết chưa
                var daLienKet = await _db.AppUserHoSos
                    .AnyAsync(x => x.AppUserId == userId && x.HoSoId == hoSo.Id);

                if (daLienKet)
                    return ServiceResult<HoSoBenhNhan>.Fail(
                        $"Hồ sơ với CMND '{cmnd}' đã tồn tại trong danh sách của bạn", 409);
            }
            else
            {   // tạo mới
                hoSo = new Nguoibenhdangky
                {
                    Holot = req.Holot,
                    Ten = req.Ten,
                    Ngaysinh = req.Ngaysinh,
                    Gioitinh = req.Gioitinh,
                    Diachi = req.Diachi,
                    Cmnd = req.Cmnd,
                    Ngaycap = req.Ngaycap,
                    Noicap = req.Noicap,
                    Maloaigiayto = req.Maloaigiayto,
                    Maqg = req.Maqg,
                    NhomMau = req.NhomMau,
                    Mathe = req.Mathe,
                    Madt = req.Madt,
                    Manghe = req.Manghe,
                    Maxa = req.Maxa,
                    Matinh = req.Matinh
                };
                _db.Nguoibenhdangkys.Add(hoSo);
            }
        }
        else
        {
            //
            hoSo = new Nguoibenhdangky
            {
                Holot = req.Holot,
                Ten = req.Ten,
                Ngaysinh = req.Ngaysinh,
                Gioitinh = req.Gioitinh,
                Diachi = req.Diachi,
                Cmnd = req.Cmnd,
                Ngaycap = req.Ngaycap,
                Noicap = req.Noicap,
                Maloaigiayto = req.Maloaigiayto,
                Maqg = req.Maqg,
                NhomMau = req.NhomMau,
                Mathe = req.Mathe,
                Madt = req.Madt,
                Manghe = req.Manghe,
                Maxa = req.Maxa,
                Matinh = req.Matinh
            };
            _db.Nguoibenhdangkys.Add(hoSo);
        }

      
        var chuaCoHoSo = !await _db.AppUserHoSos.AnyAsync(lk => lk.AppUserId == userId);

        bool seLaMacDinh;
        if (chuaCoHoSo)
        {
            // Hồ sơ đầu tiên luôn là mặc định
            seLaMacDinh = true;
        }
        else
        {
            // chỉ set mặc định nếu user chọn
            seLaMacDinh = req.LaMacDinh == true;
        }

        if (seLaMacDinh)
            await BoPhuongMacDinh(userId);


        var lienKet = new AppUserHoSo
        {
            AppUserId = userId,
            HoSo = hoSo,
            QuanHe = req.QuanHe ?? "ban_than",
            LaMacDinh = seLaMacDinh,
            NgayLienKet = DateTimeOffset.UtcNow,
        };
        if (hoSoCu)
        {
            lienKet.HoSoId = hoSo.Id; // Nếu là hồ sơ cũ, ta gán Id luôn
        }

        _db.AppUserHoSos.Add(lienKet);
        await _db.SaveChangesAsync();
        await _db.Entry(lienKet)
            .Reference(x => x.HoSo)
            .LoadAsync();

        var msg = hoSoCu
            ? "Đã liên kết hồ sơ có sẵn"
            : "Thêm hồ sơ mới thành công";

        return ServiceResult<HoSoBenhNhan>
            .Ok(MapToResponse(lienKet), msg);
    }


    /// <summary>
    /// Chỉ cập nhật QuanHe và LaMacDinh (metadata liên kết).
    /// Không cho phép sửa thông tin hồ sơ (tên, CMND...) vì hồ sơ có thể chia sẻ.
    /// </summary>
    // public async Task<ServiceResult<HoSoBenhNhan>> CapNhatLienKetAsync(
    //     int userId, int hoSoId, HoSoBenhNhan req)
    // {
    //     var lienKet = await _db.AppUserHoSos
    //         .Include(lk => lk.HoSo)
    //         .FirstOrDefaultAsync(lk => lk.AppUserId == userId && lk.HoSoId == hoSoId);

    //     if (lienKet is null)
    //         return ServiceResult<HoSoBenhNhan>.Fail("Không tìm thấy hồ sơ", 404);

    //     // if (req.LaMacDinh == true && !lienKet.LaMacDinh )
    //     //     await BoPhuongMacDinh(userId);

    //     lienKet.QuanHe    = req.QuanHe ?? lienKet.QuanHe;
    //     lienKet.LaMacDinh = req.LaMacDinh ?? false;

    //     await _db.SaveChangesAsync();

    // _logger.LogInformation("User {UserId} cập nhật liên kết hồ sơ #{HoSoId}", userId, hoSoId);

    //     return ServiceResult<HoSoBenhNhanResponse>.Ok(MapToResponse(lienKet), "Cập nhật thành công");
    // }
    public async Task<ServiceResult<HoSoBenhNhan>> CapNhatHoSoAsync(int hoSoId, HoSoBenhNhan req)
    {
        var hoSo = await _db.Nguoibenhdangkys
            .FirstOrDefaultAsync(h => h.Id == hoSoId);

        if (hoSo is null)
            return ServiceResult<HoSoBenhNhan>.Fail("Không tìm thấy hồ sơ", 404);
        // cập nhật thông tin hồ sơ
        hoSo.Holot = req.Holot;
        hoSo.Ten = req.Ten;
        hoSo.Cmnd  = req.Cmnd;
        hoSo.Ngaysinh = req.Ngaysinh;
        hoSo.Gioitinh = req.Gioitinh;
        hoSo.Sodienthoai = req.Sodienthoai;
        hoSo.Diachi = req.Diachi;
        hoSo.Ngaycap = req.Ngaycap;
        hoSo.Noicap = req.Noicap;
        hoSo.Maloaigiayto = req.Maloaigiayto;
        hoSo.Maqg = req.Maqg;
        hoSo.NhomMau = req.NhomMau;
        hoSo.Mathe = req.Mathe;
        hoSo.Madt = req.Madt;
        hoSo.Manghe = req.Manghe;
        hoSo.Maxa = req.Maxa;
        hoSo.Matinh = req.Matinh;

        
        await _db.SaveChangesAsync();
        return ServiceResult<HoSoBenhNhan>.Ok(req, "Cập nhật hồ sơ thành công");
    }


    public async Task<ServiceResult<bool>> XoaHoSoAsync(int userId, int hoSoId)
    {
            var lienKet = await _db.AppUserHoSos
                .FirstOrDefaultAsync(lk => lk.AppUserId == userId && lk.HoSoId == hoSoId);

            if (lienKet is null)
                return ServiceResult<bool>.Fail("Không tìm thấy hồ sơ", 404);

            _db.AppUserHoSos.Remove(lienKet);
            
            var coLichKham = await _db.DangKyKhams.AnyAsync(dk => dk.Mandk == hoSoId);
            
            if (!coLichKham)
            {
                var hoSo = await _db.Nguoibenhdangkys
                    .FirstOrDefaultAsync(h => h.Id == hoSoId);
                
                var coUserKhacLienKet = await _db.AppUserHoSos
                    .AnyAsync(lk => lk.HoSoId == hoSoId && lk.AppUserId != userId);

                if (hoSo is not null && !coUserKhacLienKet)
                {
                    _db.Nguoibenhdangkys.Remove(hoSo);
                }
            }

            await _db.SaveChangesAsync();

            _logger.LogInformation("User {UserId} xóa hồ sơ #{HoSoId} khỏi danh sách", userId, hoSoId);

            return ServiceResult<bool>.Ok(true, "Đã xóa hồ sơ khỏi danh sách của bạn");
    }

    public async Task<ServiceResult<HoSoBenhNhanResponse>> DatMacDinhAsync(int userId, int hoSoId)
    {
        var lienKet = await _db.AppUserHoSos
            .Include(lk => lk.HoSo)
            .FirstOrDefaultAsync(lk => lk.AppUserId == userId && lk.HoSoId == hoSoId);

        if (lienKet is null)
            return ServiceResult<HoSoBenhNhanResponse>.Fail("Không tìm thấy hồ sơ", 404);

        await BoPhuongMacDinh(userId);
        lienKet.LaMacDinh = true;
        await _db.SaveChangesAsync();

        return ServiceResult<HoSoBenhNhanResponse>.Ok(MapToResponse(lienKet), "Đã đặt làm hồ sơ mặc định");
    }

    private static Nguoibenhdangky TaoHoSoMoi(HoSoBenhNhan req) => new()
    {
        Holot = req.Holot.Trim(),
        Ten = req.Ten.Trim(),
        Ngaysinh = req.Ngaysinh,
        Gioitinh = req.Gioitinh,
        Diachi = req.Diachi?.Trim(),
        Sodienthoai = req.Sodienthoai?.Trim(),
        Cmnd = string.IsNullOrWhiteSpace(req.Cmnd) ? null : req.Cmnd.Trim(),
        Ngaycap = req.Ngaycap,
        Noicap = req.Noicap?.Trim(),
        Maloaigiayto = req.Maloaigiayto?.Trim(),
        Maqg = req.Maqg ?? "VN",
        NhomMau = req.NhomMau?.Trim(),
        Mathe = req.Mathe?.Trim(),
        Madt = req.Madt?.Trim(),
        Manghe = req.Manghe?.Trim(),
        Maxa = req.Maxa?.Trim(),
        Matinh = req.Matinh?.Trim(),
    };

    private async Task BoPhuongMacDinh(int userId)
    {
        await _db.AppUserHoSos
            .Where(lk => lk.AppUserId == userId && lk.LaMacDinh)
            .ExecuteUpdateAsync(s => s.SetProperty(lk => lk.LaMacDinh, false));
    }

    private static HoSoBenhNhanResponse MapToResponse(AppUserHoSo lk) => new()
    {
        Id = lk.HoSoId,
        QuanHe = lk.QuanHe,
        LaMacDinh = lk.LaMacDinh,
        NgayLienKet = lk.NgayLienKet,
        HoTen = $"{lk.HoSo.Holot} {lk.HoSo.Ten}".Trim(),
        Holot = lk.HoSo.Holot,
        Ten = lk.HoSo.Ten,
        Ngaysinh = lk.HoSo.Ngaysinh,
        Gioitinh = lk.HoSo.Gioitinh,
        Diachi = lk.HoSo.Diachi,
        Sodienthoai = lk.HoSo.Sodienthoai,
        Cmnd = lk.HoSo.Cmnd ?? "",
        Ngaycap = lk.HoSo.Ngaycap,
        Noicap = lk.HoSo.Noicap,
        Maloaigiayto = lk.HoSo.Maloaigiayto,
        Maqg = lk.HoSo.Maqg,
        NhomMau = lk.HoSo.NhomMau,
        Mathe = lk.HoSo.Mathe,
        Madt = lk.HoSo.Madt,
        Manghe = lk.HoSo.Manghe,
        Maxa = lk.HoSo.Maxa,
        Matinh = lk.HoSo.Matinh,
    };


}
