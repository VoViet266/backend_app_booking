using his_backend.Services.chuyekhoa;
using his_backend.DTOs;
using his_backend.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace his_backend.Controllers;

[ApiController]
[Route("api/chuyenkhoa")]
public class ChuyenKhoaController : ControllerBase
{
    private readonly chuyenkhoaService _chuyenkhoaService;

    public ChuyenKhoaController(chuyenkhoaService chuyenkhoaService)
    {
        _chuyenkhoaService = chuyenkhoaService;
    }

    [HttpGet("laydanhsach-chuyenkhoa")]
    [ProducesResponseType(typeof(ServiceResult<List<ChuyenkhoaDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<List<ChuyenkhoaDto>>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LayDanhSachChuyenKhoa()
    {
        var result = await _chuyenkhoaService.GetAll();
        return result.Success
            ? Ok(result)
            : NotFound(result);
    }
}