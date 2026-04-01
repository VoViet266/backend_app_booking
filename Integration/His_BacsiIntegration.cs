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
public class His_BacsiIntegration : IHis_BacsiIntegration
{
    private readonly AppDbContext db;
    
    public His_BacsiIntegration(AppDbContext db)
    {
        this.db = db;
    }
    public async Task<ServiceResult<List<BacsiDto>>> GetBacsiAsync()
    {
        var data = await db.BacsiChuyenKhoas
    .Where(x => x.NhanVien.Trangthai == "1" && (x.NhanVien.Holot.StartsWith("BS") || x.NhanVien.Holot.StartsWith("Ths. BS")))
    .Select(x => new BacsiDto
    {
        Manv = x.NhanVien.Manv,
        Holot = x.NhanVien.Holot,
        Ten = x.NhanVien.Ten,
        Mack = x.Mack,
        Duockham = x.NhanVien.Duockham,
        Trangthai = x.NhanVien.Trangthai,
        TenChuyenKhoa = x.ChuyenKhoa.TenCk
    })
    .ToListAsync();

        return ServiceResult<List<BacsiDto>>.Ok(data);
    }

    public async Task<ServiceResult<List<BacsiDto>>> GetBacsiTheoChuyenKhoaAsync(string mack)
    {
            var data = await db.BacsiChuyenKhoas
    .Where(x => x.Mack == mack && x.NhanVien.Trangthai == "1" && x.NhanVien.Holot.Contains("BS"))
    .Select(x => new BacsiDto
    {
        Manv = x.NhanVien.Manv,
        Holot = x.NhanVien.Holot,
        Ten = x.NhanVien.Ten,
        Mack = x.Mack,
        Duockham = x.NhanVien.Duockham,
        Trangthai = x.NhanVien.Trangthai,
        TenChuyenKhoa = x.ChuyenKhoa.TenCk
    })
    .ToListAsync();
        return ServiceResult<List<BacsiDto>>.Ok(data);
    }

}