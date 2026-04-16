using System.ComponentModel.DataAnnotations;

namespace his_backend.DTOs;

public class NguoiDungInfo
{
    public int?      Id              { get; set; }
    public required string   SoDienThoai     { get; set; } 
    public DateTimeOffset? NgayTao         { get; set; }
    public DateTimeOffset? LanDangNhapCuoi { get; set; }
    public string?   Holot           { get; set; } 
    public string?   Ten             { get; set; } 
    public DateOnly?  Ngaysinh        { get; set; }
    public decimal?  Gioitinh        { get; set; }
    public string?   Cmnd            { get; set; } 

    
}

