using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using his_backend.DTOs;
using his_backend.Common;
using his_backend.Services.dangkykham;
using Microsoft.AspNetCore.RateLimiting;
namespace his_backend.Controller;

[ApiController]
[Route("api/dangky")]
public class DangkykbController(IDangkykbService dangkykbService) : ControllerBase
{
    private readonly IDangkykbService _dangkykbService = dangkykbService;

    /// <summary>
    /// Đặt lịch khám bệnh.
    /// - Guest: không cần token, điền đầy đủ họ tên/ngày sinh/sdt.
    /// - User đăng nhập: có thể truyền HoSoId để tự động fill thông tin.
    /// </summary>
    [HttpPost]
    [EnableRateLimiting("realtime")]
    [ProducesResponseType(typeof(ServiceResult<DatLichKhamResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<DatLichKhamResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DatLich([FromBody] DatLichKhamRequest req)
    {
        if (!ModelState.IsValid)
            return BadRequest(ServiceResult<DatLichKhamResponse>.Fail(
                string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)), 400));

        var userId = LayUserId();
        var result = await _dangkykbService.DatLichAsync(req, userId);

        return result.Success
            ? Ok(result)
            : StatusCode(result.StatusCode, result);
    }

    [HttpGet("cua-toi")]
    [Authorize]
    [EnableRateLimiting("normal")]
    [ProducesResponseType(typeof(ServiceResult<List<LichDaDatResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> LayDanhSachLich()
    {
        var userId = LayUserId();
        if (userId is null)
            return Unauthorized(ServiceResult<List<LichDaDatResponse>>.Fail("Chưa đăng nhập", 401));

        var result = await _dangkykbService.LayDanhSachLichAsync(userId.Value);
        return Ok(result);
    }


    [HttpGet("chi-tiet-lich-dat/{id}")]
    [EnableRateLimiting("normal")]
    public async Task<IActionResult> LayChiTietLichDat([FromRoute] int id)
    {
        var userId = LayUserId();
        if (userId is null)
            return Unauthorized(ServiceResult<LichDaDatResponse>.Fail("Chưa đăng nhập", 401));
        var result = await _dangkykbService.LayChiTietLichAsync(id, userId.Value ) ;
        return Ok(result);
    }


    [HttpGet("lich-theo-ngay/{ngay}")]
    [EnableRateLimiting("normal")]
    [ProducesResponseType(typeof(ServiceResult<List<LichDaDatResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> LayLichTheoNgay([FromRoute] DateOnly ngay)
    {
        var result = await _dangkykbService.LayLichDangKyTheoNgayAsync(ngay);
        return Ok(result);
    }

    private int? LayUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue("sub");

        return int.TryParse(claim, out var id) ? id : null;
    }
}