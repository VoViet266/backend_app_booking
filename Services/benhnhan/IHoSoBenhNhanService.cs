using his_backend.Common;
using his_backend.DTOs;

namespace his_backend.Services;
public interface IHoSoBenhNhanService   
{
    Task<ServiceResult<List<HoSoBenhNhanResponse>>> LayDanhSachAsync(int userId);
    Task<ServiceResult<HoSoBenhNhanResponse>> LayChiTietAsync(int userId, int hoSoId);
    Task<ServiceResult<HoSoBenhNhan>> ThemHoSoAsync(int userId, ThemHosoRequest req);
    // Task<ServiceResult<HoSoBenhNhanResponse>> CapNhatLienKetAsync(int userId, int hoSoId, CapNhatLienKetRequest req);
    Task<ServiceResult<HoSoBenhNhan>> CapNhatHoSoAsync(
        int hoSoId,
        HoSoBenhNhan req
    );
    Task<ServiceResult<bool>> XoaHoSoAsync(int userId, int hoSoId);
    Task<ServiceResult<HoSoBenhNhanResponse>> DatMacDinhAsync(int userId, int hoSoId);

}