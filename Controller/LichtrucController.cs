namespace his_backend.Controller;

using Microsoft.AspNetCore.Mvc;
using his_backend.Services;
using his_backend.Models;
using his_backend.Common;
using his_backend.DTOs;
using Microsoft.AspNetCore.RateLimiting;
[ApiController]
[Route("api/lichtruc")]
public class LichtrucController(ILichtrucService lichtrucService) : ControllerBase
{
    private readonly ILichtrucService _lichtrucService = lichtrucService;

    [HttpGet("lay-danh-sach-lich-truc")]
    [EnableRateLimiting("normal")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _lichtrucService.GetAll();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    //lấy lịch trực theo bác sĩ
    [HttpGet("lich-truc-bs/{mabs}")]
    [EnableRateLimiting("normal")]
    [ProducesResponseType(typeof(ServiceResult<List<Lichtruc>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ServiceResult<List<Lichtruc>>> GetbyMabs(
        [FromRoute] string mabs
    )
    {
        var result = await _lichtrucService.GetbyMabs(mabs);
        return result;
    }
    
}