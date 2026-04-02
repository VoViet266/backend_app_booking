using his_backend.Common;
using his_backend.Models;
using Microsoft.EntityFrameworkCore;

public interface IHis_BenhnhanIntegration
{
    Task<ServiceResult<List<DmbenhnhanDto>>> GetBenhnhanAsync();

    Task<ServiceResult<List<LichSuKhamDto>>> GetLichSuKhamBnAsync(string cmnd);

    Task<ServiceResult<List<DotKhamDto>>> GetToaThuocTheoLichSuKhamAsync(string cmnd);

    Task<ServiceResult<List<DonthuocDto>>> GetChiTietDonThuocAsync(string makb);
}

public class His_BenhnhanIntegration : IHis_BenhnhanIntegration
{
    private readonly AppDbContext _appDbContext;
    public His_BenhnhanIntegration(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<ServiceResult<List<DmbenhnhanDto>>> GetBenhnhanAsync()
    {
        return ServiceResult<List<DmbenhnhanDto>>.Ok([]);
    }

    /// <summary>
    ///select p.mabn, d.holot , d.ten , k.ngaykcb , d.cmnd, p.mathe
    ///from "current".psdangky p 
    ///join "current".dmbenhnhan d  on p.mabn  = d.mabn 
    ///join "current".khambenh k  on k.mabn =  p.mabn 
    ///where p.mathe = :mathe and k.dakham = 3
    ///group by  p.mabn, d.holot, d.ten, k.ngaykcb, d.cmnd, p.mathe
    /// </summary>
    public async Task<ServiceResult<List<LichSuKhamDto>>> GetLichSuKhamBnAsync(string cmnd)
    {
        var mathe = await (
            from p in _appDbContext.Psdangkies
            join d2 in _appDbContext.Dmbenhnhans
                on p.Mabn equals d2.Mabn
            where d2.Cmnd == cmnd
            select p.Mathe
        ).ToListAsync();

        var lichsukham = await (
            from p in _appDbContext.Psdangkies
            join d2 in _appDbContext.Dmbenhnhans
                on p.Mabn equals d2.Mabn
            join k in _appDbContext.Khambenhs
                on p.Makb equals k.Makb
            join d in _appDbContext.Dmicds
                on k.Maicd equals d.Maicd into dJoin
            from d in dJoin.DefaultIfEmpty() 
            where mathe.Contains(p.Mathe) && k.Dakham == 3
            orderby k.Ngaykcb descending
            select new LichSuKhamDto
            {
                Makb        = p.Makb,
                Ngaykham    = p.Ngaydk,
                Maicd       = k.Maicd,
                TenvietIcd  = d != null ? d.Tenviet  : null,
            }
        ).ToListAsync();

        if (lichsukham.Count == 0)
        {
            return ServiceResult<List<LichSuKhamDto>>.Fail(
                $"Không tìm thấy lịch sử khám cho CMND {cmnd}", 404);
        }

        return ServiceResult<List<LichSuKhamDto>>.Ok(lichsukham);
    }

    /// <summary>
    /// Lấy danh sách toa thuốc theo CMND/CCCD:
    ///   SELECT p.ngayhd, p.mabn, p.mahh, dm.tenhc, ...
    ///   FROM dmbenhnhan d
    ///   JOIN psdangky ps ON d.mabn = ps.mabn
    ///   JOIN pshdxn p ON ps.mabn = p.mabn
    ///   JOIN dmthuoc dm ON p.mahh = dm.mahh
    ///   WHERE d.cmnd = :cmnd AND p.xoa != 1
    ///   ORDER BY p.ngayhd
    /// </summary>
    public async Task<ServiceResult<List<DotKhamDto>>> GetToaThuocTheoLichSuKhamAsync(string cmnd)
    {
        // Bước 1: Lấy danh sách mabn từ dmbenhnhan theo cmnd
        var mabnList = await _appDbContext.Dmbenhnhans
            .Where(d => d.Cmnd == cmnd)
            .Select(d => d.Mabn)
            .Distinct()
            .ToListAsync();

        if (!mabnList.Any())
        {
            return ServiceResult<List<DotKhamDto>>.Fail(
                $"Không tìm thấy bệnh nhân với CMND {cmnd}", 404);
        }

        // Bước 2: Query toa thuốc: pshdxn JOIN dmthuoc, filter theo danh sách mabn
        var data = await (
            from p in _appDbContext.Pshdxns
            join dm in _appDbContext.Dmthuocs
                on p.Mahh equals dm.Mahh
            where mabnList.Contains(p.Mabn)
                  && p.Xoa != 1  // loại bỏ bản ghi đã xóa
            orderby p.Ngayhd, p.Mahh
            select new
            {
                p.Ngayhd,
                p.Mabn,
                p.Mahh,
                dm.Tenhh,
                dm.Tenhc,
                dm.Dvt,
                p.Soluong,
                p.Cachuong,
                p.LieuDung
            }
        ).ToListAsync();

        if (data.Count == 0)
        {
            return ServiceResult<List<DotKhamDto>>.Fail(
                $"Không có đơn thuốc nào cho CMND {cmnd}", 404);
        }
        var result = data
            .GroupBy(x => x.Ngayhd)
            .OrderByDescending(g => g.Key)
            .Select(g => new DotKhamDto
            {
                Makb = "",
                Ngaydk = g.Key.HasValue
                    ? g.Key.Value.ToDateTime(TimeOnly.MinValue)
                    : null,
                Donthuocs = g.Select(x => new DonthuocDto
                {
                    Mahh     = x.Mahh    ?? "",
                    Tenhh    = x.Tenhh   ?? "",
                    Tenhc    = x.Tenhc   ?? "",
                    Dvt      = x.Dvt     ?? "",
                    Soluong  = x.Soluong,
                    Cachuong = x.Cachuong ?? x.LieuDung ?? ""
                }).DistinctBy(t => t.Mahh).ToList()
            })
            .ToList();

        return ServiceResult<List<DotKhamDto>>.Ok(result);
    }

    /// <summary>
    /// Lấy chi tiết đơn thuốc của 1 đợt khám cụ thể theo makb
    /// Logic: psdangky.makb → lấy maba → pshdxn.maba → join dmthuoc
    /// </summary>
    public async Task<ServiceResult<List<DonthuocDto>>> GetChiTietDonThuocAsync(string makb)
    {
        // Lấy maba từ đợt khám
        var dotKham = await _appDbContext.Psdangkies
            .Where(dk => dk.Makb == makb)
            .Select(dk => new { dk.Maba, dk.Mabn, dk.Ngaydk })
            .FirstOrDefaultAsync();

        if (dotKham == null)
        {
            return ServiceResult<List<DonthuocDto>>.Fail(
                $"Không tìm thấy đợt khám với mã {makb}", 404);
        }

        // Query đơn thuốc theo maba (nếu có) hoặc fallback theo mabn + ngayhd
        var query = _appDbContext.Pshdxns
            .Join(_appDbContext.Dmthuocs,
                p => p.Mahh,
                d => d.Mahh,
                (p, d) => new { p, d })
            .Where(x => x.p.Xoa != 1);

        // Ưu tiên link qua Maba nếu có giá trị
        if (!string.IsNullOrEmpty(dotKham.Maba))
        {
            query = query.Where(x => x.p.Maba == dotKham.Maba);
        }
        else
        {
            // Fallback: filter theo mabn + ngayhd cùng ngày khám
            var ngayKham = dotKham.Ngaydk.HasValue
                ? DateOnly.FromDateTime(dotKham.Ngaydk.Value)
                : (DateOnly?)null;

            query = query.Where(x =>
                x.p.Mabn == dotKham.Mabn &&
                x.p.Ngayhd == ngayKham);
        }

        var data = await query
            .OrderBy(x => x.d.Tenhh)
            .Select(x => new DonthuocDto
            {
                Mahh     = x.p.Mahh    ?? "",
                Tenhh    = x.d.Tenhh   ?? "",
                Tenhc    = x.d.Tenhc   ?? "",
                Dvt      = x.d.Dvt     ?? "",
                Soluong  = x.p.Soluong,
                Cachuong = x.p.Cachuong ?? x.p.LieuDung ?? ""
            })
            .ToListAsync();

        if (data.Count == 0)
        {
            return ServiceResult<List<DonthuocDto>>.Fail(
                $"Không có đơn thuốc cho đợt khám {makb}", 404);
        }

        return ServiceResult<List<DonthuocDto>>.Ok(data);
    }
}