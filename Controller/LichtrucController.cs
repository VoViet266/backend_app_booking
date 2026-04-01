namespace his_backend.Controller;

using Microsoft.AspNetCore.Mvc;
using his_backend.Services;
using his_backend.Models;
using his_backend.Common;
using his_backend.DTOs;

[ApiController]
[Route("api/lichtruc")]
public class LichtrucController : ControllerBase
{
    private readonly ILichtrucService _lichtrucService;
    public LichtrucController(ILichtrucService lichtrucService)
    {
        _lichtrucService = lichtrucService;
    }
    
    [HttpGet("lay-danh-sach-lich-truc")]
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