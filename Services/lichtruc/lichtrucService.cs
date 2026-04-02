namespace his_backend.Services;
using his_backend.Models;
using Microsoft.EntityFrameworkCore;
using his_backend.Common;
using his_backend.DTOs;

public interface ILichtrucService
{
    Task<List<Lichtruc>> GetAll();
    // Task<Lichtruc> GetById(int id);
    Task<ServiceResult<List<Lichtruc>>> GetbyMabs(string mabs);
    // Task<Lichtruc> GetbyMabsAndNgay(string mabs, DateOnly ngay);
    // Task<Lichtruc> GetbyMabsAndNgayAndThu(string mabs, DateOnly ngay, int thu);
}

public class LichtrucService : ILichtrucService
{
    private readonly AppDbContext _context;
    public LichtrucService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Lichtruc>> GetAll()
    {
        return await _context.Lichtrucs.ToListAsync();
    }

    // public async Task<Lichtruc> GetById(int id)
    // {
    //     return await _context.Lichtrucs.FindAsync(id);
    // }

    public async Task<ServiceResult<List<Lichtruc>>> GetbyMabs(string mabs)
        {
            var result = await _context.Lichtrucs.Where(l => l.manv == mabs).ToListAsync();
        return ServiceResult<List<Lichtruc>>.Ok(result);
    }


    // public async Task<Lichtruc> GetbyMabsAndNgay(string mabs, DateOnly ngay)
    // {
    //     return await _context.Lichtrucs.FirstOrDefaultAsync(l => l.manv == mabs && l.ngaytruc == ngay);
    // }

    // public async Task<Lichtruc> GetbyMabsAndNgayAndThu(string mabs, DateOnly ngay, int thu)
    // {
    //     return await _context.Lichtrucs.FirstOrDefaultAsync(l => l.manv == mabs && l.ngaytruc == ngay && l.loai_truc == thu);
    // }

    // public async Task<Lichtruc> GetbyMabsAndNgayAndThuAndLoaiCa(string mabs, DateOnly ngay, int thu, string loaiCa)
    // {
    //     return await _context.Lichtrucs.FirstOrDefaultAsync(l => l.manv == mabs && l.ngaytruc == ngay && l.loai_truc == thu && l.loai_truc == loaiCa);
    // }
}