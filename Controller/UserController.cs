using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using his_backend.Services;
using his_backend.DTOs;
using his_backend.Common;
using Microsoft.AspNetCore.RateLimiting;
namespace his_backend.Controller;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController(IUserService userService, IHoSoBenhNhanService hoSoService) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IHoSoBenhNhanService _hoSoService = hoSoService;


    /// <summary>Lấy thông tin tài khoản đang đăng nhập</summary>
    [HttpGet("laythongtin")]
    [Authorize]
    [EnableRateLimiting("normal")]
    [ProducesResponseType(typeof(ServiceResult<NguoiDungInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<NguoiDungInfo>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMe()
    {
        var userId = LayUserId();
        if (userId is null)
            return Unauthorized(ServiceResult<NguoiDungInfo>.Fail("Không xác định được người dùng"));

        var result = await _userService.LayThongTinAsync(userId.Value);
        return result.Success
            ? Ok(ServiceResult<NguoiDungInfo>.Ok(result.Data!))
            : NotFound(ServiceResult<NguoiDungInfo>.Fail(result.Message));
    }

    [HttpGet("layuser")]
    [EnableRateLimiting("normal")]
    [Authorize]
    [ProducesResponseType(typeof(ServiceResult<NguoiDungInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<NguoiDungInfo>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser()
    {
        var userId = LayUserId();
        if (userId is null)
            return Unauthorized(ServiceResult<NguoiDungInfo>.Fail("Không xác định được người dùng"));
        var result = await _userService.LayUser(userId.Value);
        return result.Success
            ? Ok(ServiceResult<NguoiDungInfo>.Ok(result.Data!))
            : NotFound(ServiceResult<NguoiDungInfo>.Fail(result.Message));
    }


    [HttpGet("ho-so")]
    [EnableRateLimiting("normal")]
    [Authorize]
    [ProducesResponseType(typeof(ServiceResult<List<HoSoBenhNhanResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> LayDanhSachHoSo()
    {
        var userId = LayUserId();
        if (userId is null)
            return Unauthorized(ServiceResult<object>.Fail("Không xác định được người dùng"));

        var result = await _hoSoService.LayDanhSachAsync(userId.Value);
        return Ok(result);
    }

    [HttpGet("ho-so/{id:int}")]
    [EnableRateLimiting("normal")]
    [Authorize]
    [ProducesResponseType(typeof(ServiceResult<HoSoBenhNhanResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<HoSoBenhNhanResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LayChiTietHoSo(int id)
    {
        var userId = LayUserId();
        if (userId is null)
            return Unauthorized(ServiceResult<object>.Fail("Không xác định được người dùng"));

        var result = await _hoSoService.LayChiTietAsync(userId.Value, id);
        return result.Success
            ? Ok(result)
            : NotFound(ServiceResult<object>.Fail(result.Message));
    }


    [HttpGet("ho-so/lay-danhsach-hosouser/{appuserid:int}")]
    [EnableRateLimiting("normal")]
    [ProducesResponseType(typeof(ServiceResult<List<HoSoBenhNhanResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<List<HoSoBenhNhanResponse>>), StatusCodes.Status404NotFound)]
    [Authorize()]
    public async Task<IActionResult> Layhosocuauser(int appuserid, int id)
    {
        var result = await _hoSoService.LayDanhSachAsync(appuserid);
        return Ok(result);
    }

    [HttpPost("ho-so")]
    [EnableRateLimiting("realtime")]
    [ProducesResponseType(typeof(ServiceResult<HoSoBenhNhanResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ServiceResult<HoSoBenhNhanResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResult<HoSoBenhNhanResponse>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ThemHoSo([FromBody] ThemHosoRequest req)
    {

        if (!ModelState.IsValid)
            return BadRequest(ServiceResult<object>.Fail("Thông tin không hợp lệ"));

        var userId = LayUserId();
        if (userId is null)
            return Unauthorized(ServiceResult<object>.Fail("Không xác định được người dùng"));

        var result = await _hoSoService.ThemHoSoAsync(userId.Value, req);

        if (!result.Success)
            return StatusCode(result.StatusCode, ServiceResult<object>.Fail(result.Message));

        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPatch("cap-nhat-ho-so")]
    [EnableRateLimiting("realtime")]
    [Authorize]
    [ProducesResponseType(typeof(ServiceResult<HoSoBenhNhanResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<HoSoBenhNhanResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CapNhatHoSo([FromBody] CapNhatHosoRequest req)
    {

        if (req.Id <= 0)
            return Unauthorized(ServiceResult<object>.Fail("Không xác định được người dùng"));

        var result = await _hoSoService.CapNhatHoSoAsync(
            req.Id,
            req
            );
        return result.Success
            ? Ok(result)
            : NotFound(ServiceResult<object>.Fail(result.Message));
    }

    // [HttpPut("ho-so/{id:int}")]
    // [ProducesResponseType(typeof(ServiceResult<HoSoBenhNhanResponse>), StatusCodes.Status200OK)]
    // [ProducesResponseType(typeof(ServiceResult<HoSoBenhNhanResponse>), StatusCodes.Status404NotFound)]
    // public async Task<IActionResult> CapNhatHoSo(int id, [FromBody] CapNhatLienKetRequest req)
    // {
    //     if (!ModelState.IsValid)
    //         return BadRequest(ServiceResult<object>.Fail(LayLoi()));

    //     var userId = LayUserId();
    //     if (userId is null)
    //         return Unauthorized(ServiceResult<object>.Fail("Không xác định được người dùng"));

    //     var result = await _hoSoService.CapNhatLienKetAsync(userId.Value, id, req);
    //     return result.Success
    //         ? Ok(result)
    //         : StatusCode(result.StatusCode, ServiceResult<object>.Fail(result.Message));
    // }

    [HttpDelete("xoa-ho-so/{hoSoId}")]
    [EnableRateLimiting("normal")]
    [Authorize]
    [ProducesResponseType(typeof(ServiceResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> XoaHoSo(int hoSoId)
    {
        var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)
            ?? User.FindFirst("sub");
        if (claim is null || !int.TryParse(claim.Value, out var userId))
            return Unauthorized(ServiceResult<bool>.Fail("Không xác định được người dùng"));

        var result = await _hoSoService.XoaHoSoAsync(userId, hoSoId);

        if (!result.Success)
            return StatusCode(result.StatusCode, result);

        return Ok(result);
    }
    [HttpPatch("ho-so/{id:int}/mac-dinh")]
    [EnableRateLimiting("normal")]
    [ProducesResponseType(typeof(ServiceResult<HoSoBenhNhanResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResult<HoSoBenhNhanResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DatMacDinh(int id)
    {
        var userId = LayUserId();
        if (userId is null)
            return Unauthorized(ServiceResult<object>.Fail("Không xác định được người dùng"));

        var result = await _hoSoService.DatMacDinhAsync(userId.Value, id);
        return result.Success
            ? Ok(result)
            : NotFound(ServiceResult<object>.Fail(result.Message));
    }

    private int? LayUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)
                 ?? User.FindFirst("sub");
        return claim is not null && int.TryParse(claim.Value, out var id) ? id : null;
    }
}