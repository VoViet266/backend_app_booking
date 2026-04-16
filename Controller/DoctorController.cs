namespace his_backend.Controller;

using Microsoft.AspNetCore.Mvc;
using his_backend.Services;
using Microsoft.AspNetCore.RateLimiting;

[ApiController]
[Route("api/bacsi")]
public class BacsiController(IBacsiService bacsiService) : ControllerBase
{
    private readonly IBacsiService _bacsiService = bacsiService;

    [HttpGet("lay-danh-sach-bac-si")]
    [EnableRateLimiting("normal")]
    public async Task<IActionResult> GetBacsiAsync()
    {   
        var result = await _bacsiService.GetBacsiAsync();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    //lấy danh sách bác sĩ theo chuyên khoa
    [HttpGet("laybacsi_khoa/{mack}")]
    [EnableRateLimiting("normal")]
    public async Task<IActionResult> GetBacsiTheoChuyenKhoaAsync( 
        [FromRoute] string mack
    )
    {
        var result = await _bacsiService.GetBacsiTheoChuyenKhoaAsync(mack);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}