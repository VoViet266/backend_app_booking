namespace his_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
[Table("app_nguoibenhdangky", Schema = "datlichkham")]
public class Nguoibenhdangky
{
    public int Id { get; set; }
    public string Holot { get; set; } = null!;
    public string Ten { get; set; } = null!;
    public DateOnly? Ngaysinh { get; set; }
    public decimal? Gioitinh { get; set; }
    public string? Diachi { get; set; }
    public string? Sodienthoai { get; set; }
    public string? Cmnd { get; set; }
    public DateOnly? Ngaycap { get; set; }
    public string? Noicap { get; set; }
    public string? Maloaigiayto { get; set; }
    public string? Maqg { get; set; } = "VN";
    public string? NhomMau { get; set; }
    public string? Mathe { get; set; }
    public string? Madt { get; set; }
    public string? Manghe { get; set; }
    public string? Maxa { get; set; }
    public string? Matinh { get; set; } 

    public ICollection<AppUserHoSo> UserLienKets { get; set; } = new List<AppUserHoSo>();
}