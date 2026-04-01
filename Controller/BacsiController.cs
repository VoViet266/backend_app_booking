namespace his_backend.Controller;

using Microsoft.AspNetCore.Mvc;
using his_backend.Services;
using his_backend.Models;
using his_backend.Common;

[ApiController]
[Route("api/bacsi")]
public class BacsiController : ControllerBase
{
    private readonly IBacsiService _bacsiService;
    public BacsiController(IBacsiService bacsiService)
    {
        _bacsiService = bacsiService;
    }   
    [HttpGet("lay-danh-sach-bac-si")]
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