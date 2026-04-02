using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace his_backend.Models;

[Table("psdangky", Schema = "current")] 
public partial class Psdangky
{
    public string Mabn { get; set; } = null!;

    public string Makb { get; set; } = null!;

    public string? Maba { get; set; }

    public decimal? Sott { get; set; }

    public string? Mathe { get; set; }

    public DateOnly? Ngaybd { get; set; }

    public DateOnly? Ngaykt { get; set; }

    public string? Mabvdk { get; set; }

    public string? Macc { get; set; }

    public string? Madt { get; set; }

    public decimal? Tuoi { get; set; }

    public decimal? Dvttuoi { get; set; }

    public string? Noigt { get; set; }

    public string? Cdoantd { get; set; }

    public string? Loaiqh { get; set; }

    public string? Hotenqh { get; set; }

    public string? Dienthoaiqh { get; set; }

    public string? Diachiqh { get; set; }

    public string? Huyetap { get; set; }

    public decimal? Mach { get; set; }

    public decimal? Nhiptho { get; set; }

    public decimal? Khamdv { get; set; }

    public decimal? Tienck { get; set; }

    public DateTime? Ngaydk { get; set; }

    public decimal? Ttkham { get; set; }

    public string? Madv { get; set; }

    public string? Maphong { get; set; }

    public string? Sogiuong { get; set; }

    public string? Tiepnhan { get; set; }

    public string? Taikhoan { get; set; }

    public string? Tenmay { get; set; }

    public decimal? Xoa { get; set; }

    public DateTime? Ngayxoa { get; set; }

    public string? Thangkt { get; set; }

    public string? Namkt { get; set; }

    public decimal? Chieucao { get; set; }

    public decimal? Vongdau { get; set; }

    public decimal? Vongnguc { get; set; }

    public decimal? Cannang { get; set; }

    public decimal? Nhietdo { get; set; }

    public decimal? Dathu { get; set; }

    public decimal? Tienskb { get; set; }

    public string? Manvvv { get; set; }

    public string? Mabvkb { get; set; }

    public decimal? Tuyen { get; set; }

    public string? Manoicap { get; set; }

    public decimal? Themoi { get; set; }

    public DateTime? Ngayrv { get; set; }

    public decimal? Ravien { get; set; }

    public decimal? Noitru { get; set; }

    public decimal? Cothe { get; set; }

    public decimal? Giaycv { get; set; }

    public decimal? Luongtt { get; set; }

    public decimal? Tienthu { get; set; }

    public decimal? Luubenh { get; set; }

    public decimal? Taikham { get; set; }

    public DateTime? Ngaybdhen { get; set; }

    public DateTime? Ngaytaikham { get; set; }

    public decimal? Cotk { get; set; }

    public string? Manoigt { get; set; }

    public decimal? Psphaitra { get; set; }

    public decimal? Hangbv { get; set; }

    public decimal? Ptthu { get; set; }

    public decimal? Uutien { get; set; }

    public decimal? Tngt { get; set; }

    public decimal? Cbsv { get; set; }

    public DateTime? Giodk { get; set; }

    public decimal? Nhanphieu { get; set; }

    public string? Madvhd { get; set; }

    public decimal? Vanchuyen { get; set; }

    public decimal? Cv2384 { get; set; }

    public string? Mahongheo { get; set; }

    public decimal? Ptngansach { get; set; }

    public decimal? Mienchitra { get; set; }

    public string? Dtss { get; set; }

    public decimal? Tuyenbv { get; set; }

    public decimal? Lydoct { get; set; }

    public string? Chuyenmon { get; set; }

    public decimal? Chandoanphuhop { get; set; }

    public decimal? Chandoankhacbiet { get; set; }

    public decimal? Phanhoi { get; set; }

    public string? Maicdtd { get; set; }

    public decimal? Chuyentuyenduoi { get; set; }

    public decimal? Bant { get; set; }

    public string? Bacluong { get; set; }

    public decimal? Namsinh { get; set; }

    public decimal? TienthuKtc { get; set; }

    public string? Ghichu { get; set; }

    public string? Giayxacnhan { get; set; }

    public string? Giaychungsinh { get; set; }

