using Microsoft.EntityFrameworkCore;

namespace WebAPI_HoaDon.Entities
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams{ get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"server=NGUYENVANMINH;database=WebAPI_HoaDon;integrated security=sspi;Encrypt=True;TrustServerCertificate=True;");
        }

    }
}
