namespace his_backend.Services;
using his_backend.Integration;
using his_backend.Models;
using his_backend.Common;

public interface IBacsiService
{
    Task<ServiceResult<List<BacsiDto>>> GetBacsiAsync();
    Task<ServiceResult<List<BacsiDto>>> GetBacsiTheoChuyenKhoaAsync(string mack);
}
class BacsiService(IHis_BacsiIntegration his_BacsiIntegration) : IBacsiService
{
    private readonly IHis_BacsiIntegration _his_BacsiIntegration = his_BacsiIntegration;

    public async Task<ServiceResult<List<BacsiDto>>> GetBacsiAsync()
    {
        return await _his_BacsiIntegration.GetBacsiAsync();
    }   


    public async Task<ServiceResult<List<BacsiDto>>> GetBacsiTheoChuyenKhoaAsync(string mack)
    {
        return await _his_BacsiIntegration.GetBacsiTheoChuyenKhoaAsync(mack);
    }
}