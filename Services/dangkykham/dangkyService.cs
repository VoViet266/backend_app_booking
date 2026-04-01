using Microsoft.EntityFrameworkCore;
using his_backend.Models;
using his_backend.DTOs;
using his_backend.Common;

namespace his_backend.Services.dangkykham;


public interface IDangkykbService
{
    Task<ServiceResult<DatLichKhamResponse>> DatLichAsync(
        DatLichKhamRequest req,
        int? userId = null);
    Task<ServiceResult<List<LichDaDatResponse>>> LayDanhSachLichAsync(int userId);
    Task<ServiceResult<LichDaDatResponse>> LayChiTietLichAsync(int maDk, int? userId = null);
    Task<ServiceResult<bool>> HuyLichAsync(int maDk, HuyLichRequest req, int? userId = null);
}


public class DangkykbService : IDangkykbService
{
    private readonly AppDbContext _db;
    private readonly ILogger<DangkykbService> _logger;

    public DangkykbService(AppDbContext db, ILogger<DangkykbService> logger)
    {
        _db     = db;
        _logger = logger;
    }
public async Task<ServiceResult<DatLichKhamResponse>> DatLichAsync(
    DatLichKhamRequest req,
    int? userId = null)
{
    var now = DateTime.UtcNow;

    var chuyenKhoa = await _db.Dmchuyenkhoas
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Mack == req.MaCk);

    if (chuyenKhoa == null)
        return ServiceResult<DatLichKhamResponse>.Fail(
            $"Chuyên khoa '{req.MaCk}' không tồn tại", 400);
    string? tenBacSi = null;

    if (req.Mabs != null)
    {
        var bacSi = await _db.BacsiChuyenKhoas
            .Include(x => x.NhanVien)
            .FirstOrDefaultAsync(x =>
                x.Manv == req.Mabs.ToString() &&
                x.Mack == req.MaCk);

        if (bacSi == null)
            return ServiceResult<DatLichKhamResponse>.Fail(
                "Bác sĩ không tồn tại hoặc không thuộc chuyên khoa", 400);

        tenBacSi = bacSi.NhanVien != null
            ? $"{bacSi.NhanVien.Holot} {bacSi.NhanVien.Ten}".Trim()
            : null;
    }

    Nguoibenhdangky? benhNhan = null;

    if (!string.IsNullOrEmpty(req.Cmnd))
    {
        benhNhan = await _db.Nguoibenhdangkys
            .FirstOrDefaultAsync(x => x.Cmnd == req.Cmnd);
    }   
    else if (!string.IsNullOrEmpty(req.Sdt))
    {
        benhNhan = await _db.Nguoibenhdangkys
            .FirstOrDefaultAsync(x => x.Sodienthoai == req.Sdt);
    }
    var parts = req.HoTen.Trim().Split(' ');
    string ten = parts.Last(); 
    string hoLot = string.Join(" ", parts.Take(parts.Length - 1));
    if (benhNhan == null)
    {
        benhNhan = new ()
        {
            Holot = hoLot ?? "", 
            Ten = ten ?? "",
            Ngaysinh = req.Ngaysinh,
            Gioitinh = req.Gioitinh,
            Diachi = req.Diachi ?? "",
            Sodienthoai = req.Sdt ?? "",
            Cmnd = req.Cmnd ?? "",
            Mathe = req.Mathe ?? "",
        };

        _db.Nguoibenhdangkys.Add(benhNhan);
        await _db.SaveChangesAsync();
    }

    if (req.Mabs != null && req.TimeSlot != null)
    {
        var timeSlotUtc = req.TimeSlot.Value.UtcDateTime;

        var trungLich = await _db.DangKyKhams.AnyAsync(x =>
            x.Mabs == req.Mabs &&
            x.Ngay == req.Ngay &&
            x.MaCk == req.MaCk &&
            x.TimeSlot == timeSlotUtc &&
            x.TrangThai != 2 &&
            !x.Xoa);

        if (trungLich)
            return ServiceResult<DatLichKhamResponse>.Fail(
                "Khung giờ này đã được đặt", 409);
    }

    
    var dangKy = new DangKyKham
    {
        Mandk = benhNhan.Id,
        Mapk = req.Mapk,
        Mabs = req.Mabs,
        MaCk = req.MaCk,

        Hoten = req.HoTen,
        Diachi = req.Diachi ?? "",
        Sdt = req.Sdt ?? "",
        Cmnd = req.Cmnd ?? "",

        LoaiQh = req.LoaiQh ?? "",
        HoTenQh = req.HotenQh ?? "",
        DienThoaiQh = req.DienThoaiQh ?? "",
        DiachiQh = req.DiachiQh ?? "",

        Ngaysinh = req.Ngaysinh,
        TimeSlot = req.TimeSlot?.UtcDateTime ?? now,
        Ngay = req.Ngay,
        NgaySua = now,

        GiaTien = 0,
        TrangThai = 0,

        LoaiKham = req.LoaiKham ?? "ngoai_tru",
        GhiChu = req.GhiChu ?? "",

        Mathe = req.Mathe ?? "",

        Phikham = 0,
        Phidv = 0,
        Phithuoc = 0,

        Status = "pending",
        Xoa = false,

        HisId = 0,
        MngthisId = "",
        HiqrCode = ""
    };

