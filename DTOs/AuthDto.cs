using System.ComponentModel.DataAnnotations;

namespace his_backend.DTOs;

public class ThemThongTinRequest
{
    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    [RegularExpression(@"^(0[3|5|7|8|9])+([0-9]{8})$",
        ErrorMessage = "Số điện thoại không hợp lệ (VD: 0901234567)")]
    public string SoDienThoai { get; set; } = null!;

    [MaxLength(255, ErrorMessage = "Họ lót tối đa 255 ký tự")]
    public string? Holot { get; set; }

    [MaxLength(255 , ErrorMessage = "Tên tối đa 255 ký tự")]
    public string? Ten { get; set; }

    public DateTimeOffset? NgaySinh { get; set; }

    [MaxLength(5, ErrorMessage = "Giới tính tối đa 5 ký tự")]
    public string? Gioitinh { get; set; }       // "Nam" | "Nữ" | "Khác"

    [MaxLength(12, ErrorMessage = "CCCD tối đa 12 ký tự")]
    public string? Cccd { get; set; }

    public DateTimeOffset? NgayCapCccd { get; set; }

    [MaxLength(500, ErrorMessage = "Địa chỉ chi tiết tối đa 500 ký tự")]
    public string? DiaChiChiTiet { get; set; }

    [MaxLength(50, ErrorMessage = "Tên thành phố tối đa 50 ký tự")]
    public string? TenThanhPho { get; set; }

    [MaxLength(50, ErrorMessage = "Tên xã tối đa 50 ký tự")]
    public string? TenXa { get; set; }

    [MaxLength(50, ErrorMessage = "Dân tộc tối đa 50 ký tự")]
    public string? DanToc { get; set; }

    [MaxLength(50, ErrorMessage = "Tôn giáo tối đa 50 ký tự")]
    public string? TonGiao { get; set; }

    [MaxLength(50, ErrorMessage = "Quốc tịch tối đa 50 ký tự")]
    public string? QuocTich { get; set; }
}

public class DangNhapRequest
{
    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    public string SoDienThoai { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    public string MatKhau { get; set; } = null!;

    public string? FcmToken { get; set; } = null!;

    public string? DeviceId { get; set; } = null!;


}

public class DoiMatKhauRequest
{
    [Required(ErrorMessage = "Mật khẩu cũ không được để trống")]
    public string MatKhauCu { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
    [MinLength(6, ErrorMessage = "Mật khẩu mới tối thiểu 6 ký tự")]
    public string MatKhauMoi { get; set; } = null!;
}

public class QuenMatKhauRequest
{
    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    public string SoDienThoai { get; set; } = null!;


    [Required(ErrorMessage = "CMND không được để trống")]
    [RegularExpression(@"^\d{9}(\d{3})?$", ErrorMessage = "CMND/CCCD phải là 9 hoặc 12 chữ số")]
    public string Cmnd { get; set; } = null!;


    [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
    [MinLength(6, ErrorMessage = "Mật khẩu mới tối thiểu 6 ký tự")]
    public string MatKhauMoi { get; set; } = null!;
}

public class VerifyAccountRequest
{
    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    public string SoDienThoai { get; set; } = null!;

    [Required(ErrorMessage = "CMND không được để trống")]
    public string Cmnd { get; set; } = null!;
}

public class RefreshTokenRequest
{
    [Required(ErrorMessage = "Refresh token không được để trống")]
    public string RefreshToken { get; set; } = null!;
}



public class DangNhapResponse
{
    public string AccessToken  { get; set; } = null!;
    public int ExpiresIn { get; set; }
    public NguoiDungInfo NguoiDung { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}



public class DangKyRequest
{
    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    public string SoDienThoai { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự")]
    public string MatKhau { get; set; } = null!;    

    [Required(ErrorMessage = "CMND không được để trống")]
    public string Cmnd { get; set; } = null!;

    public string? Token    { get; set; }  

    public string? Holot { get; set; } = null!;

    public string? Ten { get; set; } = null!;
}


