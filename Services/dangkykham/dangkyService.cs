using Microsoft.EntityFrameworkCore;
using his_backend.Models;
using his_backend.DTOs;
using his_backend.Common;

namespace his_backend.Services.dangkykham;

public class DangkykbService(AppDbContext db) : IDangkykbService
{
    private readonly AppDbContext _db = db;

    public async Task<ServiceResult<DatLichKhamResponse>> DatLichAsync(
    DatLichKhamRequest req,
    int? userId)
    {
        var now = DateTime.UtcNow;
        if (!req.TimeSlot.HasValue)
            return ServiceResult<DatLichKhamResponse>.Fail("Thời gian khám không được để trống", 400);

        Dmchuyenkhoa? chuyenKhoa = null;
        if (!string.IsNullOrEmpty(req.MaCk))
        {
            chuyenKhoa = await _db.Dmchuyenkhoas
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Mack == req.MaCk);

            if (chuyenKhoa == null)
                return ServiceResult<DatLichKhamResponse>.Fail(
                    $"Chuyên khoa '{req.MaCk}' không tồn tại", 400);
        }

        // 2. Xử lý Mabs cho phép null và không phụ thuộc cứng vào MaCk
        string? tenBacSi = null;
        if (!string.IsNullOrEmpty(req.Mabs))
        {
            var query = _db.BacsiChuyenKhoas.Include(x => x.NhanVien).AsQueryable();
            query = query.Where(x => x.Manv == req.Mabs.ToString());

            // Nếu có MaCk truyền vào thì mới bắt buộc bác sĩ phải thuộc chuyên khoa đó
            if (!string.IsNullOrEmpty(req.MaCk))
            {
                query = query.Where(x => x.Mack == req.MaCk);
            }

            var bacSi = await query.FirstOrDefaultAsync();

            if (bacSi == null)
            {
                var errMsg = string.IsNullOrEmpty(req.MaCk)
                    ? "Bác sĩ không tồn tại"
                    : "Bác sĩ không tồn tại hoặc không thuộc chuyên khoa đã chọn";
                return ServiceResult<DatLichKhamResponse>.Fail(errMsg, 400);
            }

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
            benhNhan = new Nguoibenhdangky
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

        // Phần check trùng lịch 
        if (req.Mabs != null && req.TimeSlot != null)
        {
            var timeSlotStart = req.TimeSlot.Value.UtcDateTime;
            var timeSlotEnd = timeSlotStart.AddMinutes(15); // Giả sử mỗi slot là 15 phút

            var hasConflict = await _db.DangKyKhams.AnyAsync(dk =>
                dk.Mabs == req.Mabs &&
                dk.TrangThai == 0 && // Chỉ check các lịch đang chờ hoặc đã xác nhận
                ((dk.TimeSlot >= timeSlotStart && dk.TimeSlot < timeSlotEnd) ||
                (dk.TimeSlot.AddMinutes(15) > timeSlotStart && dk.TimeSlot.AddMinutes(15) <= timeSlotEnd)));

            if (hasConflict)
            {
                return ServiceResult<DatLichKhamResponse>.Fail(
                    "Bác sĩ đã có lịch khám trong khung giờ này. Vui lòng chọn thời gian khác.", 400);
            }
        }

        var dangKy = new DangKyKham
        {
            Mandk = benhNhan.Id,
            Mapk = req.Mapk,
            Mabs = !string.IsNullOrEmpty(req.Mabs) ? req.Mabs : null,
            MaCk = req.MaCk ?? "",

            Hoten = req.HoTen,
            Diachi = req.Diachi ?? "",
            Sdt = req.Sdt ?? "",
            Cmnd = req.Cmnd ?? "",

            LoaiQh = req.LoaiQh ?? "",
            HoTenQh = req.HotenQh ?? "",
            DienThoaiQh = req.DienThoaiQh ?? "",
            DiachiQh = req.DiachiQh ?? "",

            Ngaysinh = req.Ngaysinh ?? DateOnly.FromDateTime(DateTime.Now),
            TimeSlot = req.TimeSlot.Value.UtcDateTime,
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
            Cmnd = dangKy.Cmnd,
            Sdt = dangKy.Sdt,
            Ngay = dangKy.Ngay,
            TimeSlot = req.TimeSlot,
            MaCk = dangKy.MaCk,
            TenCk = chuyenKhoa?.TenCk ?? string.Empty,
            Mabs = dangKy.Mabs ?? "",
            TenBacSi = tenBacSi,
            TrangThai = dangKy.TrangThai.ToString(),
            NgayDat = now
        };

        return ServiceResult<DatLichKhamResponse>.Ok(
            res,
            "Đặt lịch khám thành công! Vui lòng chờ xác nhận.");
    }