    _db.DangKyKhams.Add(dangKy);
    await _db.SaveChangesAsync();


    var res = new DatLichKhamResponse
    {
        MaDk = dangKy.MaDk,
        HoTen = dangKy.Hoten,
        Ngaysinh = dangKy.Ngaysinh,
        Ngay = dangKy.Ngay,
        TimeSlot = req.TimeSlot,
        MaCk = dangKy.MaCk,
        TenCk = chuyenKhoa.TenCk,
        Mabs = dangKy.Mabs,
        TenBacSi = tenBacSi,
        TrangThai = "Chờ xác nhận",
        NgayDat = now
    };

    return ServiceResult<DatLichKhamResponse>.Ok(
        res,
        "Đặt lịch khám thành công! Vui lòng chờ xác nhận.");
}
    public async Task<ServiceResult<List<LichDaDatResponse>>> LayDanhSachLichAsync(int userId)
    {
        var sdtList = await _db.AppUserHoSos
            .AsNoTracking()
            .Include(lk => lk.HoSo)
            .Where(lk => lk.AppUserId == userId)
            .Select(lk => lk.HoSo.Sodienthoai)
            .Where(s => s != null)
            .ToListAsync();

        var cmndList = await _db.AppUserHoSos
            .AsNoTracking()
            .Include(lk => lk.HoSo)
            .Where(lk => lk.AppUserId == userId)
            .Select(lk => lk.HoSo.Cmnd)
            .Where(c => c != null)
            .ToListAsync();
        var query = _db.DangKyKhams
            .AsNoTracking()
            .Where(dk => !dk.Xoa);

            // Filter theo user (CMND hoặc SDT)
        if (sdtList.Count > 0 || cmndList.Count > 0)
        {
            query = query.Where(dk =>
                sdtList.Contains(dk.Sdt) ||
                cmndList.Contains(dk.Cmnd));
        }

        var danhSach = await query
            .OrderByDescending(dk => dk.Ngay)
            .ThenByDescending(dk => dk.NgaySua)
            .ToListAsync();

        // Lấy tên chuyên khoa
        var maCkList = danhSach.Select(d => d.MaCk).Distinct().ToList();
        var tenCkDict = await _db.Dmchuyenkhoas
            .AsNoTracking()
            .Where(ck => maCkList.Contains(ck.Mack))
            .ToDictionaryAsync(ck => ck.Mack, ck => ck.TenCk);

        // Lấy tên bác sĩ
        var mabsList = danhSach.Select(d => d.Mabs).Distinct().ToList();
        var tenBacSiDict = await _db.BacsiChuyenKhoas
            .AsNoTracking()
            .Include(b => b.NhanVien)
            .Where(b => mabsList.Contains(b.Manv))
            .ToDictionaryAsync(
                b => b.Manv,
                b => b.NhanVien != null
                    ? $"{b.NhanVien.Holot} {b.NhanVien.Ten}".Trim()
                    : null);

        var result = danhSach.Select(dk => new LichDaDatResponse
        {
            MaDk          = dk.MaDk,
            HoTen         = dk.Hoten,
            Ngay          = dk.Ngay,
            LoaiQh        = dk.LoaiQh,
            TimeSlot      = new DateTimeOffset(dk.TimeSlot, TimeSpan.Zero),
            MaCk          = dk.MaCk,
            TenCk         = tenCkDict.GetValueOrDefault(dk.MaCk),
            Mabs          = dk.Mabs,
            TenBacSi      = tenBacSiDict.GetValueOrDefault(dk.Mabs),
            TrangThai     = dk.TrangThai,
            NgayDat       = dk.NgaySua,
            GhiChu        = dk.GhiChu
        }).ToList();

        return ServiceResult<List<LichDaDatResponse>>.Ok(result);
    }


    public async Task<ServiceResult<LichDaDatResponse>> LayChiTietLichAsync(
        int maDk, int? userId = null)
    {
        var dk = await _db.DangKyKhams
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.MaDk == maDk && !d.Xoa);

        if (dk is null)
            return ServiceResult<LichDaDatResponse>.Fail("Không tìm thấy lịch hẹn", 404);

        var tenCk = (await _db.Dmchuyenkhoas
            .AsNoTracking()
            .FirstOrDefaultAsync(ck => ck.Mack == dk.MaCk))?.TenCk;

        string? tenBacSi = null;
        if (!string.IsNullOrEmpty(dk.Mabs))
        {
            var bacSi = await _db.BacsiChuyenKhoas
                .AsNoTracking()
                .Include(b => b.NhanVien)
                .FirstOrDefaultAsync(b => b.Manv == dk.Mabs);

            tenBacSi = bacSi?.NhanVien != null
                ? $"{bacSi.NhanVien.Holot} {bacSi.NhanVien.Ten}".Trim()
                : null;
        }

        return ServiceResult<LichDaDatResponse>.Ok(new LichDaDatResponse
        {
            MaDk          = dk.MaDk,
            HoTen         = dk.Hoten,
            Ngay          = dk.Ngay,
            LoaiQh        = dk.LoaiQh,
            TimeSlot      = new DateTimeOffset(dk.TimeSlot, TimeSpan.Zero),
            MaCk          = dk.MaCk,
            TenCk         = tenCk,
            TrangThai     = dk.TrangThai, // 0: chờ xác nhận, 1: đã xác nhận, 2: đã hủy, 3: đã hoàn thành
            NgayDat       = dk.NgaySua,
            Mabs          = dk.Mabs,
            TenBacSi      = tenBacSi,
            GhiChu        = dk.GhiChu, // lý do hủy
        });
    }

    public async Task<ServiceResult<bool>> HuyLichAsync(
        int maDk, HuyLichRequest req, int? userId = null)
    {
        var dk = await _db.DangKyKhams
            .FirstOrDefaultAsync(d => d.MaDk == maDk && !d.Xoa);

        if (dk is null)
            return ServiceResult<bool>.Fail("Không tìm thấy lịch hẹn", 404);

        if (dk.TrangThai == 2)
            return ServiceResult<bool>.Fail("Lịch hẹn này đã được hủy trước đó", 400);

        if (dk.TrangThai == 3)
            return ServiceResult<bool>.Fail("Lịch hẹn đã hoàn thành, không thể hủy", 400);

        // Không cho hủy nếu ngày khám đã qua
        if (dk.Ngay < DateOnly.FromDateTime(DateTime.UtcNow))
            return ServiceResult<bool>.Fail("Không thể hủy lịch đã qua ngày khám", 400);

        dk.TrangThai = 2;  // 2 = đã hủy
        dk.Status    = "cancelled";
        dk.GhiChu    = string.IsNullOrWhiteSpace(req.LyDo)
            ? dk.GhiChu
            : $"{dk.GhiChu} [Lý do hủy: {req.LyDo}]".Trim();
        dk.NgaySua   = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        _logger.LogInformation("Hủy lịch MaDk={MaDk} - Lý do: {LyDo}", maDk, req.LyDo);

        return ServiceResult<bool>.Ok(true, "Hủy lịch thành công");
    }

    private static DatLichKhamResponse MapToResponse(
        DangKyKham dk, string? tenCk, string? tenBacSi) => new()
    {
        MaDk      = dk.MaDk,
        HoTen     = dk.Hoten,
        Ngaysinh  = dk.Ngaysinh,
        Ngay      = dk.Ngay,
        TimeSlot  = new DateTimeOffset(dk.TimeSlot, TimeSpan.Zero),
        MaCk      = dk.MaCk,
        TenCk     = tenCk,
        NgayDat   = dk.NgaySua,
    };

    private static LichDaDatResponse MapToListResponse(
        DangKyKham dk,
        Dictionary<string, string> tenCkDict,
        Dictionary<string, string?> tenBacSiDict) => new()
    {
        MaDk          = dk.MaDk,
        HoTen         = dk.Hoten,
        Ngay          = dk.Ngay,
        TimeSlot      = new DateTimeOffset(dk.TimeSlot, TimeSpan.Zero),
        MaCk          = dk.MaCk,
        TenCk         = tenCkDict.GetValueOrDefault(dk.MaCk),
        TrangThai     = dk.TrangThai,
        NgayDat       = dk.NgaySua,
        GhiChu        = dk.GhiChu,
    };

   
    }
