using Microsoft.EntityFrameworkCore;

namespace his_backend.Models;

public class AppDbContext : DbContext
{
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
      public DbSet<AppUser> AppUsers { get; set; }
      public DbSet<Nguoibenhdangky> Nguoibenhdangkys { get; set; }
      public DbSet<AppUserHoSo> AppUserHoSos { get; set; }
      public DbSet<Dmchuyenkhoa> Dmchuyenkhoas { get; set; }
      public DbSet<DangKyKham> DangKyKhams { get; set; }
      public DbSet<Dmnhanvien> Dmnhanviens { get; set; }
      public DbSet<Lichtruc> Lichtrucs { get; set; }
      public DbSet<BacsiChuyenKhoa> BacsiChuyenKhoas { get; set; }



      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<AppUser>(entity =>
      {
            entity.HasKey(e => e.Mand);
            entity.ToTable("app_users", "benhnhan");

            entity.HasIndex(e => e.SoDienThoai)
                  .IsUnique()
                  .HasDatabaseName("app_users_sdt_unique");

            entity.Property(e => e.Mand)
                  .HasColumnName("mand")
                  .UseIdentityAlwaysColumn();

            entity.Property(e => e.SoDienThoai)
                  .IsRequired().HasMaxLength(15)
                  .HasColumnName("sodienthoai");

            entity.Property(e => e.Holot)
                  .HasMaxLength(255)
                  .HasColumnName("holot");

            entity.Property(e => e.Ten)
                  .HasMaxLength(255)
                  .HasColumnName("ten");

            entity.Property(e => e.MatKhauHash)
                  .IsRequired().HasMaxLength(500)
                  .HasColumnName("matkhauhash");

            entity.Property(e => e.IsActive)
                  .HasDefaultValue(true)
                  .HasColumnName("isactive");

            entity.Property(e => e.NgayTao)
                  .HasDefaultValueSql("now()")
                  .HasColumnType("timestamp with time zone")
                  .HasColumnName("ngaytao");

            entity.Property(e => e.LanDangNhapCuoi)
                  .HasColumnType("timestamp with time zone")
                  .HasColumnName("landangnhapcuoi");

            entity.Property(e => e.RefreshToken)
                  .HasMaxLength(500)
                  .HasColumnName("refresh_token");

            entity.Property(e => e.RefreshTokenHetHan)
                  .HasColumnType("timestamp with time zone")
                  .HasColumnName("refresh_token_het_han");
            });

