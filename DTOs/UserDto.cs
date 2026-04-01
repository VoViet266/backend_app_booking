using System.ComponentModel.DataAnnotations;

namespace his_backend.DTOs;

public class NguoiDungInfo
{
    public int      Id              { get; set; }
    public string   SoDienThoai     { get; set; } = null!;
    public DateTimeOffset NgayTao         { get; set; }
    public DateTimeOffset? LanDangNhapCuoi { get; set; }
    public string   Holot           { get; set; } = null!;
    public string   Ten             { get; set; } = null!;
    public DateOnly?  Ngaysinh        { get; set; }
    public decimal?  Gioitinh        { get; set; }
    public string   Cmnd            { get; set; } = null!;

}

