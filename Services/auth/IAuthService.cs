using his_backend.DTOs;
using his_backend.Common;

namespace his_backend.Services;

public interface IAuthService
{
    Task<ServiceResult<NguoiDungInfo>> DangKyAsync(DangKyRequest req);

    Task<ServiceResult<DangNhapResponse>> DangNhapAsync(DangNhapRequest req);

    Task<ServiceResult<DangNhapResponse>> LamMoiTokenAsync(string refreshToken);

    Task<ServiceResult<object>> DangXuatAsync(int userId);

    Task<ServiceResult<object>> DoiMatKhauAsync(int userId, DoiMatKhauRequest req);

    

}