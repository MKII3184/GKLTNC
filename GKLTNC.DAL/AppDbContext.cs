using GKLTNC.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GKLTNC.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<PhongBan> PhongBans { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = DatabaseConfiguration.GetConnectionString();
                optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ giữa PhongBan và NhanVien (1 - nhiều)
            modelBuilder.Entity<NhanVien>()
                .HasOne(nv => nv.PhongBan)
                .WithMany(pb => pb.NhanViens)
                .HasForeignKey(nv => nv.MaPB)
                .OnDelete(DeleteBehavior.Restrict);

            // Dữ liệu mẫu (Seed Data) cho PhongBan
            modelBuilder.Entity<PhongBan>().HasData(
                new PhongBan { MaPB = "PB01", TenPB = "Phòng Kỹ thuật" },
                new PhongBan { MaPB = "PB02", TenPB = "Phòng Kế toán" },
                new PhongBan { MaPB = "PB03", TenPB = "Phòng Nhân sự" },
                new PhongBan { MaPB = "PB04", TenPB = "Phòng Kinh doanh" },
                new PhongBan { MaPB = "PB05", TenPB = "Phòng Hành chính" }
            );

            // Dữ liệu mẫu (Seed Data) cho NhanVien
            modelBuilder.Entity<NhanVien>().HasData(
                new NhanVien
                {
                    MaNV = "NV01",
                    HoTen = "Nguyễn Văn An",
                    GioiTinh = true, // Nam
                    NgaySinh = new DateTime(1995, 5, 15),
                    DiaChi = "Hà Nội",
                    Luong = 15000000m,
                    MaPB = "PB01"
                },
                new NhanVien
                {
                    MaNV = "NV02",
                    HoTen = "Trần Thị Bình",
                    GioiTinh = false, // Nữ
                    NgaySinh = new DateTime(1998, 8, 20),
                    DiaChi = "Đà Nẵng",
                    Luong = 12500000m,
                    MaPB = "PB02"
                },
                new NhanVien
                {
                    MaNV = "NV03",
                    HoTen = "Lê Hoàng Cường",
                    GioiTinh = true, // Nam
                    NgaySinh = new DateTime(1992, 12, 10),
                    DiaChi = "TP. Hồ Chí Minh",
                    Luong = 18000000m,
                    MaPB = "PB04"
                }
            );
        }
    }

    /// <summary>
    /// Factory phục vụ Entity Framework Core Migrations lúc Design-Time
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            string connectionString = DatabaseConfiguration.GetConnectionString();
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
