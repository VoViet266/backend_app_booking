using System;
using System.Collections.Generic;

namespace his_backend.Models;

public partial class Chidinhcl
{
    public string? Mabn { get; set; }

    public string? Makb { get; set; }

    public decimal? Noitru { get; set; }

    public string? Madt { get; set; }

    public string? Madv { get; set; }

    public string? Maphong { get; set; }

    public DateTime? Ngaykcb { get; set; }

    public string? Manv { get; set; }

    public string? Soct { get; set; }

    public string? Soctvp { get; set; }

    public string? Macls { get; set; }

    public string? Makl { get; set; }

    public string? Ketluan { get; set; }

    public decimal? Soluong { get; set; }

    public decimal? Dongia { get; set; }

    public decimal? Giabh { get; set; }

    public decimal? Chenhlech { get; set; }

    public decimal? Miengiam { get; set; }

    public decimal? Ptmiengiam { get; set; }

    public decimal? Thanhtien { get; set; }

    public decimal? Dath { get; set; }

    public decimal? Dain { get; set; }

    public decimal? Dathu { get; set; }

    public string? Taikhoan { get; set; }

    public string? Tenmay { get; set; }

    public decimal? Xoa { get; set; }

    public DateTime? Ngayxoa { get; set; }

    public string? Thangkt { get; set; }

    public string? Namkt { get; set; }

    public string? Maicd { get; set; }

    public string? Kqcdoan { get; set; }

    public decimal? Dichvu { get; set; }

    public string? Sogiuong { get; set; }

    public string? Maba { get; set; }

    public string? Mathe { get; set; }

    public decimal? Thanhtienmg { get; set; }

    public string? Loaixn { get; set; }

    public string? Id { get; set; }

    public decimal? Stt { get; set; }

    public string? Iddienbien { get; set; }

    public decimal? Tinhtrang { get; set; }

    public decimal? Tamin { get; set; }

    public decimal? Intoadieutri { get; set; }

    public DateTime? Ngaycd { get; set; }

    public decimal? Travedieutri { get; set; }

    public string? Buong { get; set; }

    public decimal? Toacon { get; set; }

    public string? Macon { get; set; }

    public decimal? Dongiausd { get; set; }

    public decimal? Thanhtienusd { get; set; }

    public decimal? Tygia { get; set; }

    public decimal? Bhyt { get; set; }

    public decimal? Ktcao { get; set; }

    public decimal? Ldanh { get; set; }

    public decimal? Pttraituyen { get; set; }

    public string? Soctvphd { get; set; }

    public string? Soctvpbltong { get; set; }

    public DateTime? Giocls { get; set; }

    public decimal? Thuphi { get; set; }

    public decimal? Mienphi { get; set; }

    public string? Divat { get; set; }

    public string? MadvDichvu { get; set; }

    public decimal? Bant { get; set; }

    public string? Soctvpcl { get; set; }

    public decimal? Chiphint { get; set; }

    public string? Idchidinh { get; set; }

    public decimal? Ghino { get; set; }

    public string? Soth { get; set; }

    public string? Idcobas { get; set; }

    public string? Tenclsphu { get; set; }

    public string? ThangQt { get; set; }

    public string? NamQt { get; set; }

    public decimal? Tile { get; set; }

    public decimal? Giabhdm { get; set; }

    public string? MaGiuong { get; set; }

    public string? Ppcham { get; set; }

    public string? Manoigt { get; set; }

    public decimal? Solaninchidinh { get; set; }

    public string? Maicdp { get; set; }

    public string? Kqcdoanp { get; set; }

    public DateTime? Giolaymau { get; set; }

    public decimal? Thatthuchenhlech { get; set; }

    public decimal? Dalappttt { get; set; }

    public decimal? SttLed { get; set; }

    public string? Sophong { get; set; }

    public string? MaclsByt { get; set; }

    public string? Tinhtranglaymau { get; set; }

    public string? Tinhtrangmau { get; set; }

    public DateTime? Ngaygiolaymau { get; set; }

    public decimal? Sdnguonkhac { get; set; }

    public decimal? LoaimaubandauMautinhmach { get; set; }

    public decimal? LoaimaubandauNuoctieu { get; set; }

    public decimal? LoaimaubandauKhac { get; set; }

    public string? LoaimaubandauGhichu { get; set; }

    public decimal? Chatluongmau { get; set; }

    public DateTime? Thoigiannhanphieu { get; set; }

    public DateTime? Ngaytraketqua { get; set; }

    public string? TaikhoanTraketqua { get; set; }

    public DateTime? Ngaykq { get; set; }

    public decimal? Dinhsuat { get; set; }

    public string? Mayhct { get; set; }

    public string? Tenyhct { get; set; }

    public string? IdTebaotucung { get; set; }

    public string? PayPayid { get; set; }

    public string? Idpacs { get; set; }

    public string? PayType { get; set; }

    public string? Idbundle { get; set; }

    public string? Idbundlereport { get; set; }

    public string? NguoiThucHien { get; set; }

    public decimal? Api { get; set; }

    public string? KtvthTmd { get; set; }

    public DateTime? KtvthNgayTmd { get; set; }

    public virtual Dmbenhnhan? MabnNavigation { get; set; }
}
