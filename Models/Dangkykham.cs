using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace his_backend.Models;

[Table("app_dangkykham", Schema = "current")]
    public class DangKyKham
    {
        public int MaDk { get; set; }   
        public int? Mapk { get; set; }
        public int Mandk { get; set; }
        public string Mabs { get; set; } = null!;
        public string MaCk { get; set; } = null!;
        public string Hoten {get; set;} = null!;         
        public string Diachi {get; set;} = null!;
        public string Sdt {get; set;} = null!;
        public string Cmnd {get; set;} = null!;
        public string LoaiQh{get; set;} = null!;
        public string HoTenQh { get; set; } = null!;
        public string DienThoaiQh { get; set; } = null!;
        public string DiachiQh { get; set; } = null!;
        public DateOnly Ngaysinh {get; set;}
        public DateTime TimeSlot { get; set; }  
        public DateOnly Ngay { get; set; }
        public DateTime NgaySua { get; set; }
        public decimal GiaTien { get; set; }
        public int TrangThai { get; set; }
        public string LoaiKham { get; set; } = null!;
        public string GhiChu { get; set; } = null!;
        public string Mathe { get; set; } = null!;
        public decimal Phikham { get; set; }
        public decimal Phidv { get; set; }
        public decimal Phithuoc { get; set; }
        public string Status { get; set; } = null!;
        public bool Xoa { get; set; }
        public int HisId { get; set; }
        public string MngthisId { get; set; } = null!;
        public string HiqrCode { get; set; } = null!;
}