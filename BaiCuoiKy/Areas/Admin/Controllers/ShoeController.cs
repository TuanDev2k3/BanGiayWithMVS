	using BaiCuoiKy.Data;
using BaiCuoiKy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaiCuoiKy.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ShoeController : Controller
	{

		private readonly ApplicationDbContext _db;
		public ShoeController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Home()
		{
			var product = _db.Product;
			ViewBag.Product = product;
			return View();
		}
		//CREATE SẢN PHẦM
		[HttpGet]
		public IActionResult Create_Product()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create_Product(Product product)
		{
			if (ModelState.IsValid)
			{
				_db.Product.Add(product);
				_db.SaveChanges();
				return RedirectToAction("Home");
			}
			return View();
		}


		//EDIT SẢN PHẨM
		[HttpGet]
		public IActionResult Edit_Product(int id)
		{
			if (id == 0)
			{
				return NotFound();
			}
			var product = _db.Product.Find(id);
			return View(product);
		}
		[HttpPost]
		public IActionResult Edit_Product(Product product)
		{
			if (ModelState.IsValid)
			{
				_db.Product.Update(product);
				_db.SaveChanges();
				return RedirectToAction("Home");
			}
			return View();
		}


		//DELETE SẢN PHẨM
		//DELETE SẢN PHẨM
		public IActionResult Detele_Product(int id)
		{
			var product = _db.Product.Find(id);
			if (product == null)
			{
				return NotFound();
			}
			_db.Product.Remove(product);
			_db.SaveChanges();
			return RedirectToAction("Home");
		}
		public IActionResult Login()
		{
			return View();
		}
	}
}
