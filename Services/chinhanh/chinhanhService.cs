using Microsoft.EntityFrameworkCore;
using his_backend.Models;
using his_backend.Common;


public interface IchinhanhService
{
    Task<ServiceResult<List<DmChiNhanh>>> GetAllChinhanh();
}
public class chinhanhService(AppDbContext context) : IchinhanhService
{
    private readonly AppDbContext _context = context;

    public async Task<ServiceResult<List<DmChiNhanh>>> GetAllChinhanh()
    {
        var result = await _context.Dmchinhanhs.ToListAsync();
        return ServiceResult<List<DmChiNhanh>>.Ok(result, "Lấy danh sách chi nhánh thành công");
    }
}   