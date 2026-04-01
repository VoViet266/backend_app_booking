using System.ComponentModel.DataAnnotations;

namespace his_backend.DTOs;
public class HoSoBenhNhan
{
    public string Holot { get; set; } = null!;
    public string Ten { get; set; } = null!;
    public DateOnly? Ngaysinh { get; set; }
    public decimal? Gioitinh { get; set; }
    public string? Diachi { get; set; }
    public string? Sodienthoai { get; set; }
    public string? Cmnd { get; set; }
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
    public string HoTen { get; set; } = null!;

    public string QuanHe { get; set; } = null!;

    public bool LaMacDinh { get; set; }

    public DateTimeOffset NgayLienKet { get; set; }
}
public class CapNhatHoSoRequest : HoSoBenhNhan
{
}

public class ThemHosoRequest : HoSoBenhNhan
{
    public string QuanHe { get; set; } = null!;
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