      modelBuilder.Entity<Dmnhanvien>(entity =>
      {
            entity.HasKey(e => e.Manv);
            entity.ToTable("dmnhanvien", "benhnhan");

            entity.HasOne(e => e.ChuyenKhoa)
                  .WithMany()
                  .HasForeignKey(e => e.Mack)
                  .HasPrincipalKey(ck => ck.Mack);
      });
      modelBuilder.Entity<Lichtruc>(entity =>
      {
            entity.HasKey(e => e.Malt);
            entity.ToTable("lichtruc", "benhnhan");

            entity.Property(e => e.Malt)
                  .HasColumnName("malt");

            entity.Property(e => e.Mabs)
                  .HasMaxLength(255)
                  .HasColumnName("mabs");

            entity.Property(e => e.Mack)
                  .HasMaxLength(255)
                  .HasColumnName("mack");

            entity.Property(e => e.Mapk)
                  .HasMaxLength(255)
                  .HasColumnName("mapk");

            entity.Property(e => e.Ngay)
                  .HasColumnType("date")
                  .HasColumnName("ngay");

            entity.Property(e => e.Thu)
                  .HasColumnType("numeric(1,0)")
                  .HasColumnName("thu");

            entity.Property(e => e.GioBatDau)
                  .HasColumnType("time")
                  .HasColumnName("giobatdau");

            entity.Property(e => e.GioKetThuc)
                  .HasColumnType("time")
                  .HasColumnName("gioketthuc");

            entity.Property(e => e.LoaiCa)
                  .HasMaxLength(10)
                  .HasColumnName("loaica");

            entity.Property(e => e.TrangThai)
                  .HasColumnType("numeric(1,0)")
                  .HasColumnName("trangthai");
      });
      modelBuilder.Entity<Dmchuyenkhoa>(entity =>
      {
            entity.HasKey(e => e.Mack);
            entity.ToTable("dmchuyenkhoa", "benhnhan");
      });
      modelBuilder.Entity<BacsiChuyenKhoa>(entity =>
      {
            entity.HasKey(e => e.Id);
            entity.ToTable("bacsi_chuyenkhoa", "benhnhan");

            entity.Property(e => e.Manv).HasColumnName("manv");
            entity.Property(e => e.Mack).HasColumnName("mack");

            entity.HasOne(e => e.NhanVien)
                  .WithMany(nv => nv.BacsiChuyenKhoas)
                  .HasForeignKey(e => e.Manv)
                  .HasPrincipalKey(nv => nv.Manv);

            entity.HasOne(e => e.ChuyenKhoa)
                  .WithMany()
                  .HasForeignKey(e => e.Mack)
                  .HasPrincipalKey(ck => ck.Mack);
      });
      modelBuilder.Entity<Nguoibenhdangky>(entity =>
      {     
            entity.HasKey(e => e.Id);
            entity.ToTable("nguoibenhdangky", "benhnhan");

            entity.Property(e => e.Id)
                  .UseIdentityAlwaysColumn()
                  .HasColumnName("id");

            entity.Property(e => e.Holot)
                  .IsRequired().HasMaxLength(255)
                  .HasColumnName("holot");

            entity.Property(e => e.Ten)
                  .IsRequired().HasMaxLength(255)
                  .HasColumnName("ten");

            entity.Property(e => e.Ngaysinh)
                  .HasColumnType("date")
                  .HasColumnName("ngaysinh");

            entity.Property(e => e.Gioitinh)
                  .HasColumnType("numeric(1,0)")
                  .HasColumnName("gioitinh");

            entity.Property(e => e.Diachi)
                  .HasMaxLength(500)
                  .HasColumnName("diachi");

            entity.Property(e => e.Sodienthoai)
                  .HasMaxLength(20)
                  .HasColumnName("sodienthoai");

            entity.Property(e => e.Cmnd)
                  .HasMaxLength(20)
                  .HasColumnName("cmnd");

            // Index để tra cứu nhanh và tránh hồ sơ trùng theo CMND
            entity.HasIndex(e => e.Cmnd)
                  .HasDatabaseName("nguoibenhdangky_cmnd_idx");

            entity.Property(e => e.Ngaycap)
                  .HasColumnType("date")
                  .HasColumnName("ngaycap");

            entity.Property(e => e.Noicap)
                  .HasMaxLength(256)
                  .HasColumnName("noicap");

            entity.Property(e => e.Maloaigiayto)
                  .HasMaxLength(20)
                  .HasColumnName("maloaigiayto");

            entity.Property(e => e.Maqg)
                  .HasMaxLength(20)
                  .HasDefaultValue("VN")
                  .HasColumnName("maqg");

            entity.Property(e => e.NhomMau)
                  .HasMaxLength(5)
                  .HasColumnName("nhommau");

            entity.Property(e => e.Mathe)
                  .HasMaxLength(50)
                  .HasColumnName("mathe");

            entity.Property(e => e.Madt)
                  .HasMaxLength(20)
                  .HasColumnName("madt");

            entity.Property(e => e.Manghe)
                  .HasMaxLength(20)
                  .HasColumnName("manghe");

            entity.Property(e => e.Maxa)
                  .HasMaxLength(20)
                  .HasColumnName("maxa");

            entity.Property(e => e.Matinh)
                  .HasMaxLength(20)
                  .HasColumnName("matinh");
        });

