using his_backend.DTOs;
using his_backend.Services;
using his_backend.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using System.Security.Claims;
using his_backend.Models;

namespace his_backend.Controller;


[ApiController]
[Route("api/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService           _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger      = logger;
    }

    [HttpPost("dang-ky")]
    [ProducesResponseType(typeof(ServiceResult<NguoiDungInfo>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ServiceResult<NguoiDungInfo>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResult<NguoiDungInfo>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DangKy([FromBody] DangKyRequest req)
    {
        _logger.LogInformation("Đăng ký tài khoản mới: {Sdt}", req.SoDienThoai);
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
    [ProducesResponseType(typeof(ServiceResult<DangNhapResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<DangNhapResponse>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DangNhap([FromBody] DangNhapRequest req)
    {
        if (!ModelState.IsValid)
            return BadRequest(ServiceResult<DangNhapResponse>.Fail("Thông tin không hợp lệ"));

        var result = await _authService.DangNhapAsync(req);
        if (result.Success && result.Data != null)
        {   

            await _authService.SaveDeviceTokenAsync(result.Data.NguoiDung.Id, req.DeviceId, req.FcmToken);
        }
        return result.Success
            ? Ok(ServiceResult<DangNhapResponse>.Ok(result.Data!))
            : StatusCode(result.StatusCode,
                ServiceResult<DangNhapResponse>.Fail(result.Message));
    }

    [HttpPost("lam-moi-token")]
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




    private int? LayUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)
                ?? User.FindFirst("sub");
        return claim is not null && int.TryParse(claim.Value, out var id) ? id : null;
    }
}
