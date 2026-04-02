using Microsoft.EntityFrameworkCore;
using his_backend.Models;
using his_backend.DTOs;
using his_backend.Common;
using System.Transactions;

namespace his_backend.Services;


public class HoSoBenhNhanService : IHoSoBenhNhanService
{
    private readonly AppDbContext _db;
    private readonly ILogger<HoSoBenhNhanService> _logger;

    public HoSoBenhNhanService(AppDbContext db, ILogger<HoSoBenhNhanService> logger)
    {
        _db = db;
        _logger = logger;
    }
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
        // 1. Kiểm tra user tồn tại
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

            // nếu tìm thấy hoSo thì hoSoCu = true
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
        // 3. Nếu không có CMND -- luôn tạo mới
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

        // Nếu chọn mặc định - bỏ mặc định hồ sơ khác
        if (req.LaMacDinh == true)
            await BoPhuongMacDinh(userId);

        // Tạo liên kết user - hồ sơ
        // Tạo liên kết người dùng với hồ sơ
        var lienKet = new AppUserHoSo
        {
            AppUserId = userId,
            HoSo = hoSo,
            QuanHe = req.QuanHe ?? "ban_than",
            LaMacDinh = req.LaMacDinh ?? false,  // Đặt làm thông tin mặc định khi sử dụng app
            NgayLienKet = DateTimeOffset.UtcNow,
        };

        if (hoSoCu)
        {
            lienKet.HoSoId = hoSo.Id; // Nếu là hồ sơ cũ, ta gán Id luôn
        }

        _db.AppUserHoSos.Add(lienKet);
        await _db.SaveChangesAsync();

        // await Transaction.CommitAsync();

        // 5. Load lại navigation để trả về
        await _db.Entry(lienKet)
            .Reference(x => x.HoSo)
            .LoadAsync();

        _logger.LogInformation(
            "User {UserId} liên kết hồ sơ #{HoSoId} ({HoTen}) - {TrangThai}",
            userId,
            hoSo.Id,
            $"{hoSo.Holot} {hoSo.Ten}",
            hoSoCu ? "hồ sơ cũ" : "hồ sơ mới");

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


    public async Task<ServiceResult<bool>> XoaLienKetAsync(int userId, int hoSoId)
    {
        var lienKet = await _db.AppUserHoSos
            .FirstOrDefaultAsync(lk => lk.AppUserId == userId && lk.HoSoId == hoSoId);

        if (lienKet is null)
            return ServiceResult<bool>.Fail("Không tìm thấy hồ sơ", 404);

        _db.AppUserHoSos.Remove(lienKet);
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {UserId} bỏ hồ sơ #{HoSoId} khỏi danh sách", userId, hoSoId);

        return ServiceResult<bool>.Ok(true, "Đã xóa hồ sơ khỏi danh sách của bạn");
    }

    // ─── Đặt mặc định ─────────────────────────────────────────────────────────

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
        Cmnd = lk.HoSo.Cmnd,
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
