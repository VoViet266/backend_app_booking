using System.ComponentModel.DataAnnotations;

namespace his_backend.DTOs;
public class HoSoBenhNhan
{
    public required string Holot { get; set; } 
    public required string Ten { get; set; } 
    public DateOnly? Ngaysinh { get; set; }
    public decimal? Gioitinh { get; set; }
    public string? Diachi { get; set; }
    [MaxLength(15, ErrorMessage = "Số điện thoại không hợp lệ")]
    [RegularExpression("^[0-9]+$", ErrorMessage = "Số điện thoại không hợp lệ")]
    public string? Sodienthoai { get; set; }
    [MaxLength(12, ErrorMessage = "CMND không hợp lệ")] 
    public required string Cmnd { get; set; }
    public string? Maqg { get; set; }
    public string? NhomMau { get; set; }
    public string? Mathe { get; set; }
    public string? Madt { get; set; }
    public string? Manghe { get; set; }
    public string? Maxa { get; set; }
    public string? Matinh { get; set; }
    public string? Maloaigiayto { get; set; }
    public DateOnly? Ngaycap { get; set; }
    public string? Noicap { get; set; }
    public bool? LaMacDinh { get; set; }
}
public class HoSoBenhNhanResponse : HoSoBenhNhan
{
    public int? Id { get; set; }
    public string? HoTen { get; set; } 

    public string? QuanHe { get; set; } 

    public bool LaMacDinh { get; set; }

    public DateTimeOffset NgayLienKet { get; set; }
}
public class CapNhatHoSoRequest : HoSoBenhNhan
{
}

public class ThemHosoRequest : HoSoBenhNhan
{
    public string? QuanHe { get; set; } 
}

public class CapNhatLienKetRequest : HoSoBenhNhan
{
    [Required]
    public int HoSoId { get; set; }
}

public class CapNhatHosoRequest: HoSoBenhNhan
{
    [Required]
    public int Id { get; set; }
}

