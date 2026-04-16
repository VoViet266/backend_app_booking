using his_backend.Integration;
using his_backend.Models;
using his_backend.Common;

public class DonthuocService(IHis_BenhnhanIntegration his_BenhnhanIntegration) : IHis_BenhnhanIntegration
{
    private readonly IHis_BenhnhanIntegration _his_BenhnhanIntegration = his_BenhnhanIntegration;

    public async Task<ServiceResult<List<DmbenhnhanDto>>> GetBenhnhanAsync()
    {
        return await _his_BenhnhanIntegration.GetBenhnhanAsync();
    }

    /// <summary>
    /// Lấy lịch sử danh sách các đợt khám theo mã thẻ BHYT
    /// </summary>
    public async Task<ServiceResult<List<LichSuKhamDto>>> GetLichSuKhamBnAsync(string cmnd)
    {
        return await _his_BenhnhanIntegration.GetLichSuKhamBnAsync(cmnd);
    }

    /// <summary>
    /// Lấy toàn bộ toa thuốc (group theo ngày) theo mã thẻ BHYT
    /// </summary>
    public async Task<ServiceResult<List<DotKhamDto>>> GetToaThuocTheoLichSuKhamAsync(string cmnd)
    {
        return await _his_BenhnhanIntegration.GetToaThuocTheoLichSuKhamAsync(cmnd);
    }

    /// <summary>
    /// Lấy chi tiết đơn thuốc theo mã thẻ (mathe)
    /// </summary>
    public async Task<ServiceResult<List<DonthuocDto>>> GetChiTietDonThuocAsync(string mathe)
    {
        return await _his_BenhnhanIntegration.GetChiTietDonThuocAsync(mathe);
    }
}