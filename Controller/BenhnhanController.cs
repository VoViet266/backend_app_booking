using his_backend.DTOs;
using his_backend.Models;
using his_backend.Common;
using Microsoft.AspNetCore.Mvc;
using his_backend.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;

namespace his_backend.Controller;

[ApiController]
[Route("api/nguoibenh")]
[Authorize]
public class BenhnhanController(DonthuocService donthuocService,  HoSoBenhNhanService hoSoService) : ControllerBase
{
    

    private readonly HoSoBenhNhanService _hoSoService = hoSoService;

    private readonly DonthuocService _donthuocService = donthuocService;

    [HttpPost("them-ho-so")]
    [EnableRateLimiting("normal")]
    public async Task<IActionResult> ThemBenhnhan(ThemHosoRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)
            ?? User.FindFirst("sub");
        if (claim is null || !int.TryParse(claim.Value, out var userId))
            return Unauthorized(ServiceResult<HoSoBenhNhanResponse>.Fail("Không xác định được người dùng"));

        var result = await _hoSoService.ThemHoSoAsync(userId, request);

        if (!result.Success)
            return StatusCode(result.StatusCode, result);

        return StatusCode(201, result);
    }
    

    [HttpGet("lich-su-kham/{mathe}")]
    [EnableRateLimiting("normal")]
    [ProducesResponseType(typeof(ServiceResult<List<LichSuKhamResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<List<LichSuKhamResponse>>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLichSuKhamBn(string mathe)
    {
        var data = await _donthuocService.GetLichSuKhamBnAsync(mathe);
        if (!data.Success)
            return StatusCode(data.StatusCode, data);
        return Ok(data);
    }


    [HttpGet("toa-thuoc/{mathe}")]

    [EnableRateLimiting("normal")]
    [ProducesResponseType(typeof(ServiceResult<List<DotKhamDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<List<DotKhamDto>>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetToaThuocTheoLichSuKham(string mathe)
    {
        var data = await _donthuocService.GetToaThuocTheoLichSuKhamAsync(mathe);
        if (!data.Success)
            return StatusCode(data.StatusCode, data);
        return Ok(data);
    }

    /// <summary>
    /// Lấy chi tiết đơn thuốc của 1 đợt khám cụ thể
    /// </summary>
    [HttpGet("don-thuoc/{makb}")]
    [EnableRateLimiting("normal")]
    [ProducesResponseType(typeof(ServiceResult<List<DonthuocDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<List<DonthuocDto>>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetChiTietDonThuoc(string makb)
    {
        var data = await _donthuocService.GetChiTietDonThuocAsync(makb);
        if (!data.Success)
            return StatusCode(data.StatusCode, data);
        return Ok(data);
    }
}