    ///Kiểm tra tất cả các slot đã được đặt
    public async Task<ServiceResult<object>> GetSlotBookingAsync(string mabs, DateOnly date)
    {
        ///lất tất cả các lịch đã đặt của bác sĩ trong ngày đó
        var checkSlot = await _db.DangKyKhams
            .AsNoTracking()
            .Where(dk => dk.Mabs == mabs && dk.Ngay == date && dk.TrangThai == 0)
            .Select(dk => new
            {
                dk.MaDk,
                dk.TimeSlot,
            })
            .ToListAsync();
        return ServiceResult<object>.Ok(new
        {
            Mabs = mabs,
            Ngay = date,
            Slots = checkSlot.Select(s => new
            {
                s.MaDk,
                TimeSlot = s.TimeSlot.ToLocalTime()
            }).ToList()

        });
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
                .GroupBy(b => b.Manv) // Nhóm theo mã nhân viên
                .Select(g => g.First()) // Chỉ lấy 1 dòng đại diện cho mỗi bác sĩ
                .ToDictionaryAsync(
                    b => b.Manv,
                    b => b.NhanVien != null
                        ? $"{b.NhanVien.Holot} {b.NhanVien.Ten}".Trim()
                        : null);

        var result = danhSach.Select(dk => new LichDaDatResponse
        {
            MaDk = dk.MaDk,
            HoTen = dk.Hoten,
            Ngay = dk.Ngay,
            Sdt = dk.Sdt,
            Cmnd = dk.Cmnd,
            Ngaysinh = dk.Ngaysinh,
            LoaiQh = dk.LoaiQh,
            TimeSlot = new DateTimeOffset(dk.TimeSlot, TimeSpan.Zero),
            MaCk = dk.MaCk ?? "",
            TenCk = tenCkDict.GetValueOrDefault(dk.MaCk ?? ""),
            Mabs = dk.Mabs ?? "",
            TenBacSi = tenBacSiDict.GetValueOrDefault(dk.Mabs ?? ""),
            TrangThai = dk.TrangThai,
            NgayDat = dk.NgaySua,
            GhiChu = dk.GhiChu
        }).ToList();

        return ServiceResult<List<LichDaDatResponse>>.Ok(result);
    }


    public async Task<ServiceResult<LichDaDatResponse>> LayChiTietLichAsync(
        int maDk, int userId)
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
            MaDk = dk.MaDk,

            HoTen = dk.Hoten,
            Ngay = dk.Ngay,
            Sdt = dk.Sdt,
            Cmnd = dk.Cmnd,
            Ngaysinh = dk.Ngaysinh,
            LoaiQh = dk.LoaiQh,
            TimeSlot = new DateTimeOffset(dk.TimeSlot, TimeSpan.Zero),
            MaCk = dk.MaCk ?? "",
            TenCk = tenCk,
            Mabs = dk.Mabs ?? "",
            TenBacSi = tenBacSi,
            TrangThai = dk.TrangThai,
            NgayDat = dk.NgaySua,
            GhiChu = dk.GhiChu
        });
    }

    public async Task<ServiceResult<bool>> HuyLichAsync(
        int maDk, HuyLichRequest req, int userId)
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
        dk.Status = "cancelled";
        dk.GhiChu = string.IsNullOrWhiteSpace(req.LyDo)
            ? dk.GhiChu
            : $"{dk.GhiChu} [Lý do hủy: {req.LyDo}]".Trim();
        dk.NgaySua = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return ServiceResult<bool>.Ok(true, "Hủy lịch thành công");
    }

    public async Task<ServiceResult<List<LichDaDatResponse>>> LayLichDangKyTheoNgayAsync(DateOnly ngay)
    {
        var query = _db.DangKyKhams
            .AsNoTracking()
            .Where(dk => !dk.Xoa && dk.Ngay == ngay && dk.TrangThai == 1);

        var danhSach = await query
            .OrderBy(dk => dk.TimeSlot)
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
            MaDk = dk.MaDk,
            HoTen = dk.Hoten,
            Ngay = dk.Ngay,
            LoaiQh = dk.LoaiQh,
            TimeSlot = new DateTimeOffset(dk.TimeSlot, TimeSpan.Zero),
            MaCk = dk.MaCk,
            TenCk = tenCkDict.GetValueOrDefault(dk.MaCk),
            Mabs = dk.Mabs!,
            Cmnd = dk.Cmnd,
            TenBacSi = tenBacSiDict.GetValueOrDefault(dk.Mabs!),
            TrangThai = dk.TrangThai,
            NgayDat = dk.NgaySua,
            GhiChu = dk.GhiChu,
            DienThoaiQh = dk.DienThoaiQh,
            DiachiQh = dk.DiachiQh,
            HotenQh = dk.HoTenQh
        }).ToList();

        return ServiceResult<List<LichDaDatResponse>>.Ok(result);
    }


}
