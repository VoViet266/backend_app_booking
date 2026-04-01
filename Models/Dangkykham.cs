namespace his_backend.Models
{
    public class DangKyKham
    {
        public int MaDk { get; set; } 
        public int Mapk { get; set; }
        public int Mandk { get; set; }
        public string Mabs { get; set; }
        public string MaCk { get; set; }
        public string Hoten {get; set;}         
        public string Diachi {get; set;}
        public string Sdt {get; set;}
        public string Cmnd {get; set;}
        public string LoaiQh{get; set;}
        public string HoTenQh { get; set; }
        public string DienThoaiQh { get; set; }
        public string DiachiQh { get; set; }
        public DateOnly Ngaysinh {get; set;}
        public DateTime TimeSlot { get; set; }  
        public DateOnly Ngay { get; set; }
        public DateTime NgaySua { get; set; }
        public decimal GiaTien { get; set; }
        public int TrangThai { get; set; }
        public string LoaiKham { get; set; }
        public string GhiChu { get; set; }
         
        public string Mathe { get; set; }
        public decimal Phikham { get; set; }
        public decimal Phidv { get; set; }
        public decimal Phithuoc { get; set; }
        public string Status { get; set; }
        public bool Xoa { get; set; }
        public int HisId { get; set; }
        public string MngthisId { get; set; }
        public string HiqrCode { get; set; }



    }
}