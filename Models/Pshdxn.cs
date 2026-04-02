using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace his_backend.Models;

[Table("pshdxn", Schema = "current")]
public partial class Pshdxn 
{
    public string? Sohd { get; set; }

    public DateOnly? Ngayhd { get; set; }

    public DateOnly? Ngaylap { get; set; }

    public string? Loaixn { get; set; }

    public string? Madt { get; set; }

    public string? Mabn { get; set; }

    public string? Makh { get; set; }

    public string? Khochan { get; set; }

    public string? Khole { get; set; }

    public decimal? Noitru { get; set; }

    public string? Madv { get; set; }

    public string? Maphong { get; set; }

    public string? Mahh { get; set; }

    public decimal? Gianhap { get; set; }

    public decimal? Quidoi { get; set; }

    public decimal? Vat { get; set; }

    public decimal? Giavat { get; set; }

    public decimal? Ptcong { get; set; }

    public decimal? Giaban { get; set; }

    public decimal? Ck { get; set; }

    public string? Solo { get; set; }

    public string? Handung { get; set; }

    public string? Visa { get; set; }

    public decimal? Soluong { get; set; }

    public decimal? Theodon { get; set; }

    public decimal? Tientvat { get; set; }

    public decimal? Tienvat { get; set; }

    public decimal? Tienck { get; set; }

    public decimal? Thanhtien { get; set; }

    public string? Taikhoan { get; set; }

    public string? Tenmay { get; set; }

    public string? Cachuong { get; set; }

    public string? Soctvp { get; set; }

    public decimal? Xoa { get; set; }

    public DateTime? Ngayxoa { get; set; }

    public string? Thangkt { get; set; }

    public string? Namkt { get; set; }

    public decimal? Khoaso { get; set; }

    public decimal? Thu { get; set; }

    public decimal? Loaitoa { get; set; }

    public string? Userin { get; set; }

    public string? Mathe { get; set; }

    public string? Tutruc { get; set; }

    public decimal? Toatutruc { get; set; }

    public decimal? Stt { get; set; }

    public decimal? Sang { get; set; }

    public decimal? Trua { get; set; }

    public decimal? Chieu { get; set; }

    public decimal? Toi { get; set; }

    public DateTime? Ngayth { get; set; }

    public string? Iddienbien { get; set; }

    public string? Kyhieu { get; set; }

    public decimal? Nhanct { get; set; }

    public decimal? Dain { get; set; }

    public DateTime? Giolap { get; set; }

    public decimal? Tamin { get; set; }

    public decimal? Intoadieutri { get; set; }

    public decimal? Travedieutri { get; set; }

    public decimal? Toaxv { get; set; }

    public decimal? Toacon { get; set; }

    public string? Macon { get; set; }

    public decimal? Giabhyt { get; set; }

    public decimal? Giakc { get; set; }

    public decimal? Thanhtienbhyt { get; set; }

    public string? Soctnb { get; set; }

    public decimal? Bhyt { get; set; }

    public decimal? Thuock { get; set; }

    public decimal? Inmaubhyt { get; set; }

    public string? Sohdc { get; set; }

    public string? Makhc { get; set; }

    public string? Tutrucc { get; set; }

    public decimal? Dutru { get; set; }

    public DateTime? Ngayin { get; set; }

    public decimal? Ttchinhtoa { get; set; }

    public string? Sohdnb { get; set; }

    public decimal? Muangoai { get; set; }

    public string? Thanhtoan { get; set; }

    public decimal? Tamkhoa { get; set; }

    public string? Soctvphd { get; set; }

    public DateTime? NgaythKhoa { get; set; }

    public string? Manguon { get; set; }

    public decimal? Bant { get; set; }

    public string? Maba { get; set; }

    public decimal? Solanin { get; set; }

    public decimal? Ptbanle { get; set; }

    public string? ThangQt { get; set; }

    public string? NamQt { get; set; }

    public decimal? Haohutduoclieu { get; set; }

    public decimal? Sdnguonkhac { get; set; }

    public decimal? Dacd { get; set; }

    public string? Soctcd { get; set; }

    public string? PayPayid { get; set; }

    public decimal? Dinhsuat { get; set; }

    public string? PayType { get; set; }

    public string? Sophieudutru { get; set; }

    public string? LieuDung { get; set; }

    public string? Sohdx { get; set; }

    public decimal? Loaitoatt { get; set; }

    public string? GcTmd { get; set; }

    public decimal? Stent2lan { get; set; }
}
