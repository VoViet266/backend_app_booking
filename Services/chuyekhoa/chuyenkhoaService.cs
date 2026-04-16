using his_backend.Models;
using his_backend.DTOs;
using his_backend.Common;
using Microsoft.EntityFrameworkCore;

namespace his_backend.Services.chuyekhoa;

public class chuyenkhoaService(AppDbContext db)
{
    private readonly AppDbContext _db = db;

    public async Task<ServiceResult<List<ChuyenkhoaDto>>> GetAll()
    {
        var danhSach = await _db.Dmchuyenkhoas
            .Select(ck => new ChuyenkhoaDto
            {
                Mack             = ck.Mack,
                TenCk            = ck.TenCk,
                MoTaTrieuChung   = ck.MoTaTrieuChung,
                ImageUrl         = ck.ImageUrl
            })
            .Where(ck => ck.Mack != "00")
            .ToListAsync();
        return ServiceResult<List<ChuyenkhoaDto>>.Ok(danhSach);
    }

    public async Task<ServiceResult<ChuyenkhoaDto>> GetById(string? mack)
    {
        var data = await _db.Dmchuyenkhoas
            .Where(ck => ck.Mack == mack)   
            .Select(ck => new ChuyenkhoaDto 
            {
                Mack             = ck.Mack,
                TenCk            = ck.TenCk,
                MoTaTrieuChung   = ck.MoTaTrieuChung,
                ImageUrl         = ck.ImageUrl
            })
            .FirstOrDefaultAsync();

        if (data is null)
            return ServiceResult<ChuyenkhoaDto>.Fail("Không tìm thấy chuyên khoa", 404);

        return ServiceResult<ChuyenkhoaDto>.Ok(data);
    }
}