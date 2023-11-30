using BaiCuoiKy.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaiCuoiKy.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TaiKhoanController : Controller
	{
		private readonly ApplicationDbContext _db;
		public TaiKhoanController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			var taikhoan = _db.ApplicationUser.Where(x => x.Name != null).ToList();
			ViewBag.TaiKhoan = taikhoan;
			return View();
		}
		public IActionResult Detele(string id)
		{
			Models.ApplicationUser? taikhoan = _db.ApplicationUser.Find(id);
			if (taikhoan == null)
			{
				return NotFound();
			}
			_db.ApplicationUser.Remove(taikhoan);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