    public decimal? Bntaikham { get; set; }

    public decimal? XuatXml917 { get; set; }

    public string? ThangQt { get; set; }

    public string? NamQt { get; set; }

    public string? Kqcdoan { get; set; }

    public string? Maicd { get; set; }

    public string? Maicdp { get; set; }

    public decimal? Tinhtrang { get; set; }

    public DateTime? Ngayinphieu { get; set; }

    public string? MadvInphieu { get; set; }

    public decimal? Dain { get; set; }

    public decimal? Daguibyt { get; set; }

    public decimal? Daguibhxh { get; set; }

    public string? Maphonghd { get; set; }

    public string? KetluanKsk { get; set; }

    public string? PhanloaiKsk { get; set; }

    public string? ManvKetluanKsk { get; set; }

    public decimal? TrangthaiKetluanKsk { get; set; }

    public string? ManvDangKetLuan { get; set; }

    public decimal? Dieutriopc { get; set; }

    public DateOnly? Ngaymienct { get; set; }

    public string? Cmndqh { get; set; }

    public string? SanphukhoaKsk { get; set; }

    public string? NoingoaikhoaKsk { get; set; }

    public string? DiungKsk { get; set; }

    public string? TsgiadinhKsk { get; set; }

    public string? TuyenvuKsk { get; set; }

    public string? AmdaoKsk { get; set; }

    public string? TucungKsk { get; set; }

    public string? DenghiKsk { get; set; }

    public string? KqclsKsk { get; set; }

    public decimal? Loaibn { get; set; }

    public string? KetQuaDtri { get; set; }

    public string? Socttd { get; set; }

    public DateOnly? Tungaytd { get; set; }

    public DateOnly? Denngaytd { get; set; }

    public string? TaikhoanInphieu { get; set; }

    public DateOnly? Ngay5nam { get; set; }

    public string? Mayhct { get; set; }

    public string? Tenyhct { get; set; }

    public string? MatheGh { get; set; }

    public DateOnly? NgaybdGh { get; set; }

    public DateOnly? NgayktGh { get; set; }

    public string? MaphongInphieu { get; set; }

    public string? TaikhoanKetthuc { get; set; }

    public decimal? SofilepdfXn { get; set; }

    public string? Maphongbd { get; set; }

    public decimal? Guihssk { get; set; }

    public decimal? Tiennguonkhac { get; set; }

    public decimal? Sdnguonkhac { get; set; }

    public decimal? Sms { get; set; }

    public decimal? Dinhsuat { get; set; }

    public decimal? Tongtienbv { get; set; }

    public decimal? Tongtienbh { get; set; }

    public decimal? Thetam { get; set; }

    public decimal? Tuyenxml { get; set; }

    public decimal? BenhnhanLao { get; set; }

    public string? MabvDieutriLao { get; set; }

    public DateOnly? NgaychungnhanLao { get; set; }

    public string? Soluutru { get; set; }

    public string? Keluu { get; set; }

    public decimal? Nguycotenga { get; set; }

    public string? Sangloctenga { get; set; }

    public decimal? Quetcccd { get; set; }

    public decimal? Trangthaichuyentuyen { get; set; }

    public decimal? Giayxacnhancutru { get; set; }

    public byte[]? Giayluu { get; set; }

    public byte[]? Giayluuchuyentuyen { get; set; }

    public string? Mabshk { get; set; }

    public DateOnly? Namsinhqh { get; set; }

    public decimal? Tuoiqh { get; set; }

    public decimal? GuiPkh { get; set; }

    public string? ChandoanDautien { get; set; }

    public DateTime? Ngaytv { get; set; }

    public string? Matttv { get; set; }

    public string? Matgtv { get; set; }

    public string? Nguyennhantv { get; set; }

    public string? Maicdgp { get; set; }

    public string? Kqcdoangp { get; set; }

    public string? Maicdtv { get; set; }

    public decimal? Khamnghiemtt { get; set; }

    public string? Tvtruoc24h { get; set; }

    public string? Quyen { get; set; }

    public string? So { get; set; }

    public decimal? Tvtdcapcuu { get; set; }

    public virtual Dmdoituong? MadtNavigation { get; set; }
}
