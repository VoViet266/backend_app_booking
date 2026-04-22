using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace his_backend.Controller;

/// <summary>
/// Base controller với các helper methods cho authentication & authorization
/// </summary>
public abstract class BaseApiController : ControllerBase
{
    /// <summary>
    /// Lấy ID của user đang đăng nhập
    /// </summary>
    protected int? LayUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)
                ?? User.FindFirst("sub");
        return claim is not null && int.TryParse(claim.Value, out var id) ? id : null;
    }

    protected bool IsUserAuthorizedForUser(int? currentUserId, int targetUserId)
    {
        return currentUserId is not null && currentUserId == targetUserId;
    }

    /// <summary>
    /// Helper để trả về lỗi authorization
    /// </summary>
    protected IActionResult ForbidAccess()
    {
        return StatusCode(
            StatusCodes.Status403Forbidden,
            new { message = "Bạn không có quyền truy cập tài nguyên này" }
        );
    }

    /// <summary>
    /// Helper để trả về lỗi unauthorized
    /// </summary>
    protected IActionResult UnauthorizedResponse()
    {
        return StatusCode(
            StatusCodes.Status401Unauthorized,
            new { message = "Không xác định được người dùng" }
        );
    }
}
