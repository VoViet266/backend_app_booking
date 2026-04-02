using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace his_backend.Models;

[Table("dmthuoc", Schema = "current")]
public partial class Dmthuoc
{
    public string Mahh { get; set; } = null!;

    public string? Tenhh { get; set; }

    public string? Tenhc { get; set; }

    public string? Tenhcp { get; set; }

    public string? Dvt { get; set; }

    public string? Quicachdg { get; set; }

    public string? Khoql { get; set; }

    public string? Manhom { get; set; }

    public string? Manpp { get; set; }

    public string? Kho { get; set; }

    public string? Nhasx { get; set; }

    public string? Nuocsx { get; set; }

    public string? Maloai { get; set; }

    public string? Taikhoan { get; set; }

    public string? Tenmay { get; set; }

    public decimal? Xoa { get; set; }

    public DateTime? Ngayxoa { get; set; }

    public string? Noingoai { get; set; }

    public string? Theodon { get; set; }

    public string? Mahhthau { get; set; }

    public decimal? Thuocsd { get; set; }

    public decimal? Thuock { get; set; }

    public decimal? Dachat { get; set; }

    public decimal? Ktcao { get; set; }

    public string? Hamluong { get; set; }

    public decimal? Dacbiet { get; set; }

    public string? TenhhByt { get; set; }

    public string? SttByt { get; set; }

    public decimal? Phache { get; set; }

    public decimal? Thaythe { get; set; }

    public string? MahhByt { get; set; }

    public decimal? Tyle { get; set; }

    public decimal? TyleTt { get; set; }

    public decimal? Vatnhap { get; set; }

    public string? Madd { get; set; }

    public string? MaHoatChat { get; set; }

    public string? SttThau { get; set; }

    public string? QuyetDinh { get; set; }

    public string? CongBo { get; set; }

    public decimal? LoaiThuoc { get; set; }

    public decimal? LoaiThau { get; set; }

    public string? NhomThau { get; set; }

    public string? MaNhomVtyt917 { get; set; }

    public string? TenNhomVtyt917 { get; set; }

    public string? MaHieu { get; set; }

    public decimal? DinhMuc { get; set; }

    public string? Ghichu { get; set; }

    public string? Sodk { get; set; }

    public decimal? Ao { get; set; }

    public decimal? HanghoaCnk { get; set; }

    public string? NamThau { get; set; }

    public decimal? Thuocd { get; set; }

    public decimal? TTrantt { get; set; }

    public string? TtThau { get; set; }

    public string? Cophim { get; set; }

    public decimal? Stent { get; set; }

    public decimal? Qtrieng { get; set; }

    public string? Mahhtt { get; set; }

    public string? MathuocDuocquocgia { get; set; }

    public decimal? Khonglamtron { get; set; }

    public decimal? Nguonkhac { get; set; }

    public decimal? Loainguonkhac { get; set; }

    public decimal? Viemganc { get; set; }

    public decimal? Covid19 { get; set; }

    public decimal? Loainguonbhyt { get; set; }

    public decimal? NguonCovid19Nsnn { get; set; }

    public decimal? NguonCovid19Ttvt { get; set; }

    public string? MaPpChebien { get; set; }

    public string? Dangbc { get; set; }

    public string? MaCskcbThuoc { get; set; }

    public string? TtThau4750 { get; set; }

    public decimal? Tienich { get; set; }

    public decimal? Songaytoithieu { get; set; }

    public string? Maloaitoa { get; set; }

    public decimal SoluongMax { get; set; }

    public decimal? Gioitinh { get; set; }
}
