using Microsoft.AspNetCore.Mvc;
using his_backend.Services;
using his_backend.Common;
using his_backend.DTOs;
using Microsoft.AspNetCore.RateLimiting;


[ApiController]
[Route("api/chinhanh")]
public class ChinhanhController(chinhanhService chinhanhService) : ControllerBase
{
    private readonly chinhanhService _chinhanhService = chinhanhService;

    [HttpGet]
    public async Task<IActionResult> LayDanhSachChinhanh()
    {
        var result = await _chinhanhService.GetAllChinhanh();
        return result.Success
            ? Ok(result)
            : NotFound(result);
    }
}