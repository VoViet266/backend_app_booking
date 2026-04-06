using System.ComponentModel.DataAnnotations;

namespace his_backend.DTOs;


public class DatLichKhamRequest
{
    [Required(ErrorMessage = "Họ tên không được để trống")]
    [MaxLength(255)]
    public string HoTen { get; set; } = null!;

    public DateOnly Ngaysinh { get; set; }

    public decimal? Gioitinh { get; set; }

    [MaxLength(500)]
    public string? Diachi { get; set; }

    [MaxLength(12)]
    [RegularExpression(@"^(0[3|5|7|8|9])+([0-9]{8})$",
        ErrorMessage = "Số điện thoại không hợp lệ")]
    public string? Sdt { get; set; }

    [MaxLength(12)] 
    public string? Cmnd { get; set; }
    
    public int Mandk { get; set; }
    public int Mapk { get; set; }
    [Required(ErrorMessage = "Mã chuyên khoa không được để trống")]
    [MaxLength(20)]
    public string MaCk{ get; set; } = null!;

    [Required(ErrorMessage = "Mã bác sĩ không được để trống")]
    [MaxLength(20)]
    public string Mabs { get; set; } =   null!;
    
    [MaxLength(50)]
    public string? Mathe { get; set; }

    [MaxLength(30)]
    public string? LoaiQh  { get; set; } = "";

    public string? DienThoaiQh { get; set; }

    public string? DiachiQh { get; set; }

    [MaxLength(255)]
    public string? HotenQh { get; set; }
   
    public DateOnly Ngay { get; set; }

    public DateTimeOffset? TimeSlot { get; set; }

    [MaxLength(50)]
    public string? LoaiKham { get; set; } 

    [MaxLength(500)]
    public string? GhiChu { get; set; }
}
    public class DatLichKhamResponse
{
    public int    MaDk      { get; set; }
    public string HoTen     { get; set; } = null!;
    public DateOnly Ngaysinh { get; set; }
    public DateOnly Ngay     { get; set; }
    public string Cmnd { get; set; } = null!;
    public DateTimeOffset? TimeSlot { get; set; }
    public string MaCk      { get; set; } = null!;
    public string? TenCk    { get; set; }
    public string   Mabs      { get; set; }
    public string? TenBacSi { get; set; }
    public string TrangThai  { get; set; } = null!;
    public DateTime NgayDat  { get; set; }
    public string? LoaiQh { get; set; }
    public string? DienThoaiQh { get; set; }
    public string? DiachiQh { get; set; }
    public string? HotenQh { get; set; }
}

public class LichDaDatResponse
{
    public int      MaDk       { get; set; }
    public string   HoTen      { get; set; } = null!;
    public DateOnly Ngay       { get; set; }
    public string LoaiQh { get; set; }
    public DateTimeOffset? TimeSlot { get; set; }
    public string   MaCk       { get; set; } = null!;
    public string?  TenCk      { get; set; }
    public string   Mabs       { get; set; }
    public string Cmnd { get; set; } = null!;
    public string?  TenBacSi   { get; set; }
    public int   TrangThai  { get; set; }
    public DateTime NgayDat    { get; set; }
    public string?  GhiChu     { get; set; }
    public string? DienThoaiQh { get; set; }
    public string? DiachiQh { get; set; }
    public string? HotenQh { get; set; }
}


public class HuyLichRequest
{
    [MaxLength(500)]
    public string? LyDo { get; set; }
}
