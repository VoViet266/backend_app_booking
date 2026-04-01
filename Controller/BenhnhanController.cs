using his_backend.DTOs;
using his_backend.Models;
using his_backend.Common;
using Microsoft.AspNetCore.Mvc;
using his_backend.Services;


namespace his_backend.Controller;

[ApiController]
[Route("api/nguoibenh")]
public class BenhnhanController : ControllerBase
{
    private readonly HisDbContext _hisDbContext;
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<BenhnhanController> _logger;
    private readonly HoSoBenhNhanService _hoSoService;

    private readonly DonthuocService _donthuocService;

    public BenhnhanController( DonthuocService donthuocService, HisDbContext hisDbContext, AppDbContext appDbContext, ILogger<BenhnhanController> logger, HoSoBenhNhanService hoSoService)
    {
        _hisDbContext = hisDbContext;
        _appDbContext = appDbContext;
        _logger = logger;
        _hoSoService = hoSoService;
        _donthuocService = donthuocService;
    }


    [HttpPost("them-ho-so")]
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
