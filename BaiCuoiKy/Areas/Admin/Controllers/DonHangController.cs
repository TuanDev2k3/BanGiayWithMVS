using BaiCuoiKy.Data;
using BaiCuoiKy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaiCuoiKy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DonHangController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DonHangController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
			IEnumerable<HoaDon> hoadon = _db.HoaDon.ToList();
			return View(hoadon);
		}
		public IActionResult Detele(int id)
		{
			var hoadon = _db.HoaDon.FirstOrDefault(dh => dh.ID == id);
			if (hoadon == null)
			{
				return NotFound();
			}
			_db.HoaDon.Remove(hoadon);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult DetailHD(int id)
		{
			var donhang = _db.ChiTietHoaDon.Include("Product").Where(h => h.HoaDonId == id).ToList();

			ViewBag.Donhang = donhang;
			int ToTal = 0;
			foreach (var item in @ViewBag.Donhang) 
			{
				ToTal += item.ProductPrice;
            }
			ViewBag.Total = ToTal;
			return View();
		}
	}
}
