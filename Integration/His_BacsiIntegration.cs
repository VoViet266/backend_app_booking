namespace his_backend.Integration;

using Microsoft.EntityFrameworkCore;
using his_backend.Models;
using his_backend.DTOs;
using his_backend.Common;
using Microsoft.AspNetCore.Http.HttpResults;

interface IHis_BacsiIntegration
{
    Task<ServiceResult<List<BacsiDto>>> GetBacsiAsync();
    Task<ServiceResult<List<BacsiDto>>> GetBacsiTheoChuyenKhoaAsync(string mack);
}
public class His_BacsiIntegration(AppDbContext db) : IHis_BacsiIntegration
{
    private readonly AppDbContext db = db;
    public async Task<ServiceResult<List<BacsiDto>>> GetBacsiAsync()
    {
        var data = await db.BacsiChuyenKhoas
            .Where(x =>
                x.NhanVien != null &&
                x.NhanVien.Trangthai == "1" &&
                (
                    x.NhanVien.Holot.StartsWith("BS") ||
                    x.NhanVien.Holot.StartsWith("Ths. BS")
                )
            )
            .GroupBy(x => x.NhanVien!.Manv)
            .Select(g => new BacsiDto
            {
                Manv = g.First().NhanVien!.Manv,
                Holot = g.First().NhanVien!.Holot,
                Ten = g.First().NhanVien!.Ten,
                Duockham = g.First().NhanVien!.Duockham,
                Trangthai = g.First().NhanVien!.Trangthai,
                Gioitinh = g.First().NhanVien!.Gioitinh,
                Mack = g.First().Mack,
                TenChuyenKhoa = string.Join(", ",
                    g.Select(x => x.ChuyenKhoa!.TenCk).Distinct())
            })
            .ToListAsync();

        return ServiceResult<List<BacsiDto>>.Ok(data);
    }
    public async Task<ServiceResult<List<BacsiDto>>> GetBacsiTheoChuyenKhoaAsync(string mack)
    {
        var data = await db.BacsiChuyenKhoas
.Where(x => x.Mack == mack && x.NhanVien!.Trangthai == "1" && x.NhanVien.Holot.Contains("BS"))
.Select(x => new BacsiDto
{
    Manv = x.NhanVien!.Manv,
    Holot = x.NhanVien.Holot,
    Ten = x.NhanVien.Ten,
    Mack = x.Mack,
    Duockham = x.NhanVien!.Duockham,
    Trangthai = x.NhanVien!.Trangthai,
    TenChuyenKhoa = x.ChuyenKhoa!.TenCk,
    Gioitinh = x.NhanVien!.Gioitinh
})
.ToListAsync();
        return ServiceResult<List<BacsiDto>>.Ok(data);
    }

}