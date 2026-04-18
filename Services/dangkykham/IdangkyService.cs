using his_backend.Common;
using his_backend.DTOs;

public interface IDangkykbService
{
    Task<ServiceResult<DatLichKhamResponse>> DatLichAsync(
        DatLichKhamRequest req,
        int? userId);
    Task<ServiceResult<object>> GetSlotBookingAsync(string mabs, DateOnly date);
    Task<ServiceResult<List<LichDaDatResponse>>> LayDanhSachLichAsync(int userId);
    Task<ServiceResult<LichDaDatResponse>> LayChiTietLichAsync(int maDk, int userId);
    Task<ServiceResult<bool>> HuyLichAsync(int maDk, HuyLichRequest req, int userId);
    Task<ServiceResult<List<LichDaDatResponse>>> LayLichDangKyTheoNgayAsync(DateOnly ngay);
}