        // ── AppUserHoSo (bảng liên kết nhiều-nhiều) ─────────────────────────
      modelBuilder.Entity<AppUserHoSo>(entity =>
      {
            entity.HasKey(e => new { e.AppUserId, e.HoSoId });
            entity.ToTable("app_user_hoso", "benhnhan");

            entity.Property(e => e.AppUserId)
                  .HasColumnName("appuserid");

            entity.Property(e => e.HoSoId)
                  .HasColumnName("hosoid");

            entity.Property(e => e.QuanHe)
                  .HasMaxLength(30)
                  .HasDefaultValue("ban_than")
                  .HasColumnName("quanhe");

            entity.Property(e => e.LaMacDinh)
                  .HasDefaultValue(false)
                  .HasColumnName("lamacdinh");

            entity.Property(e => e.NgayLienKet)
                  .HasDefaultValueSql("now()")
                  .HasColumnType("timestamp with time zone")
                  .HasColumnName("ngaylienket");

            // Quan hệ: AppUser → AppUserHoSo
            entity.HasOne(e => e.AppUser)
                  .WithMany(u => u.HoSoLienKets)
                  .HasForeignKey(e => e.AppUserId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ: Nguoibenhdangky → AppUserHoSo
            entity.HasOne(e => e.HoSo)
                  .WithMany(h => h.UserLienKets)
                  .HasForeignKey(e => e.HoSoId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
      
      modelBuilder.Entity<DangKyKham>(entity =>
      {
            entity.HasKey(e => e.MaDk);
            entity.ToTable("dangkykham", "benhnhan");

            entity.Property(e => e.MaDk)
                  .UseIdentityAlwaysColumn()
                  .HasColumnName("madk");

            entity.Property(e => e.Mandk)
                  .HasColumnName("mandk");

            entity.Property(e => e.Mapk)
                  .HasColumnName("mapk");

            entity.Property(e => e.Mabs)
                  .HasColumnName("mabs");

            entity.Property(e => e.Hoten)
                  .HasMaxLength(255)
                  .HasColumnName("hoten");

            entity.Property(e => e.Diachi)
                  .HasMaxLength(500)
                  .HasColumnName("diachi");

            entity.Property(e => e.Sdt)
                  .HasMaxLength(20)
                  .HasColumnName("sdt");

            entity.Property(e => e.Cmnd)
                  .HasMaxLength(20)
                  .HasColumnName("cmnd");

            entity.Property(e => e.LoaiQh)
                  .HasMaxLength(30)
                  .HasColumnName("loaiqh");

            entity.Property(e => e.HoTenQh)
                  .HasMaxLength(255)
                  .HasColumnName("hotenqh");

            entity.Property(e => e.DienThoaiQh)
                  .HasMaxLength(20)
                  .HasColumnName("dienthoaiqh");

            entity.Property(e => e.DiachiQh)
                  .HasMaxLength(500)
                  .HasColumnName("diachiqh");

            entity.Property(e => e.Ngaysinh)
                  .HasColumnType("date")
                  .HasColumnName("ngaysinh");

            entity.Property(e => e.TimeSlot)
                  .HasColumnType("timestamp with time zone")
                  .HasColumnName("timeslot");

            entity.Property(e => e.Ngay)
                  .HasColumnType("date")
                  .HasColumnName("ngay");

            entity.Property(e => e.NgaySua)
                  .HasColumnType("timestamp with time zone")
                  .HasColumnName("ngaysua");

            entity.Property(e => e.MaCk)
                  .HasMaxLength(20)
                  .HasColumnName("mack");

            entity.Property(e => e.GiaTien)
                  .HasColumnType("numeric(10,2)")
                  .HasColumnName("giatien");

            entity.Property(e => e.TrangThai)
                  .HasColumnType("numeric(1,0)")
                  .HasColumnName("trangthai");

            entity.Property(e => e.LoaiKham)
                  .HasMaxLength(50)
                  .HasColumnName("loaikham");

            entity.Property(e => e.GhiChu)
                  .HasMaxLength(500)
                  .HasColumnName("ghichu");

       

            entity.Property(e => e.Mathe)
                  .HasMaxLength(50)
                  .HasColumnName("mathe");

            entity.Property(e => e.Phikham)
                  .HasColumnType("numeric(10,2)")
                  .HasColumnName("phikham");

            entity.Property(e => e.Phidv)
                  .HasColumnType("numeric(10,2)")
                  .HasColumnName("phidv");

            entity.Property(e => e.Phithuoc)
                  .HasColumnType("numeric(10,2)")
                  .HasColumnName("phithuoc");

            entity.Property(e => e.Status)
                  .HasMaxLength(50)
                  .HasColumnName("status");

            entity.Property(e => e.Xoa)
                  .HasColumnType("boolean")
                  .HasColumnName("xoa");

            entity.Property(e => e.HisId)
                  .HasColumnType("integer")
                  .HasColumnName("hisid");

            entity.Property(e => e.MngthisId)
                  .HasMaxLength(50)
                  .HasColumnName("mngthisid");

            entity.Property(e => e.HiqrCode)
                  .HasMaxLength(255)
                  .HasColumnName("hiqrcode");
      });   
      modelBuilder.Entity<Dmchuyenkhoa>(entity =>
      {
            entity.HasKey(e => e.Mack);
            entity.ToTable("dmchuyenkhoa", "benhnhan");

            entity.Property(e => e.Mack)
                  .HasColumnName("mack");

            entity.Property(e => e.TenCk)
                  .HasMaxLength(255)
                  .HasColumnName("tenck");

            entity.Property(e => e.MoTaTrieuChung)
                  .HasMaxLength(255)
                  .HasColumnName("mota_trieuchung");
        });
      }
    
}
