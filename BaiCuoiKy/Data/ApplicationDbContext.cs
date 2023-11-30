using BaiCuoiKy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaiCuoiKy.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Product> Product { get; set; }
		public DbSet<ApplicationUser> ApplicationUser { get; set; }
		public DbSet<GioHang> GioHang { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
		public DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
	}
}