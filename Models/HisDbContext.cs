using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace his_backend.Models;

public partial class HisDbContext : DbContext
{
    public HisDbContext()
    {
    }

    public HisDbContext(DbContextOptions<HisDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chidinhcl> Chidinhcls { get; set; }

    public virtual DbSet<Ctthuocb> Ctthuocbs { get; set; }

    public virtual DbSet<Dmbenhnhan> Dmbenhnhans { get; set; }

    public virtual DbSet<Dmbo> Dmbos { get; set; }

    public virtual DbSet<Dmcl> Dmcls { get; set; }

    public virtual DbSet<Dmdoituong> Dmdoituongs { get; set; }

    public virtual DbSet<Dmicd> Dmicds { get; set; }

    public virtual DbSet<Dmthuoc> Dmthuocs { get; set; }

    public virtual DbSet<Khambenh> Khambenhs { get; set; }

    public virtual DbSet<Psdangky> Psdangkies { get; set; }

    public virtual DbSet<Pshdxn> Pshdxns { get; set; }

    public virtual DbSet<Dmnhanvien> Dmnhanviens { get; set; }   

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
        
            optionsBuilder.UseNpgsql("Name=ConnectionStrings:DefaultConnection");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dmnhanvien>()
        .ToTable("dmnhanvien", "current")
        .HasOne(bs => bs.ChuyenKhoa)
        .WithMany()
        .HasForeignKey(bs => bs.Mack);

        modelBuilder.Entity<Dmchuyenkhoa>()
            .ToTable("dmchuyenkhoa", "current");

        modelBuilder.Entity<Chidinhcl>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("chidinhcls", "current");

            entity.HasIndex(e => new { e.Mabn, e.Makb, e.Madt, e.Madv, e.Maphong, e.Ngaykcb, e.Manv, e.Macls, e.Dath, e.Dathu, e.Xoa, e.Ngayxoa, e.Thangkt, e.Namkt, e.Maba, e.Mathe }, "chidinhcls_idx").HasFilter("(xoa = (0)::numeric)");

            entity.HasIndex(e => new { e.Mabn, e.Maba, e.Xoa }, "chidinhcls_idx1");

            entity.HasIndex(e => new { e.Namkt, e.Thangkt, e.Ngayxoa, e.Xoa, e.Dath, e.Macls, e.Manv, e.Ngaykcb, e.Maphong, e.Madv, e.Madt, e.Makb, e.Mabn }, "chidinhcls_idx2");

            entity.HasIndex(e => new { e.Mabn, e.Makb, e.Xoa, e.Maba }, "chidinhcls_idx3");

            entity.HasIndex(e => new { e.Mabn, e.Maba, e.Xoa }, "chidinhcls_idx_cr1");

            entity.HasIndex(e => new { e.Mabn, e.Makb, e.Xoa, e.Maba }, "chidinhcls_idx_cr2");

            entity.HasIndex(e => new { e.Macls, e.Thangkt, e.Namkt }, "chidinhcls_idx_diagnose").HasFilter("(xoa = (0)::numeric)");

            entity.HasIndex(e => new { e.Macls, e.Dath, e.Xoa }, "chidinhcls_idx_diagnose_reset_dath");

            entity.HasIndex(e => new { e.Mabn, e.Makb, e.Soctvp, e.Noitru, e.Soluong, e.Ngaykcb, e.Xoa, e.Dongia, e.Giabh, e.Chenhlech }, "chidinhcls_idx_fees");

            entity.HasIndex(e => new { e.Mabn, e.Makb }, "chidinhcls_idx_report").HasFilter("(COALESCE(xoa, (0)::numeric) = (0)::numeric)");

            entity.Property(e => e.Api)
                .HasPrecision(1)
                .HasColumnName("api");
            entity.Property(e => e.Bant)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("bant");
            entity.Property(e => e.Bhyt)
                .HasPrecision(3)
                .HasDefaultValueSql("1")
                .HasColumnName("bhyt");
            entity.Property(e => e.Buong)
                .HasMaxLength(20)
                .HasColumnName("buong");
            entity.Property(e => e.Chatluongmau)
                .HasPrecision(1)
                .HasColumnName("chatluongmau");
            entity.Property(e => e.Chenhlech)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("chenhlech");
            entity.Property(e => e.Chiphint)
                .HasPrecision(1)
                .HasColumnName("chiphint");
            entity.Property(e => e.Dain)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("dain");
            entity.Property(e => e.Dalappttt)
                .HasPrecision(1)
                .HasColumnName("dalappttt");
            entity.Property(e => e.Dath)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("dath");
            entity.Property(e => e.Dathu)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("dathu");
            entity.Property(e => e.Dichvu)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("dichvu");
            entity.Property(e => e.Dinhsuat)
                .HasPrecision(1)
                .HasColumnName("dinhsuat");
            entity.Property(e => e.Divat)
                .HasMaxLength(500)
                .HasColumnName("divat");
            entity.Property(e => e.Dongia)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("dongia");
            entity.Property(e => e.Dongiausd)
                .HasPrecision(14)
                .HasColumnName("dongiausd");
            entity.Property(e => e.Ghino)
                .HasPrecision(1)
                .HasColumnName("ghino");
            entity.Property(e => e.Giabh)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("giabh");
            entity.Property(e => e.Giabhdm)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("giabhdm");
            entity.Property(e => e.Giocls)
                .HasPrecision(0)
                .HasColumnName("giocls");
            entity.Property(e => e.Giolaymau)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("giolaymau");
            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .HasColumnName("id");
            entity.Property(e => e.IdTebaotucung)
                .HasMaxLength(20)
                .HasColumnName("id_tebaotucung");
            entity.Property(e => e.Idbundle)
                .HasMaxLength(20)
                .HasColumnName("idbundle");
            entity.Property(e => e.Idbundlereport)
                .HasMaxLength(20)
                .HasColumnName("idbundlereport");
            entity.Property(e => e.Idchidinh)
                .HasMaxLength(30)
                .HasColumnName("idchidinh");
            entity.Property(e => e.Idcobas)
                .HasMaxLength(10)
                .HasColumnName("idcobas");
            entity.Property(e => e.Iddienbien)
                .HasMaxLength(40)
                .HasColumnName("iddienbien");
            entity.Property(e => e.Idpacs)
                .HasMaxLength(10)
                .HasColumnName("idpacs");
            entity.Property(e => e.Intoadieutri)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("intoadieutri");
            entity.Property(e => e.Ketluan)
                .HasMaxLength(255)
                .HasColumnName("ketluan");
            entity.Property(e => e.Kqcdoan)
                .HasMaxLength(2000)
                .HasColumnName("kqcdoan");
            entity.Property(e => e.Kqcdoanp)
                .HasMaxLength(1000)
                .HasColumnName("kqcdoanp");
            entity.Property(e => e.Ktcao)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("ktcao");
            entity.Property(e => e.KtvthNgayTmd)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ktvth_ngay_tmd");
            entity.Property(e => e.KtvthTmd)
                .HasMaxLength(4)
                .HasColumnName("ktvth_tmd");
            entity.Property(e => e.Ldanh)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("ldanh");
            entity.Property(e => e.LoaimaubandauGhichu)
                .HasColumnType("character varying")
                .HasColumnName("loaimaubandau_ghichu");
            entity.Property(e => e.LoaimaubandauKhac)
                .HasPrecision(1)
                .HasColumnName("loaimaubandau_khac");
            entity.Property(e => e.LoaimaubandauMautinhmach)
                .HasPrecision(1)
                .HasColumnName("loaimaubandau_mautinhmach");
            entity.Property(e => e.LoaimaubandauNuoctieu)
                .HasPrecision(1)
                .HasColumnName("loaimaubandau_nuoctieu");
            entity.Property(e => e.Loaixn)
                .HasMaxLength(10)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("loaixn");
            entity.Property(e => e.MaGiuong)
                .HasMaxLength(20)
                .HasColumnName("ma_giuong");
            entity.Property(e => e.Maba)
                .HasMaxLength(20)
                .HasColumnName("maba");
            entity.Property(e => e.Mabn)
                .HasMaxLength(20)
                .HasColumnName("mabn");
            entity.Property(e => e.Macls)
                .HasMaxLength(20)
                .HasColumnName("macls");
            entity.Property(e => e.MaclsByt)
                .HasMaxLength(50)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("macls_byt");
            entity.Property(e => e.Macon)
                .HasMaxLength(20)
                .HasColumnName("macon");
            entity.Property(e => e.Madt)
                .HasMaxLength(20)
                .HasColumnName("madt");
            entity.Property(e => e.Madv)
                .HasMaxLength(20)
                .HasColumnName("madv");
            entity.Property(e => e.MadvDichvu)
                .HasMaxLength(20)
                .HasColumnName("madv_dichvu");
            entity.Property(e => e.Maicd)
                .HasMaxLength(20)
                .HasColumnName("maicd");
            entity.Property(e => e.Maicdp)
                .HasMaxLength(255)
                .HasColumnName("maicdp");
            entity.Property(e => e.Makb)
                .HasMaxLength(20)
                .HasColumnName("makb");
            entity.Property(e => e.Makl)
                .HasMaxLength(20)
                .HasColumnName("makl");
            entity.Property(e => e.Manoigt)
                .HasMaxLength(20)
                .HasColumnName("manoigt");
            entity.Property(e => e.Manv)
                .HasMaxLength(20)
                .HasColumnName("manv");
            entity.Property(e => e.Maphong)
                .HasMaxLength(20)
                .HasColumnName("maphong");
            entity.Property(e => e.Mathe)
                .HasMaxLength(20)
                .HasColumnName("mathe");
            entity.Property(e => e.Mayhct)
                .HasMaxLength(20)
                .HasColumnName("mayhct");
            entity.Property(e => e.Miengiam)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("miengiam");
            entity.Property(e => e.Mienphi)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("mienphi");
            entity.Property(e => e.NamQt)
                .HasMaxLength(4)
                .HasColumnName("nam_qt");
            entity.Property(e => e.Namkt)
                .HasMaxLength(4)
                .HasColumnName("namkt");
            entity.Property(e => e.Ngaycd)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaycd");
            entity.Property(e => e.Ngaygiolaymau)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaygiolaymau");
            entity.Property(e => e.Ngaykcb)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaykcb");
            entity.Property(e => e.Ngaykq)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("ngaykq");
            entity.Property(e => e.Ngaytraketqua)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaytraketqua");
            entity.Property(e => e.Ngayxoa)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngayxoa");
            entity.Property(e => e.NguoiThucHien)
                .HasMaxLength(255)
                .HasColumnName("nguoi_thuc_hien");
            entity.Property(e => e.Noitru)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("noitru");
            entity.Property(e => e.PayPayid)
                .HasMaxLength(200)
                .HasColumnName("pay_payid");
            entity.Property(e => e.PayType)
                .HasMaxLength(10)
                .HasColumnName("pay_type");
            entity.Property(e => e.Ppcham)
                .HasColumnType("character varying")
                .HasColumnName("ppcham");
            entity.Property(e => e.Ptmiengiam)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("ptmiengiam");
            entity.Property(e => e.Pttraituyen)
                .HasPrecision(3)
                .HasDefaultValueSql("0")
                .HasColumnName("pttraituyen");
            entity.Property(e => e.Sdnguonkhac)
                .HasPrecision(1)
                .HasColumnName("sdnguonkhac");
            entity.Property(e => e.Soct)
                .HasMaxLength(20)
                .HasColumnName("soct");
            entity.Property(e => e.Soctvp)
                .HasMaxLength(20)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("soctvp");
            entity.Property(e => e.Soctvpbltong)
                .HasMaxLength(20)
                .HasColumnName("soctvpbltong");
            entity.Property(e => e.Soctvpcl)
                .HasMaxLength(200)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("soctvpcl");
            entity.Property(e => e.Soctvphd)
                .HasMaxLength(20)
                .HasColumnName("soctvphd");
            entity.Property(e => e.Sogiuong)
                .HasMaxLength(20)
                .HasColumnName("sogiuong");
            entity.Property(e => e.Solaninchidinh)
                .HasPrecision(1)
                .HasColumnName("solaninchidinh");
            entity.Property(e => e.Soluong)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("soluong");
            entity.Property(e => e.Sophong)
                .HasMaxLength(255)
                .HasColumnName("sophong");
            entity.Property(e => e.Soth)
                .HasMaxLength(20)
                .HasColumnName("soth");
            entity.Property(e => e.Stt)
                .HasPrecision(3)
                .HasDefaultValueSql("0")
                .HasColumnName("stt");
            entity.Property(e => e.SttLed)
                .HasPrecision(4)
                .HasColumnName("stt_led");
            entity.Property(e => e.Taikhoan)
                .HasMaxLength(20)
                .HasColumnName("taikhoan");
            entity.Property(e => e.TaikhoanTraketqua)
                .HasMaxLength(255)
                .HasColumnName("taikhoan_traketqua");
            entity.Property(e => e.Tamin)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("tamin");
            entity.Property(e => e.Tenclsphu)
                .HasMaxLength(500)
                .HasColumnName("tenclsphu");
            entity.Property(e => e.Tenmay)
                .HasMaxLength(20)
                .HasColumnName("tenmay");
            entity.Property(e => e.Tenyhct)
                .HasMaxLength(255)
                .HasColumnName("tenyhct");
            entity.Property(e => e.ThangQt)
                .HasMaxLength(2)
                .HasColumnName("thang_qt");
            entity.Property(e => e.Thangkt)
                .HasMaxLength(2)
                .HasColumnName("thangkt");
            entity.Property(e => e.Thanhtien)
                .HasPrecision(14, 1)
                .HasDefaultValueSql("0")
                .HasColumnName("thanhtien");
            entity.Property(e => e.Thanhtienmg)
                .HasPrecision(14, 1)
                .HasDefaultValueSql("0")
                .HasColumnName("thanhtienmg");
            entity.Property(e => e.Thanhtienusd)
                .HasPrecision(14)
                .HasColumnName("thanhtienusd");
            entity.Property(e => e.Thatthuchenhlech)
                .HasPrecision(20, 2)
                .HasColumnName("thatthuchenhlech");
            entity.Property(e => e.Thoigiannhanphieu)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("thoigiannhanphieu");
            entity.Property(e => e.Thuphi)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("thuphi");
            entity.Property(e => e.Tile)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("tile");
            entity.Property(e => e.Tinhtrang)
                .HasPrecision(1)
                .HasDefaultValueSql("1")
                .HasColumnName("tinhtrang");
            entity.Property(e => e.Tinhtranglaymau)
                .HasColumnType("character varying")
                .HasColumnName("tinhtranglaymau");
            entity.Property(e => e.Tinhtrangmau)
                .HasColumnType("character varying")
                .HasColumnName("tinhtrangmau");
            entity.Property(e => e.Toacon)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("toacon");
            entity.Property(e => e.Travedieutri)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("travedieutri");
            entity.Property(e => e.Tygia)
                .HasPrecision(14, 2)
                .HasColumnName("tygia");
            entity.Property(e => e.Xoa)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("xoa");

            entity.HasOne(d => d.MabnNavigation).WithMany()
                .HasForeignKey(d => d.Mabn)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("chidinhcls_fk2");
        });

        modelBuilder.Entity<Ctthuocb>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ctthuocbs", "current");

            entity.Property(e => e.Chidinh)
                .HasMaxLength(255)
                .HasColumnName("chidinh");
            entity.Property(e => e.Idtoabs).HasColumnName("idtoabs");
            entity.Property(e => e.LieuDung)
                .HasMaxLength(1024)
                .HasColumnName("lieu_dung");
            entity.Property(e => e.Mahh)
                .HasMaxLength(20)
                .HasColumnName("mahh");
            entity.Property(e => e.Soluong)
                .HasPrecision(5, 2)
                .HasColumnName("soluong");

            entity.HasOne(d => d.MahhNavigation).WithMany()
                .HasForeignKey(d => d.Mahh)
                .HasConstraintName("ctthuocbs_fk");
        });

        modelBuilder.Entity<Dmbenhnhan>(entity =>
        {
            entity.HasKey(e => e.Mabn).HasName("dmbenhnhan_pkey");

            entity.ToTable("dmbenhnhan", "current");

            entity.Property(e => e.Mabn)
                .HasMaxLength(20)
                .HasColumnName("mabn");
            entity.Property(e => e.Bhxh)
                .HasMaxLength(50)
                .HasColumnName("bhxh");
            entity.Property(e => e.BophanKsk)
                .HasMaxLength(500)
                .HasColumnName("bophan_ksk");
            entity.Property(e => e.ChucdanhKsk)
                .HasMaxLength(500)
                .HasColumnName("chucdanh_ksk");
            entity.Property(e => e.Cmnd)
                .HasMaxLength(255)
                .HasColumnName("cmnd");
            entity.Property(e => e.Dadn)
                .HasPrecision(1)
                .HasColumnName("dadn");
            entity.Property(e => e.Diachi)
                .HasMaxLength(255)
                .HasColumnName("diachi");
            entity.Property(e => e.Diachi2)
                .HasMaxLength(255)
                .HasColumnName("diachi2");
            entity.Property(e => e.DiachiCuTmd)
                .HasMaxLength(255)
                .HasColumnName("diachi_cu_tmd");
            entity.Property(e => e.Diachinoict)
                .HasMaxLength(255)
                .HasColumnName("diachinoict");
            entity.Property(e => e.Dienthoai)
                .HasMaxLength(255)
                .HasColumnName("dienthoai");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.GhichuKsk)
                .HasMaxLength(500)
                .HasColumnName("ghichu_ksk");
            entity.Property(e => e.Giayks)
                .HasMaxLength(255)
                .HasColumnName("giayks");
            entity.Property(e => e.Gioitinh).HasColumnName("gioitinh");
            entity.Property(e => e.Hinh).HasColumnName("hinh");
            entity.Property(e => e.Holot)
                .HasMaxLength(255)
                .HasColumnName("holot");
            entity.Property(e => e.Iddinhdanh)
                .HasMaxLength(20)
                .HasColumnName("iddinhdanh");
            entity.Property(e => e.Khoa)
                .HasPrecision(1)
                .HasColumnName("khoa");
            entity.Property(e => e.Macv)
                .HasMaxLength(20)
                .HasDefaultValueSql("'01'::character varying")
                .HasColumnName("macv");
            entity.Property(e => e.Madt)
                .HasMaxLength(20)
                .HasColumnName("madt");
            entity.Property(e => e.Madtuong)
                .HasMaxLength(20)
                .HasColumnName("madtuong");
            entity.Property(e => e.Mahuyen546)
                .HasMaxLength(20)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("mahuyen546");
            entity.Property(e => e.Maloaicb)
                .HasMaxLength(10)
                .HasColumnName("maloaicb");
            entity.Property(e => e.Maloaigiayto)
                .HasMaxLength(20)
                .HasColumnName("maloaigiayto");
            entity.Property(e => e.Manghe)
                .HasMaxLength(20)
                .HasDefaultValueSql("'99'::character varying")
                .HasColumnName("manghe");
            entity.Property(e => e.Maqg)
                .HasMaxLength(20)
                .HasDefaultValueSql("'VN'::character varying")
                .HasColumnName("maqg");
            entity.Property(e => e.Matg)
                .HasMaxLength(20)
                .HasDefaultValueSql("'01'::character varying")
                .HasColumnName("matg");
            entity.Property(e => e.Matinh546)
                .HasMaxLength(20)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("matinh546");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(500)
                .HasColumnName("matkhau");
            entity.Property(e => e.Maxa)
                .HasMaxLength(20)
                .HasColumnName("maxa");
            entity.Property(e => e.Maxa546)
                .HasMaxLength(20)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("maxa546");
            entity.Property(e => e.MaxaCuTmd)
                .HasMaxLength(20)
                .HasColumnName("maxa_cu_tmd");
            entity.Property(e => e.Mstnoict)
                .HasMaxLength(20)
                .HasColumnName("mstnoict");
            entity.Property(e => e.Ngaycap).HasColumnName("ngaycap");
            entity.Property(e => e.Ngaydn)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("ngaydn");
            entity.Property(e => e.Ngaykhamgannhat).HasColumnName("ngaykhamgannhat");
            entity.Property(e => e.Ngaysinh).HasColumnName("ngaysinh");
            entity.Property(e => e.Ngoaikieu)
                .HasMaxLength(200)
                .HasColumnName("ngoaikieu");
            entity.Property(e => e.NhomMau)
                .HasMaxLength(5)
                .HasColumnName("nhom_mau");
            entity.Property(e => e.Noicap)
                .HasMaxLength(256)
                .HasColumnName("noicap");
            entity.Property(e => e.Noict)
                .HasMaxLength(255)
                .HasColumnName("noict");
            entity.Property(e => e.Opc)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("opc");
            entity.Property(e => e.PayPan)
                .HasMaxLength(20)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("pay_pan");
            entity.Property(e => e.Ten)
                .HasMaxLength(255)
                .HasColumnName("ten");
            entity.Property(e => e.Thamgiabh).HasColumnName("thamgiabh");
            entity.Property(e => e.Thanhthi)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("thanhthi");
            entity.Property(e => e.Ttbenhnhan)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("ttbenhnhan");
            entity.Property(e => e.Vienchuc)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("vienchuc");
            entity.Property(e => e.Xoa)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("xoa");
            entity.Property(e => e.Xuatnt)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("xuatnt");
        });

        modelBuilder.Entity<Dmbo>(entity =>
        {
            entity.HasKey(e => e.Mabo).HasName("dmbo_pkey");

            entity.ToTable("dmbo", "current");


        });

        modelBuilder.Entity<Dmcl>(entity =>
        {
            entity.HasKey(e => e.Macls).HasName("dmcls_pkey");

                entity.ToTable("dmcls", "current");

                entity.HasIndex(e => new { e.Macls, e.Maloai, e.Kho, e.Stt, e.Sttxn }, "dmcls_idx");

            entity.HasIndex(e => new { e.Macls, e.Macha, e.Bhyt, e.Stt, e.Maloai, e.Kho, e.Laybo, e.Gioitinh }, "dmcls_idx1");

        });

        modelBuilder.Entity<Dmdoituong>(entity =>
        {
            entity.HasKey(e => e.Madt).HasName("dmdoituong_pkey");

            entity.ToTable("dmdoituong", "current");

            entity.HasIndex(e => new { e.Madt, e.Bhyt, e.Macls }, "dmdoituong_idx");

            entity.Property(e => e.Madt)
                .HasMaxLength(10)
                .HasColumnName("madt");
            entity.Property(e => e.Bhyt)
                .HasPrecision(1)
                .HasColumnName("bhyt");
            entity.Property(e => e.CccdRequired)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("cccd_required");
            entity.Property(e => e.Cothe)
                .HasPrecision(1)
                .HasColumnName("cothe");
            entity.Property(e => e.IdSytHcm)
                .HasPrecision(10)
                .HasDefaultValueSql("0")
                .HasColumnName("id_syt_hcm");
            entity.Property(e => e.Ktdongchitra01)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("ktdongchitra01");
            entity.Property(e => e.Kttck)
                .HasPrecision(1)
                .HasColumnName("kttck");
            entity.Property(e => e.MaMedisoft)
                .HasMaxLength(10)
                .HasColumnName("ma_medisoft");
            entity.Property(e => e.Macls)
                .HasMaxLength(20)
                .HasColumnName("macls");
            entity.Property(e => e.Sdnguonkhac)
                .HasPrecision(1)
                .HasColumnName("sdnguonkhac");
            entity.Property(e => e.Tendt)
                .HasMaxLength(255)
                .HasColumnName("tendt");
            entity.Property(e => e.Tienck)
                .HasPrecision(6)
                .HasColumnName("tienck");
            entity.Property(e => e.Tienckdv)
                .HasPrecision(6)
                .HasColumnName("tienckdv");
            entity.Property(e => e.Tienskb)
                .HasPrecision(6)
                .HasColumnName("tienskb");
            entity.Property(e => e.Xoa)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("xoa");
        });

        modelBuilder.Entity<Dmicd>(entity =>
        {
            entity.HasKey(e => e.Maicd).HasName("dmicd_pkey");

            entity.ToTable("dmicd", "current");

            entity.HasIndex(e => new { e.Manhom, e.Mapl, e.Maicd }, "dmicd_idx");

            entity.HasIndex(e => e.Manhom, "dmicd_idx1");

            entity.HasIndex(e => e.Xoa, "dmicd_idx_xoa");

            entity.Property(e => e.Maicd)
                .HasMaxLength(20)
                .HasColumnName("maicd");
            entity.Property(e => e.Capchuyenmon)
                .HasPrecision(1)
                .HasColumnName("capchuyenmon");
            entity.Property(e => e.CdcLoaitru)
                .HasPrecision(1)
                .HasColumnName("cdc_loaitru");
            entity.Property(e => e.Giatri1nam)
                .HasPrecision(1)
                .HasColumnName("giatri1nam");
            entity.Property(e => e.IdTmd)
                .HasMaxLength(5)
                .HasColumnName("id_tmd");
            entity.Property(e => e.Ma15)
                .HasPrecision(10)
                .HasDefaultValueSql("320")
                .HasColumnName("ma15");
            entity.Property(e => e.Machuong3970)
                .HasMaxLength(500)
                .HasColumnName("machuong3970");
            entity.Property(e => e.Maloai3970)
                .HasMaxLength(500)
                .HasColumnName("maloai3970");
            entity.Property(e => e.Manhom)
                .HasMaxLength(20)
                .HasColumnName("manhom");
            entity.Property(e => e.Manhom3970)
                .HasMaxLength(500)
                .HasColumnName("manhom3970");
            entity.Property(e => e.ManhomBcByt3970)
                .HasMaxLength(500)
                .HasColumnName("manhom_bc_byt3970");
            entity.Property(e => e.Mapl)
                .HasMaxLength(20)
                .HasColumnName("mapl");
            entity.Property(e => e.NgaycapnhatTmd)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaycapnhat_tmd");
            entity.Property(e => e.NgaytaoTmd)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaytao_tmd");
            entity.Property(e => e.NgayuongMax)
                .HasPrecision(2)
                .HasColumnName("ngayuong_max");
            entity.Property(e => e.NgayuongMin)
                .HasPrecision(2)
                .HasColumnName("ngayuong_min");
            entity.Property(e => e.Sttchuong3970)
                .HasMaxLength(500)
                .HasColumnName("sttchuong3970");
            entity.Property(e => e.Tenanh)
                .HasMaxLength(255)
                .HasColumnName("tenanh");
            entity.Property(e => e.Tenchuong3970)
                .HasMaxLength(500)
                .HasColumnName("tenchuong3970");
            entity.Property(e => e.Tenloai3970)
                .HasMaxLength(500)
                .HasColumnName("tenloai3970");
            entity.Property(e => e.Tennhom3970)
                .HasMaxLength(500)
                .HasColumnName("tennhom3970");
            entity.Property(e => e.Tenrieng)
                .HasMaxLength(255)
                .HasColumnName("tenrieng");
            entity.Property(e => e.Tenviet)
                .HasMaxLength(255)
                .HasColumnName("tenviet");
            entity.Property(e => e.Tt26)
                .HasPrecision(1)
                .HasColumnName("tt26");
            entity.Property(e => e.Xoa)
                .HasPrecision(1)
                .HasColumnName("xoa");
        });

        modelBuilder.Entity<Dmthuoc>(entity =>
        {
            entity.HasKey(e => e.Mahh).HasName("dmthuoc_pkey");

            entity.ToTable("dmthuoc", "current");

            entity.HasIndex(e => new { e.Khoql, e.Mahh }, "dmthuoc_idx");

            entity.HasIndex(e => new { e.Manhom, e.Mahh, e.Khoql, e.Kho }, "dmthuoc_idx1");

            entity.Property(e => e.Mahh)
                .HasMaxLength(20)
                .HasColumnName("mahh");
            entity.Property(e => e.Ao)
                .HasPrecision(1)
                .HasColumnName("ao");
            entity.Property(e => e.CongBo)
                .HasColumnType("character varying")
                .HasColumnName("cong_bo");
            entity.Property(e => e.Cophim)
                .HasMaxLength(20)
                .HasColumnName("cophim");
            entity.Property(e => e.Covid19)
                .HasPrecision(1)
                .HasColumnName("covid19");
            entity.Property(e => e.Dacbiet)
                .HasPrecision(2)
                .HasDefaultValueSql("0")
                .HasColumnName("dacbiet");
            entity.Property(e => e.Dachat)
                .HasPrecision(3)
                .HasDefaultValueSql("0")
                .HasColumnName("dachat");
            entity.Property(e => e.Dangbc)
                .HasMaxLength(2000)
                .HasColumnName("dangbc");
            entity.Property(e => e.DinhMuc)
                .HasPrecision(20)
                .HasColumnName("dinh_muc");
            entity.Property(e => e.Dvt)
                .HasMaxLength(20)
                .HasColumnName("dvt");
            entity.Property(e => e.Ghichu)
                .HasColumnType("character varying")
                .HasColumnName("ghichu");
            entity.Property(e => e.Gioitinh)
                .HasPrecision(1)
                .HasColumnName("gioitinh");
            entity.Property(e => e.Hamluong).HasColumnName("hamluong");
            entity.Property(e => e.HanghoaCnk)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("hanghoa_cnk");
            entity.Property(e => e.Kho)
                .HasMaxLength(20)
                .HasColumnName("kho");
            entity.Property(e => e.Khonglamtron)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("khonglamtron");
            entity.Property(e => e.Khoql)
                .HasMaxLength(20)
                .HasColumnName("khoql");
            entity.Property(e => e.Ktcao)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("ktcao");
            entity.Property(e => e.LoaiThau)
                .HasPrecision(20)
                .HasColumnName("loai_thau");
            entity.Property(e => e.LoaiThuoc)
                .HasPrecision(20)
                .HasColumnName("loai_thuoc");
            entity.Property(e => e.Loainguonbhyt)
                .HasPrecision(1)
                .HasColumnName("loainguonbhyt");
            entity.Property(e => e.Loainguonkhac)
                .HasPrecision(1)
                .HasColumnName("loainguonkhac");
            entity.Property(e => e.MaCskcbThuoc)
                .HasMaxLength(10)
                .HasColumnName("ma_cskcb_thuoc");
            entity.Property(e => e.MaHieu)
                .HasColumnType("character varying")
                .HasColumnName("ma_hieu");
            entity.Property(e => e.MaHoatChat)
                .HasColumnType("character varying")
                .HasColumnName("ma_hoat_chat");
            entity.Property(e => e.MaNhomVtyt917)
                .HasColumnType("character varying")
                .HasColumnName("ma_nhom_vtyt_917");
            entity.Property(e => e.MaPpChebien)
                .HasMaxLength(255)
                .HasColumnName("ma_pp_chebien");
            entity.Property(e => e.Madd)
                .HasMaxLength(50)
                .HasColumnName("madd");
            entity.Property(e => e.MahhByt)
                .HasMaxLength(255)
                .HasColumnName("mahh_byt");
            entity.Property(e => e.Mahhthau)
                .HasMaxLength(255)
                .HasColumnName("mahhthau");
            entity.Property(e => e.Mahhtt)
                .HasMaxLength(20)
                .HasColumnName("mahhtt");
            entity.Property(e => e.Maloai)
                .HasMaxLength(20)
                .HasColumnName("maloai");
            entity.Property(e => e.Maloaitoa)
                .HasMaxLength(20)
                .HasColumnName("maloaitoa");
            entity.Property(e => e.Manhom)
                .HasMaxLength(20)
                .HasColumnName("manhom");
            entity.Property(e => e.Manpp)
                .HasMaxLength(20)
                .HasColumnName("manpp");
            entity.Property(e => e.MathuocDuocquocgia)
                .HasMaxLength(20)
                .HasColumnName("mathuoc_duocquocgia");
            entity.Property(e => e.NamThau)
                .HasMaxLength(4)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("nam_thau");
            entity.Property(e => e.Ngayxoa)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngayxoa");
            entity.Property(e => e.NguonCovid19Nsnn)
                .HasPrecision(1)
                .HasColumnName("nguon_covid19_nsnn");
            entity.Property(e => e.NguonCovid19Ttvt)
                .HasPrecision(1)
                .HasColumnName("nguon_covid19_ttvt");
            entity.Property(e => e.Nguonkhac)
                .HasPrecision(1)
                .HasColumnName("nguonkhac");
            entity.Property(e => e.Nhasx)
                .HasMaxLength(255)
                .HasColumnName("nhasx");
            entity.Property(e => e.NhomThau)
                .HasColumnType("character varying")
                .HasColumnName("nhom_thau");
            entity.Property(e => e.Noingoai)
                .HasMaxLength(1)
                .HasColumnName("noingoai");
            entity.Property(e => e.Nuocsx)
                .HasMaxLength(255)
                .HasColumnName("nuocsx");
            entity.Property(e => e.Phache)
                .HasPrecision(1)
                .HasColumnName("phache");
            entity.Property(e => e.Qtrieng)
                .HasPrecision(1)
                .HasColumnName("qtrieng");
            entity.Property(e => e.Quicachdg)
                .HasMaxLength(255)
                .HasColumnName("quicachdg");
            entity.Property(e => e.QuyetDinh)
                .HasColumnType("character varying")
                .HasColumnName("quyet_dinh");
            entity.Property(e => e.Sodk)
                .HasColumnType("character varying")
                .HasColumnName("sodk");
            entity.Property(e => e.SoluongMax)
                .HasPrecision(10, 2)
                .HasColumnName("soluong_max");
            entity.Property(e => e.Songaytoithieu)
                .HasPrecision(3)
                .HasColumnName("songaytoithieu");
            entity.Property(e => e.Stent)
                .HasPrecision(3)
                .HasColumnName("stent");
            entity.Property(e => e.SttByt)
                .HasMaxLength(255)
                .HasColumnName("stt_byt");
            entity.Property(e => e.SttThau)
                .HasMaxLength(500)
                .HasColumnName("stt_thau");
            entity.Property(e => e.TTrantt)
                .HasPrecision(20, 6)
                .HasColumnName("t_trantt");
            entity.Property(e => e.Taikhoan)
                .HasMaxLength(20)
                .HasColumnName("taikhoan");
            entity.Property(e => e.TenNhomVtyt917)
                .HasColumnType("character varying")
                .HasColumnName("ten_nhom_vtyt_917");
            entity.Property(e => e.Tenhc)
                .HasMaxLength(500)
                .HasColumnName("tenhc");
            entity.Property(e => e.Tenhcp)
                .HasMaxLength(255)
                .HasColumnName("tenhcp");
            entity.Property(e => e.Tenhh)
                .HasMaxLength(255)
                .HasColumnName("tenhh");
            entity.Property(e => e.TenhhByt)
                .HasMaxLength(255)
                .HasColumnName("tenhh_byt");
            entity.Property(e => e.Tenmay)
                .HasMaxLength(20)
                .HasColumnName("tenmay");
            entity.Property(e => e.Thaythe)
                .HasPrecision(3)
                .HasDefaultValueSql("0")
                .HasColumnName("thaythe");
            entity.Property(e => e.Theodon)
                .HasMaxLength(1)
                .HasDefaultValueSql("'0'::character varying")
                .HasColumnName("theodon");
            entity.Property(e => e.Thuocd)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("thuocd");
            entity.Property(e => e.Thuock)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("thuock");
            entity.Property(e => e.Thuocsd)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("thuocsd");
            entity.Property(e => e.Tienich)
                .HasPrecision(1)
                .HasColumnName("tienich");
            entity.Property(e => e.TtThau)
                .HasColumnType("character varying")
                .HasColumnName("tt_thau");
            entity.Property(e => e.TtThau4750)
                .HasColumnType("character varying")
                .HasColumnName("tt_thau4750");
            entity.Property(e => e.Tyle)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("tyle");
            entity.Property(e => e.TyleTt)
                .HasPrecision(10)
                .HasDefaultValueSql("0")
                .HasColumnName("tyle_tt");
            entity.Property(e => e.Vatnhap)
                .HasPrecision(2)
                .HasColumnName("vatnhap");
            entity.Property(e => e.Viemganc)
                .HasPrecision(1)
                .HasColumnName("viemganc");
            entity.Property(e => e.Xoa)
                .HasPrecision(1)
                .HasColumnName("xoa");
        });

        modelBuilder.Entity<Khambenh>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("khambenh", "current");

            entity.Property(e => e.Bant)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("bant");
            entity.Property(e => e.Cannang)
                .HasPrecision(6, 2)
                .HasColumnName("cannang");
            entity.Property(e => e.ChandoanDautien)
                .HasMaxLength(2000)
                .HasColumnName("chandoan_dautien");
            entity.Property(e => e.Chieucao)
                .HasPrecision(6, 2)
                .HasColumnName("chieucao");
            entity.Property(e => e.Cls)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("cls");
            entity.Property(e => e.Dain)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("dain");
            entity.Property(e => e.Daingiay)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("daingiay");
            entity.Property(e => e.Dakham)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("dakham");
            entity.Property(e => e.Fio2)
                .HasMaxLength(50)
                .HasColumnName("fio2");
            entity.Property(e => e.Giokb)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("giokb");
            entity.Property(e => e.Hb)
                .HasMaxLength(50)
                .HasColumnName("hb");
            entity.Property(e => e.Huyetap)
                .HasMaxLength(20)
                .HasColumnName("huyetap");
            entity.Property(e => e.Khoin)
                .HasMaxLength(10)
                .HasColumnName("khoin");
            entity.Property(e => e.Kqcdoan)
                .HasMaxLength(2000)
                .HasColumnName("kqcdoan");
            entity.Property(e => e.Kqcdoanp)
                .HasColumnType("character varying")
                .HasColumnName("kqcdoanp");
            entity.Property(e => e.Lydo)
                .HasMaxLength(1000)
                .HasColumnName("lydo");
            entity.Property(e => e.Maba)
                .HasMaxLength(20)
                .HasColumnName("maba");
            entity.Property(e => e.Mabn)
                .HasMaxLength(20)
                .HasColumnName("mabn");
            entity.Property(e => e.Mabv)
                .HasMaxLength(20)
                .HasColumnName("mabv");
            entity.Property(e => e.Mach)
                .HasPrecision(4)
                .HasColumnName("mach");
            entity.Property(e => e.Madv)
                .HasMaxLength(20)
                .HasColumnName("madv");
            entity.Property(e => e.Maicd)
                .HasMaxLength(20)
                .HasColumnName("maicd");
            entity.Property(e => e.Maicdp)
                .HasMaxLength(500)
                .HasColumnName("maicdp");
            entity.Property(e => e.Makb)
                .HasMaxLength(20)
                .HasColumnName("makb");
            entity.Property(e => e.Manv)
                .HasMaxLength(20)
                .HasColumnName("manv");
            entity.Property(e => e.Maphong)
                .HasMaxLength(20)
                .HasColumnName("maphong");
            entity.Property(e => e.Mapl)
                .HasMaxLength(20)
                .HasColumnName("mapl");
            entity.Property(e => e.Maxt)
                .HasMaxLength(20)
                .HasColumnName("maxt");
            entity.Property(e => e.Mayhct)
                .HasMaxLength(20)
                .HasColumnName("mayhct");
            entity.Property(e => e.NamQt)
                .HasMaxLength(4)
                .HasColumnName("nam_qt");
            entity.Property(e => e.Namkt)
                .HasMaxLength(4)
                .HasColumnName("namkt");
            entity.Property(e => e.Ngaybenh)
                .HasPrecision(3)
                .HasDefaultValueSql("1")
                .HasColumnName("ngaybenh");
            entity.Property(e => e.Ngaykcb)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaykcb");
            entity.Property(e => e.Ngayxoa)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngayxoa");
            entity.Property(e => e.Nhietdo)
                .HasPrecision(4, 2)
                .HasColumnName("nhietdo");
            entity.Property(e => e.Nhiptho)
                .HasPrecision(4)
                .HasColumnName("nhiptho");
            entity.Property(e => e.Sohd)
                .HasMaxLength(20)
                .HasColumnName("sohd");
            entity.Property(e => e.Songaydt)
                .HasPrecision(4)
                .HasColumnName("songaydt");
            entity.Property(e => e.Sott)
                .HasPrecision(10)
                .HasColumnName("sott");
            entity.Property(e => e.Taikhoan)
                .HasMaxLength(20)
                .HasColumnName("taikhoan");
            entity.Property(e => e.Tenmay)
                .HasMaxLength(20)
                .HasColumnName("tenmay");
            entity.Property(e => e.Tenyhct)
                .HasMaxLength(255)
                .HasColumnName("tenyhct");
            entity.Property(e => e.ThangQt)
                .HasMaxLength(2)
                .HasColumnName("thang_qt");
            entity.Property(e => e.Thangkt)
                .HasMaxLength(2)
                .HasColumnName("thangkt");
            entity.Property(e => e.Tinhtrang)
                .HasPrecision(1)
                .HasDefaultValueSql("1")
                .HasColumnName("tinhtrang");
            entity.Property(e => e.Tuvankhac)
                .HasColumnType("character varying")
                .HasColumnName("tuvankhac");
            entity.Property(e => e.Vongdau)
                .HasPrecision(6, 2)
                .HasColumnName("vongdau");
            entity.Property(e => e.Vongnguc)
                .HasPrecision(6, 2)
                .HasColumnName("vongnguc");
            entity.Property(e => e.Xoa)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("xoa");
            entity.Property(e => e.Xutritenga)
                .HasMaxLength(20)
                .HasColumnName("xutritenga");
        });

        modelBuilder.Entity<Psdangky>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("psdangky", "current");

            entity.HasIndex(e => new { e.Thangkt, e.Namkt, e.Xoa, e.Mabn, e.Makb }, "psdangky_idx");

            entity.HasIndex(e => new { e.Mabn, e.Makb, e.Ngaydk }, "psdangky_idx1");

            entity.HasIndex(e => new { e.Mabn, e.Makb, e.Maba, e.Mathe, e.Madt, e.Madv, e.Maphong, e.Xoa, e.Thangkt, e.Namkt }, "psdangky_idx2").HasFilter("(xoa = (0)::numeric)");

            entity.HasIndex(e => new { e.Xoa, e.Ravien, e.Noitru, e.Tiepnhan }, "psdangky_idx3").HasFilter("((xoa = (0)::numeric) AND (ravien = (0)::numeric) AND (noitru = (1)::numeric))");

            entity.HasIndex(e => new { e.Xoa, e.Ravien, e.Noitru, e.Tiepnhan }, "psdangky_idx4").HasFilter("((xoa = (0)::numeric) AND (ravien = (1)::numeric) AND (noitru = (1)::numeric))");

            entity.HasIndex(e => new { e.Tiepnhan, e.Xoa, e.Mabn, e.Makb, e.Maba }, "psdangky_idx_fees");

            entity.HasIndex(e => new { e.Xoa, e.Maba, e.Thangkt, e.Namkt, e.Madt }, "psdangky_pr01");

            entity.HasIndex(e => new { e.ThangQt, e.NamQt }, "psdangky_thangqt_xml");

            entity.Property(e => e.AmdaoKsk)
                .HasColumnType("character varying")
                .HasColumnName("amdao_ksk");
            entity.Property(e => e.Bacluong)
                .HasMaxLength(20)
                .HasColumnName("bacluong");
            entity.Property(e => e.Bant)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("bant");
            entity.Property(e => e.BenhnhanLao)
                .HasPrecision(1)
                .HasColumnName("benhnhan_lao");
            entity.Property(e => e.Bntaikham)
                .HasPrecision(1)
                .HasColumnName("bntaikham");
            entity.Property(e => e.Cannang)
                .HasPrecision(6, 2)
                .HasColumnName("cannang");
            entity.Property(e => e.Cbsv)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("cbsv");
            entity.Property(e => e.Cdoantd)
                .HasMaxLength(2000)
                .HasColumnName("cdoantd");
            entity.Property(e => e.ChandoanDautien)
                .HasMaxLength(2000)
                .HasColumnName("chandoan_dautien");
            entity.Property(e => e.Chandoankhacbiet)
                .HasPrecision(1)
                .HasColumnName("chandoankhacbiet");
            entity.Property(e => e.Chandoanphuhop)
                .HasPrecision(1)
                .HasColumnName("chandoanphuhop");
            entity.Property(e => e.Chieucao)
                .HasPrecision(6, 2)
                .HasColumnName("chieucao");
            entity.Property(e => e.Chuyenmon).HasColumnName("chuyenmon");
            entity.Property(e => e.Chuyentuyenduoi)
                .HasPrecision(1)
                .HasColumnName("chuyentuyenduoi");
            entity.Property(e => e.Cmndqh)
                .HasMaxLength(25)
                .HasColumnName("cmndqh");
            entity.Property(e => e.Cothe)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("cothe");
            entity.Property(e => e.Cotk)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("cotk");
            entity.Property(e => e.Cv2384)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("cv2384");
            entity.Property(e => e.Daguibhxh)
                .HasPrecision(10)
                .HasDefaultValueSql("0")
                .HasColumnName("daguibhxh");
            entity.Property(e => e.Daguibyt)
                .HasPrecision(10)
                .HasDefaultValueSql("0")
                .HasColumnName("daguibyt");
            entity.Property(e => e.Dain)
                .HasPrecision(5)
                .HasDefaultValueSql("0")
                .HasColumnName("dain");
            entity.Property(e => e.Dathu)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("dathu");
            entity.Property(e => e.DenghiKsk)
                .HasColumnType("character varying")
                .HasColumnName("denghi_ksk");
            entity.Property(e => e.Denngaytd).HasColumnName("denngaytd");
            entity.Property(e => e.Diachiqh)
                .HasMaxLength(255)
                .HasColumnName("diachiqh");
            entity.Property(e => e.Dienthoaiqh)
                .HasMaxLength(255)
                .HasColumnName("dienthoaiqh");
            entity.Property(e => e.Dieutriopc)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("dieutriopc");
            entity.Property(e => e.Dinhsuat)
                .HasPrecision(1)
                .HasColumnName("dinhsuat");
            entity.Property(e => e.DiungKsk)
                .HasColumnType("character varying")
                .HasColumnName("diung_ksk");
            entity.Property(e => e.Dtss)
                .HasMaxLength(10)
                .HasColumnName("dtss");
            entity.Property(e => e.Dvttuoi)
                .HasPrecision(1)
                .HasColumnName("dvttuoi");
            entity.Property(e => e.Ghichu).HasColumnName("ghichu");
            entity.Property(e => e.Giaychungsinh)
                .HasMaxLength(500)
                .HasColumnName("giaychungsinh");
            entity.Property(e => e.Giaycv)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("giaycv");
            entity.Property(e => e.Giayluu).HasColumnName("giayluu");
            entity.Property(e => e.Giayluuchuyentuyen).HasColumnName("giayluuchuyentuyen");
            entity.Property(e => e.Giayxacnhan)
                .HasMaxLength(500)
                .HasColumnName("giayxacnhan");
            entity.Property(e => e.Giayxacnhancutru)
                .HasPrecision(1)
                .HasColumnName("giayxacnhancutru");
            entity.Property(e => e.Giodk)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("giodk");
            entity.Property(e => e.GuiPkh)
                .HasPrecision(1)
                .HasColumnName("gui_pkh");
            entity.Property(e => e.Guihssk)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("guihssk");
            entity.Property(e => e.Hangbv)
                .HasPrecision(1)
                .HasColumnName("hangbv");
            entity.Property(e => e.Hotenqh)
                .HasMaxLength(255)
                .HasColumnName("hotenqh");
            entity.Property(e => e.Huyetap)
                .HasMaxLength(20)
                .HasColumnName("huyetap");
            entity.Property(e => e.Keluu)
                .HasMaxLength(50)
                .HasColumnName("keluu");
            entity.Property(e => e.KetQuaDtri)
                .HasMaxLength(2)
                .HasColumnName("ket_qua_dtri");
            entity.Property(e => e.KetluanKsk)
                .HasColumnType("character varying")
                .HasColumnName("ketluan_ksk");
            entity.Property(e => e.Khamdv)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("khamdv");
            entity.Property(e => e.Khamnghiemtt)
                .HasPrecision(1)
                .HasColumnName("khamnghiemtt");
            entity.Property(e => e.Kqcdoan)
                .HasMaxLength(2000)
                .HasColumnName("kqcdoan");
            entity.Property(e => e.Kqcdoangp)
                .HasMaxLength(500)
                .HasColumnName("kqcdoangp");
            entity.Property(e => e.KqclsKsk)
                .HasColumnType("character varying")
                .HasColumnName("kqcls_ksk");
            entity.Property(e => e.Loaibn)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("loaibn");
            entity.Property(e => e.Loaiqh)
                .HasMaxLength(255)
                .HasColumnName("loaiqh");
            entity.Property(e => e.Luongtt)
                .HasPrecision(20, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("luongtt");
            entity.Property(e => e.Luubenh)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("luubenh");
            entity.Property(e => e.Lydoct)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("lydoct");
            entity.Property(e => e.Maba)
                .HasMaxLength(20)
                .HasColumnName("maba");
            entity.Property(e => e.Mabn)
                .HasMaxLength(20)
                .HasColumnName("mabn");
            entity.Property(e => e.Mabshk)
                .HasMaxLength(20)
                .HasColumnName("mabshk");
            entity.Property(e => e.MabvDieutriLao)
                .HasMaxLength(20)
                .HasColumnName("mabv_dieutri_lao");
            entity.Property(e => e.Mabvdk)
                .HasMaxLength(20)
                .HasColumnName("mabvdk");
            entity.Property(e => e.Mabvkb)
                .HasMaxLength(20)
                .HasColumnName("mabvkb");
            entity.Property(e => e.Macc)
                .HasMaxLength(20)
                .HasColumnName("macc");
            entity.Property(e => e.Mach)
                .HasPrecision(4)
                .HasColumnName("mach");
            entity.Property(e => e.Madt)
                .HasMaxLength(20)
                .HasColumnName("madt");
            entity.Property(e => e.Madv)
                .HasMaxLength(20)
                .HasColumnName("madv");
            entity.Property(e => e.MadvInphieu)
                .HasMaxLength(50)
                .HasColumnName("madv_inphieu");
            entity.Property(e => e.Madvhd)
                .HasMaxLength(20)
                .HasColumnName("madvhd");
            entity.Property(e => e.Mahongheo)
                .HasMaxLength(255)
                .HasColumnName("mahongheo");
            entity.Property(e => e.Maicd)
                .HasMaxLength(15)
                .HasColumnName("maicd");
            entity.Property(e => e.Maicdgp)
                .HasMaxLength(20)
                .HasColumnName("maicdgp");
            entity.Property(e => e.Maicdp)
                .HasMaxLength(255)
                .HasColumnName("maicdp");
            entity.Property(e => e.Maicdtd)
                .HasMaxLength(20)
                .HasColumnName("maicdtd");
            entity.Property(e => e.Maicdtv)
                .HasMaxLength(20)
                .HasColumnName("maicdtv");
            entity.Property(e => e.Makb)
                .HasMaxLength(20)
                .HasColumnName("makb");
            entity.Property(e => e.Manoicap)
                .HasMaxLength(20)
                .HasColumnName("manoicap");
            entity.Property(e => e.Manoigt)
                .HasMaxLength(20)
                .HasColumnName("manoigt");
            entity.Property(e => e.ManvDangKetLuan)
                .HasColumnType("character varying")
                .HasColumnName("manv_dang_ket_luan");
            entity.Property(e => e.ManvKetluanKsk)
                .HasColumnType("character varying")
                .HasColumnName("manv_ketluan_ksk");
            entity.Property(e => e.Manvvv)
                .HasMaxLength(20)
                .HasColumnName("manvvv");
            entity.Property(e => e.Maphong)
                .HasMaxLength(20)
                .HasColumnName("maphong");
            entity.Property(e => e.MaphongInphieu)
                .HasMaxLength(20)
                .HasColumnName("maphong_inphieu");
            entity.Property(e => e.Maphongbd)
                .HasMaxLength(20)
                .HasColumnName("maphongbd");
            entity.Property(e => e.Maphonghd)
                .HasMaxLength(20)
                .HasColumnName("maphonghd");
            entity.Property(e => e.Matgtv)
                .HasMaxLength(20)
                .HasColumnName("matgtv");
            entity.Property(e => e.Mathe)
                .HasMaxLength(255)
                .HasColumnName("mathe");
            entity.Property(e => e.MatheGh)
                .HasMaxLength(255)
                .HasColumnName("mathe_gh");
            entity.Property(e => e.Matttv)
                .HasMaxLength(20)
                .HasColumnName("matttv");
            entity.Property(e => e.Mayhct)
                .HasMaxLength(50)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("mayhct");
            entity.Property(e => e.Mienchitra)
                .HasPrecision(1)
                .HasColumnName("mienchitra");
            entity.Property(e => e.NamQt)
                .HasMaxLength(4)
                .HasColumnName("nam_qt");
            entity.Property(e => e.Namkt)
                .HasMaxLength(4)
                .HasColumnName("namkt");
            entity.Property(e => e.Namsinh)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("namsinh");
            entity.Property(e => e.Namsinhqh).HasColumnName("namsinhqh");
            entity.Property(e => e.Ngay5nam).HasColumnName("ngay5nam");
            entity.Property(e => e.Ngaybd).HasColumnName("ngaybd");
            entity.Property(e => e.NgaybdGh).HasColumnName("ngaybd_gh");
            entity.Property(e => e.Ngaybdhen)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("ngaybdhen");
            entity.Property(e => e.NgaychungnhanLao).HasColumnName("ngaychungnhan_lao");
            entity.Property(e => e.Ngaydk)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaydk");
            entity.Property(e => e.Ngayinphieu)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("ngayinphieu");
            entity.Property(e => e.Ngaykt).HasColumnName("ngaykt");
            entity.Property(e => e.NgayktGh).HasColumnName("ngaykt_gh");
            entity.Property(e => e.Ngaymienct).HasColumnName("ngaymienct");
            entity.Property(e => e.Ngayrv)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("ngayrv");
            entity.Property(e => e.Ngaytaikham)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("ngaytaikham");
            entity.Property(e => e.Ngaytv)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("ngaytv");
            entity.Property(e => e.Ngayxoa)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngayxoa");
            entity.Property(e => e.Nguycotenga)
                .HasPrecision(1)
                .HasColumnName("nguycotenga");
            entity.Property(e => e.Nguyennhantv)
                .HasMaxLength(500)
                .HasColumnName("nguyennhantv");
            entity.Property(e => e.Nhanphieu)
                .HasPrecision(1)
                .HasColumnName("nhanphieu");
            entity.Property(e => e.Nhietdo)
                .HasPrecision(4, 2)
                .HasColumnName("nhietdo");
            entity.Property(e => e.Nhiptho)
                .HasPrecision(4)
                .HasColumnName("nhiptho");
            entity.Property(e => e.Noigt)
                .HasMaxLength(255)
                .HasColumnName("noigt");
            entity.Property(e => e.NoingoaikhoaKsk)
                .HasColumnType("character varying")
                .HasColumnName("noingoaikhoa_ksk");
            entity.Property(e => e.Noitru)
                .HasPrecision(1)
                .HasDefaultValueSql("1")
                .HasColumnName("noitru");
            entity.Property(e => e.Phanhoi)
                .HasPrecision(1)
                .HasColumnName("phanhoi");
            entity.Property(e => e.PhanloaiKsk)
                .HasColumnType("character varying")
                .HasColumnName("phanloai_ksk");
            entity.Property(e => e.Psphaitra)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("psphaitra");
            entity.Property(e => e.Ptngansach)
                .HasPrecision(5, 2)
                .HasColumnName("ptngansach");
            entity.Property(e => e.Ptthu)
                .HasPrecision(5, 2)
                .HasColumnName("ptthu");
            entity.Property(e => e.Quetcccd)
                .HasPrecision(1)
                .HasColumnName("quetcccd");
            entity.Property(e => e.Quyen)
                .HasMaxLength(20)
                .HasColumnName("quyen");
            entity.Property(e => e.Ravien)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("ravien");
            entity.Property(e => e.Sangloctenga)
                .HasMaxLength(20)
                .HasColumnName("sangloctenga");
            entity.Property(e => e.SanphukhoaKsk)
                .HasColumnType("character varying")
                .HasColumnName("sanphukhoa_ksk");
            entity.Property(e => e.Sdnguonkhac)
                .HasPrecision(1)
                .HasColumnName("sdnguonkhac");
            entity.Property(e => e.Sms)
                .HasPrecision(1)
                .HasColumnName("sms");
            entity.Property(e => e.So)
                .HasMaxLength(20)
                .HasColumnName("so");
            entity.Property(e => e.Socttd)
                .HasMaxLength(50)
                .HasColumnName("socttd");
            entity.Property(e => e.SofilepdfXn)
                .HasPrecision(10)
                .HasColumnName("sofilepdf_xn");
            entity.Property(e => e.Sogiuong)
                .HasMaxLength(20)
                .HasColumnName("sogiuong");
            entity.Property(e => e.Soluutru)
                .HasMaxLength(50)
                .HasColumnName("soluutru");
            entity.Property(e => e.Sott)
                .HasPrecision(10)
                .HasColumnName("sott");
            entity.Property(e => e.Taikham)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("taikham");
            entity.Property(e => e.Taikhoan)
                .HasMaxLength(20)
                .HasColumnName("taikhoan");
            entity.Property(e => e.TaikhoanInphieu)
                .HasMaxLength(40)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("taikhoan_inphieu");
            entity.Property(e => e.TaikhoanKetthuc)
                .HasMaxLength(40)
                .HasColumnName("taikhoan_ketthuc");
            entity.Property(e => e.Tenmay)
                .HasMaxLength(20)
                .HasColumnName("tenmay");
            entity.Property(e => e.Tenyhct)
                .HasMaxLength(255)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("tenyhct");
            entity.Property(e => e.ThangQt)
                .HasMaxLength(2)
                .HasColumnName("thang_qt");
            entity.Property(e => e.Thangkt)
                .HasMaxLength(2)
                .HasColumnName("thangkt");
            entity.Property(e => e.Themoi)
                .HasPrecision(1)
                .HasDefaultValueSql("1")
                .HasColumnName("themoi");
            entity.Property(e => e.Thetam)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("thetam");
            entity.Property(e => e.Tienck)
                .HasPrecision(6)
                .HasColumnName("tienck");
            entity.Property(e => e.Tiennguonkhac)
                .HasPrecision(24)
                .HasColumnName("tiennguonkhac");
            entity.Property(e => e.Tienskb)
                .HasPrecision(6)
                .HasColumnName("tienskb");
            entity.Property(e => e.Tienthu)
                .HasPrecision(20, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("tienthu");
            entity.Property(e => e.TienthuKtc)
                .HasPrecision(10)
                .HasColumnName("tienthu_ktc");
            entity.Property(e => e.Tiepnhan)
                .HasMaxLength(20)
                .HasColumnName("tiepnhan");
            entity.Property(e => e.Tinhtrang)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("tinhtrang");
            entity.Property(e => e.Tngt)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("tngt");
            entity.Property(e => e.Tongtienbh)
                .HasPrecision(20, 6)
                .HasColumnName("tongtienbh");
            entity.Property(e => e.Tongtienbv)
                .HasPrecision(20, 6)
                .HasColumnName("tongtienbv");
            entity.Property(e => e.TrangthaiKetluanKsk)
                .HasPrecision(1)
                .HasColumnName("trangthai_ketluan_ksk");
            entity.Property(e => e.Trangthaichuyentuyen)
                .HasPrecision(1)
                .HasColumnName("trangthaichuyentuyen");
            entity.Property(e => e.TsgiadinhKsk)
                .HasColumnType("character varying")
                .HasColumnName("tsgiadinh_ksk");
            entity.Property(e => e.Ttkham)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("ttkham");
            entity.Property(e => e.TucungKsk)
                .HasColumnType("character varying")
                .HasColumnName("tucung_ksk");
            entity.Property(e => e.Tungaytd).HasColumnName("tungaytd");
            entity.Property(e => e.Tuoi)
                .HasPrecision(3)
                .HasColumnName("tuoi");
            entity.Property(e => e.Tuoiqh)
                .HasPrecision(2)
                .HasColumnName("tuoiqh");
            entity.Property(e => e.Tuyen)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("tuyen");
            entity.Property(e => e.Tuyenbv)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("tuyenbv");
            entity.Property(e => e.TuyenvuKsk)
                .HasColumnType("character varying")
                .HasColumnName("tuyenvu_ksk");
            entity.Property(e => e.Tuyenxml)
                .HasPrecision(1)
                .HasColumnName("tuyenxml");
            entity.Property(e => e.Tvtdcapcuu)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("tvtdcapcuu");
            entity.Property(e => e.Tvtruoc24h)
                .HasColumnType("character varying")
                .HasColumnName("tvtruoc24h");
            entity.Property(e => e.Uutien)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("uutien");
            entity.Property(e => e.Vanchuyen)
                .HasPrecision(1)
                .HasColumnName("vanchuyen");
            entity.Property(e => e.Vongdau)
                .HasPrecision(6, 2)
                .HasColumnName("vongdau");
            entity.Property(e => e.Vongnguc)
                .HasPrecision(6, 2)
                .HasColumnName("vongnguc");
            entity.Property(e => e.Xoa)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("xoa");
            entity.Property(e => e.XuatXml917)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("xuat_xml_917");

            entity.HasOne(d => d.MadtNavigation).WithMany()
                .HasForeignKey(d => d.Madt)
                .HasConstraintName("psdangky_fk1");
        });

        modelBuilder.Entity<Pshdxn>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("pshdxn", "current");

            entity.HasIndex(e => e.Sohd, "pshdxn_idx");

            entity.HasIndex(e => e.Loaixn, "pshdxn_idx1");

            entity.HasIndex(e => new { e.Thangkt, e.Namkt, e.Xoa, e.Khole, e.Loaixn }, "pshdxn_idx10").HasFilter("((xoa = (0)::numeric) AND ((khole)::text = '14'::text) AND ((loaixn)::text = 'xkh'::text))");

            entity.HasIndex(e => new { e.Thangkt, e.Namkt, e.Mahh, e.Xoa, e.Khole }, "pshdxn_idx11").HasFilter("((xoa = (0)::numeric) AND ((khole)::text = '14'::text))");

            entity.HasIndex(e => new { e.Xoa, e.Mabn, e.Khole, e.Loaixn, e.Noitru }, "pshdxn_idx12").HasFilter("((xoa = (0)::numeric) AND ((loaixn)::text = 'xbl'::text) AND (noitru = (1)::numeric))");

            entity.HasIndex(e => new { e.Mabn, e.Makh, e.Xoa }, "pshdxn_idx13");

            entity.HasIndex(e => new { e.Sohd, e.Makh, e.Ngayhd, e.Thangkt, e.Namkt, e.Xoa }, "pshdxn_idx2");

            entity.HasIndex(e => new { e.Sohd, e.Makh, e.Loaixn, e.Ngayhd, e.Mahh, e.Giavat, e.Giaban, e.Handung, e.Thangkt, e.Namkt, e.Xoa }, "pshdxn_idx3");

            entity.HasIndex(e => new { e.Thangkt, e.Namkt, e.Mabn, e.Xoa }, "pshdxn_idx4");

            entity.HasIndex(e => new { e.Thangkt, e.Namkt, e.Mahh, e.Xoa, e.Khole }, "pshdxn_idx5");

            entity.HasIndex(e => new { e.Thangkt, e.Namkt, e.Xoa, e.Loaixn }, "pshdxn_idx6").HasFilter("((xoa = (0)::numeric) AND (((loaixn)::text = 'xbb'::text) OR ((loaixn)::text = 'xbl'::text)))");

            entity.HasIndex(e => new { e.Thangkt, e.Namkt, e.Xoa, e.Loaixn }, "pshdxn_idx7").HasFilter("((xoa = (0)::numeric) AND ((loaixn)::text = 'ndt'::text))");

            entity.HasIndex(e => new { e.Thangkt, e.Namkt, e.Xoa, e.Loaixn }, "pshdxn_idx8").HasFilter("((xoa = (0)::numeric) AND ((loaixn)::text = 'xkp'::text))");

            entity.HasIndex(e => new { e.Thangkt, e.Namkt, e.Xoa, e.Loaixn }, "pshdxn_idx9").HasFilter("((xoa = (0)::numeric) AND ((loaixn)::text = 'xth'::text))");

            entity.HasIndex(e => new { e.Mabn, e.Makh, e.Xoa }, "pshdxn_idx_cr");

            entity.HasIndex(e => new { e.Mabn, e.Makh, e.Madt, e.Noitru, e.Ngayhd, e.Kyhieu, e.Xoa }, "pshdxn_idx_fees");

            entity.HasIndex(e => new { e.Mahh, e.Khochan, e.Madt, e.Makh, e.Noitru, e.Loaixn, e.Kyhieu, e.Loaitoa, e.Xoa, e.Mabn }, "pshdxn_idx_fees1");

            entity.HasIndex(e => new { e.Thangkt, e.Namkt, e.Noitru }, "pshdxn_pr01");

            entity.HasIndex(e => new { e.Xoa, e.Noitru, e.Maphong, e.Thangkt, e.Namkt, e.Madt, e.Loaixn, e.Kyhieu, e.Bhyt }, "pshdxn_pr02");

            entity.HasIndex(e => new { e.Xoa, e.Noitru, e.Maphong, e.Thangkt, e.Namkt, e.Madt, e.Loaixn, e.Kyhieu, e.Bhyt, e.Loaitoa }, "pshdxn_pr03");

            entity.Property(e => e.Bant)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("bant");
            entity.Property(e => e.Bhyt)
                .HasPrecision(3)
                .HasDefaultValueSql("1")
                .HasColumnName("bhyt");
            entity.Property(e => e.Cachuong)
                .HasMaxLength(255)
                .HasColumnName("cachuong");
            entity.Property(e => e.Chieu)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("chieu");
            entity.Property(e => e.Ck)
                .HasPrecision(5, 2)
                .HasColumnName("ck");
            entity.Property(e => e.Dacd)
                .HasPrecision(1)
                .HasColumnName("dacd");
            entity.Property(e => e.Dain)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("dain");
            entity.Property(e => e.Dinhsuat)
                .HasPrecision(1)
                .HasColumnName("dinhsuat");
            entity.Property(e => e.Dutru)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("dutru");
            entity.Property(e => e.GcTmd)
                .HasMaxLength(10)
                .HasColumnName("gc_tmd");
            entity.Property(e => e.Giaban)
                .HasPrecision(24, 6)
                .HasColumnName("giaban");
            entity.Property(e => e.Giabhyt)
                .HasPrecision(14, 6)
                .HasDefaultValueSql("0")
                .HasColumnName("giabhyt");
            entity.Property(e => e.Giakc)
                .HasPrecision(14, 6)
                .HasColumnName("giakc");
            entity.Property(e => e.Gianhap)
                .HasPrecision(24, 6)
                .HasColumnName("gianhap");
            entity.Property(e => e.Giavat)
                .HasPrecision(24, 6)
                .HasColumnName("giavat");
            entity.Property(e => e.Giolap)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("giolap");
            entity.Property(e => e.Handung)
                .HasMaxLength(10)
                .HasColumnName("handung");
            entity.Property(e => e.Haohutduoclieu)
                .HasPrecision(5, 1)
                .HasColumnName("haohutduoclieu");
            entity.Property(e => e.Iddienbien)
                .HasMaxLength(40)
                .HasColumnName("iddienbien");
            entity.Property(e => e.Inmaubhyt)
                .HasPrecision(2)
                .HasDefaultValueSql("0")
                .HasColumnName("inmaubhyt");
            entity.Property(e => e.Intoadieutri)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("intoadieutri");
            entity.Property(e => e.Khoaso)
                .HasPrecision(1)
                .HasColumnName("khoaso");
            entity.Property(e => e.Khochan)
                .HasMaxLength(10)
                .HasColumnName("khochan");
            entity.Property(e => e.Khole)
                .HasMaxLength(10)
                .HasColumnName("khole");
            entity.Property(e => e.Kyhieu)
                .HasMaxLength(255)
                .HasColumnName("kyhieu");
            entity.Property(e => e.LieuDung)
                .HasMaxLength(1024)
                .HasColumnName("lieu_dung");
            entity.Property(e => e.Loaitoa)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("loaitoa");
            entity.Property(e => e.Loaitoatt)
                .HasPrecision(1)
                .HasColumnName("loaitoatt");
            entity.Property(e => e.Loaixn)
                .HasMaxLength(10)
                .HasColumnName("loaixn");
            entity.Property(e => e.Maba)
                .HasMaxLength(20)
                .HasColumnName("maba");
            entity.Property(e => e.Mabn)
                .HasMaxLength(20)
                .HasColumnName("mabn");
            entity.Property(e => e.Macon)
                .HasMaxLength(20)
                .HasColumnName("macon");
            entity.Property(e => e.Madt)
                .HasMaxLength(10)
                .HasColumnName("madt");
            entity.Property(e => e.Madv)
                .HasMaxLength(20)
                .HasColumnName("madv");
            entity.Property(e => e.Mahh)
                .HasMaxLength(20)
                .HasColumnName("mahh");
            entity.Property(e => e.Makh)
                .HasMaxLength(20)
                .HasColumnName("makh");
            entity.Property(e => e.Makhc)
                .HasMaxLength(20)
                .HasColumnName("makhc");
            entity.Property(e => e.Manguon)
                .HasMaxLength(9)
                .HasColumnName("manguon");
            entity.Property(e => e.Maphong)
                .HasMaxLength(20)
                .HasColumnName("maphong");
            entity.Property(e => e.Mathe)
                .HasMaxLength(20)
                .HasColumnName("mathe");
            entity.Property(e => e.Muangoai)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("muangoai");
            entity.Property(e => e.NamQt)
                .HasMaxLength(4)
                .HasColumnName("nam_qt");
            entity.Property(e => e.Namkt)
                .HasMaxLength(4)
                .HasColumnName("namkt");
            entity.Property(e => e.Ngayhd).HasColumnName("ngayhd");
            entity.Property(e => e.Ngayin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngayin");
            entity.Property(e => e.Ngaylap).HasColumnName("ngaylap");
            entity.Property(e => e.Ngayth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngayth");
            entity.Property(e => e.NgaythKhoa)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngayth_khoa");
            entity.Property(e => e.Ngayxoa)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngayxoa");
            entity.Property(e => e.Nhanct)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("nhanct");
            entity.Property(e => e.Noitru)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("noitru");
            entity.Property(e => e.PayPayid)
                .HasMaxLength(200)
                .HasColumnName("pay_payid");
            entity.Property(e => e.PayType)
                .HasMaxLength(10)
                .HasColumnName("pay_type");
            entity.Property(e => e.Ptbanle)
                .HasPrecision(3)
                .HasColumnName("ptbanle");
            entity.Property(e => e.Ptcong)
                .HasPrecision(15, 10)
                .HasColumnName("ptcong");
            entity.Property(e => e.Quidoi)
                .HasPrecision(4)
                .HasColumnName("quidoi");
            entity.Property(e => e.Sang)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("sang");
            entity.Property(e => e.Sdnguonkhac)
                .HasPrecision(1)
                .HasColumnName("sdnguonkhac");
            entity.Property(e => e.Soctcd)
                .HasMaxLength(20)
                .HasColumnName("soctcd");
            entity.Property(e => e.Soctnb)
                .HasMaxLength(50)
                .HasColumnName("soctnb");
            entity.Property(e => e.Soctvp)
                .HasMaxLength(20)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("soctvp");
            entity.Property(e => e.Soctvphd)
                .HasMaxLength(20)
                .HasColumnName("soctvphd");
            entity.Property(e => e.Sohd)
                .HasMaxLength(20)
                .HasColumnName("sohd");
            entity.Property(e => e.Sohdc)
                .HasMaxLength(20)
                .HasColumnName("sohdc");
            entity.Property(e => e.Sohdnb)
                .HasMaxLength(20)
                .HasColumnName("sohdnb");
            entity.Property(e => e.Sohdx)
                .HasMaxLength(20)
                .HasColumnName("sohdx");
            entity.Property(e => e.Solanin)
                .HasPrecision(2)
                .HasDefaultValueSql("0")
                .HasColumnName("solanin");
            entity.Property(e => e.Solo)
                .HasMaxLength(100)
                .HasColumnName("solo");
            entity.Property(e => e.Soluong)
                .HasPrecision(20, 3)
                .HasColumnName("soluong");
            entity.Property(e => e.Sophieudutru)
                .HasMaxLength(20)
                .HasColumnName("sophieudutru");
            entity.Property(e => e.Stent2lan)
                .HasPrecision(2)
                .HasColumnName("stent2lan");
            entity.Property(e => e.Stt)
                .HasPrecision(6)
                .HasDefaultValueSql("0")
                .HasColumnName("stt");
            entity.Property(e => e.Taikhoan)
                .HasMaxLength(20)
                .HasColumnName("taikhoan");
            entity.Property(e => e.Tamin)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("tamin");
            entity.Property(e => e.Tamkhoa)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("tamkhoa");
            entity.Property(e => e.Tenmay)
                .HasMaxLength(20)
                .HasColumnName("tenmay");
            entity.Property(e => e.ThangQt)
                .HasMaxLength(2)
                .HasColumnName("thang_qt");
            entity.Property(e => e.Thangkt)
                .HasMaxLength(2)
                .HasColumnName("thangkt");
            entity.Property(e => e.Thanhtien)
                .HasPrecision(24, 2)
                .HasColumnName("thanhtien");
            entity.Property(e => e.Thanhtienbhyt)
                .HasPrecision(14, 6)
                .HasColumnName("thanhtienbhyt");
            entity.Property(e => e.Thanhtoan)
                .HasMaxLength(5)
                .HasColumnName("thanhtoan");
            entity.Property(e => e.Theodon)
                .HasPrecision(20, 3)
                .HasColumnName("theodon");
            entity.Property(e => e.Thu)
                .HasPrecision(16, 4)
                .HasColumnName("thu");
            entity.Property(e => e.Thuock)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("thuock");
            entity.Property(e => e.Tienck)
                .HasPrecision(14, 2)
                .HasColumnName("tienck");
            entity.Property(e => e.Tientvat)
                .HasPrecision(24, 2)
                .HasColumnName("tientvat");
            entity.Property(e => e.Tienvat)
                .HasPrecision(14, 2)
                .HasColumnName("tienvat");
            entity.Property(e => e.Toacon)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("toacon");
            entity.Property(e => e.Toatutruc)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("toatutruc");
            entity.Property(e => e.Toaxv)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("toaxv");
            entity.Property(e => e.Toi)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("toi");
            entity.Property(e => e.Travedieutri)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("travedieutri");
            entity.Property(e => e.Trua)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("trua");
            entity.Property(e => e.Ttchinhtoa)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("ttchinhtoa");
            entity.Property(e => e.Tutruc)
                .HasMaxLength(10)
                .HasColumnName("tutruc");
            entity.Property(e => e.Tutrucc)
                .HasMaxLength(20)
                .HasColumnName("tutrucc");
            entity.Property(e => e.Userin)
                .HasMaxLength(20)
                .HasColumnName("userin");
            entity.Property(e => e.Vat)
                .HasPrecision(3, 1)
                .HasColumnName("vat");
            entity.Property(e => e.Visa)
                .HasMaxLength(500)
                .HasColumnName("visa");
            entity.Property(e => e.Xoa)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("xoa");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
