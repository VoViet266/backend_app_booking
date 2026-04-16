public class DmbenhnhanDto
{
    public int? Id { get; set; }
    public required string Mabn { get; set; } 
    public required string Holot { get; set; } 
    public required string Ten { get; set; } 
    public DateOnly? Ngaysinh { get; set; }
    public decimal? Gioitinh { get; set; }
    public string? Diachi { get; set; }
    public string? Sdt { get; set; }
    public string? Mathe { get; set; }
}
public class LichSuKhamDto
{
    public required string Makb       { get; set; }
    public DateTime? Ngaykham { get; set; }
    public string? Maicd      { get; set; }  
    public string? TenvietIcd  { get; set; } 
}
public class BenhNhanLichSuDto
{
    public required string Mabn { get; set; }
    public required string Hoten { get; set; }
    public List<LichSuKhamDto> LichSu { get; set; }
}