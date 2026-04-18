using his_backend.DTOs;
using his_backend.Services;
using his_backend.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.RateLimiting;

namespace his_backend.Controller;


[ApiController]
[Route("api/auth")]
[Produces("application/json")]
public class AuthController(IAuthService authService, ILogger<AuthController> logger) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly ILogger<AuthController> _logger = logger;

    [HttpPost("dang-ky")]
    [EnableRateLimiting("auth")]
    [ProducesResponseType(typeof(ServiceResult<NguoiDungInfo>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ServiceResult<NguoiDungInfo>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResult<NguoiDungInfo>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DangKy([FromBody] DangKyRequest req)
    {
        if (!ModelState.IsValid)
            return BadRequest(ServiceResult<NguoiDungInfo>.Fail("Thông tin không hợp lệ"));
    
        var result = await _authService.DangKyAsync(req);
        return result.Success
            ? StatusCode(StatusCodes.Status201Created,
                ServiceResult<NguoiDungInfo>.Ok(result.Data!, result.Message))
            : StatusCode(result.StatusCode,
                ServiceResult<NguoiDungInfo>.Fail(result.Message));
    }

    [HttpPost("dang-nhap")]
    [EnableRateLimiting("auth")]
    [ProducesResponseType(typeof(ServiceResult<DangNhapResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<DangNhapResponse>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DangNhap([FromBody] DangNhapRequest req)
    {
        if (!ModelState.IsValid)
            return Unauthorized(ServiceResult<DangNhapResponse>.Fail("Thông tin không hợp lệ"));

        var result = await _authService.DangNhapAsync(req);
        if (result.Success && result.Data != null)
        {   

            await _authService.SaveDeviceTokenAsync(result.Data.NguoiDung.Id ?? 0, req.DeviceId ?? "", req.FcmToken ?? "");
        }
        return result.Success
            ? Ok(ServiceResult<DangNhapResponse>.Ok(result.Data!))
            : StatusCode(result.StatusCode,
                ServiceResult<DangNhapResponse>.Fail(result.Message));
    }

    [HttpPost("lam-moi-token")]
    [EnableRateLimiting("auth")]
    [ProducesResponseType(typeof(ServiceResult<DangNhapResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<DangNhapResponse>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LamMoiToken([FromBody] RefreshTokenRequest req)
    {
        if (!ModelState.IsValid)
            return BadRequest(ServiceResult<DangNhapResponse>.Fail("Thông tin không hợp lệ"));

        var result = await _authService.LamMoiTokenAsync(req.RefreshToken);
        return result.Success
            ? Ok(ServiceResult<DangNhapResponse>.Ok(result.Data!))
            : StatusCode(result.StatusCode,
                ServiceResult<DangNhapResponse>.Fail(result.Message));
    }


    [HttpPost("dang-xuat")]
    [Authorize]
    [ProducesResponseType(typeof(ServiceResult<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DangXuat()
    {
        var userId = LayUserId();
        if (userId is null)
            return Unauthorized(ServiceResult<object>.Fail("Token không hợp lệ"));

        var result = await _authService.DangXuatAsync(userId.Value);
        return Ok(ServiceResult<object>.Ok(null!, result.Message));
    }

    [HttpPut("doi-mat-khau")]
    [Authorize]
    [EnableRateLimiting("auth")]
    [ProducesResponseType(typeof(ServiceResult<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DoiMatKhau([FromBody] DoiMatKhauRequest req)
    {
        if (!ModelState.IsValid)
            return BadRequest(ServiceResult<object>.Fail("Thông tin không hợp lệ"));

        var userId = LayUserId();
        if (userId is null)
            return Unauthorized(ServiceResult<object>.Fail("Không xác định được người dùng"));

        var result = await _authService.DoiMatKhauAsync(userId.Value, req);
        return result.Success
            ? Ok(ServiceResult<object>.Ok(null!, result.Message))
            : StatusCode(result.StatusCode, ServiceResult<object>.Fail(result.Message));
    }

    [HttpPut("quen-mat-khau")]
    [EnableRateLimiting("auth")]
    [ProducesResponseType(typeof(ServiceResult<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> QuenMatKhau([FromBody] QuenMatKhauRequest req)
    {
        if (!ModelState.IsValid)
            return BadRequest(ServiceResult<object>.Fail("Thông tin không hợp lệ"));

        var result = await _authService.QuenMatKhauAsync(req);
        return result.Success
            ? Ok(ServiceResult<object>.Ok(null!, result.Message))
            : StatusCode(result.StatusCode, ServiceResult<object>.Fail(result.Message));
    }

    [HttpPost("verify-account")]
    [EnableRateLimiting("auth")]
    [ProducesResponseType(typeof(ServiceResult<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> XacThucTaiKhoan([FromBody] VerifyAccountRequest req)
    {
        if (!ModelState.IsValid)
            return BadRequest(ServiceResult<object>.Fail("Thông tin không hợp lệ"));

        var result = await _authService.verifyAccountAsync(req);
        return result.Success
            ? Ok(ServiceResult<object>.Ok(null!, result.Message))
            : StatusCode(result.StatusCode, ServiceResult<object>.Fail(result.Message));
    }


    private int? LayUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)
                ?? User.FindFirst("sub");
        return claim is not null && int.TryParse(claim.Value, out var id) ? id : null;
    }
}
