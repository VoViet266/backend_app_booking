using his_backend.Models;
using his_backend.DTOs;
using his_backend.Common;
using Microsoft.EntityFrameworkCore;

namespace his_backend.Services.chuyekhoa;

public class chuyenkhoaService
{
    private readonly AppDbContext _db;

    public chuyenkhoaService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ServiceResult<List<ChuyenkhoaDto>>> GetAll()
    {
        var danhSach = await _db.Dmchuyenkhoas
            .Select(ck => new ChuyenkhoaDto
            {
                Mack             = ck.Mack,
                TenCk            = ck.TenCk,
                MoTaTrieuChung   = ck.MoTaTrieuChung
            })
            .ToListAsync();
        return ServiceResult<List<ChuyenkhoaDto>>.Ok(danhSach);
    }
}