using his_backend.Services.chuyekhoa;
using his_backend.DTOs;
using his_backend.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
namespace his_backend.Controllers;

[ApiController]
[Route("api/chuyenkhoa")]
public class ChuyenKhoaController(chuyenkhoaService chuyenkhoaService) : ControllerBase
{
    private readonly chuyenkhoaService _chuyenkhoaService = chuyenkhoaService;

    [HttpGet("laydanhsach-chuyenkhoa")]
    [EnableRateLimiting("normal")]
    [ProducesResponseType(typeof(ServiceResult<List<ChuyenkhoaDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<List<ChuyenkhoaDto>>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LayDanhSachChuyenKhoa()
    {
        var result = await _chuyenkhoaService.GetAll();
        return result.Success
            ? Ok(result)
            : NotFound(result);
    }

    [HttpGet("chitiet-chuyenkhoa/{mack}")]
    [EnableRateLimiting("normal")]
    [ProducesResponseType(typeof(ServiceResult<List<ChuyenkhoaDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<List<ChuyenkhoaDto>>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChiTietChuyenKhoa([FromRoute] string mack)
    {
        var result = await _chuyenkhoaService.GetById(mack);
        return result.Success
            ? Ok(result)
            : NotFound(result);
    }
}