using BaiCuoiKy.Data;
using BaiCuoiKy.Data.Migrations;
using BaiCuoiKy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System.Security.Claims;

namespace BaiCuoiKy.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.Active = 5;

            var product = _db.Product.ToList();
            return View(product);
        }
        public IActionResult Product(int? page, string SearchString = "")
        {
            ViewBag.Active = 2; 
            IQueryable<Product> products = _db.Product;

            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(x => x.Name.ToUpper().Contains(SearchString.ToUpper()));
            }

            int pageSize = 8;
            int pageNumber = page ?? 1;

            var pagedProducts = products.OrderBy(x => x.Id).ToPagedList(pageNumber, pageSize);

            return View(pagedProducts);

        }

        [HttpGet]
        public IActionResult Product_Detail(int productId)
        {
            GioHang giohang = new GioHang()
            {
                ProductId = productId,
                Product = _db.Product.Find(productId),
                Quantity = 1
            };
            return View(giohang);
        }
        [HttpPost]
        [Authorize] // Yeu cau login
        public IActionResult Product_Detail(GioHang giohang)
        {
            // Lay thong tin tk
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);      
            giohang.ApplicationUserId = claim.Value;
            // Them vao cart
            var giohangdb = _db.GioHang.FirstOrDefault(sp => sp.ProductId == giohang.ProductId
            && sp.ApplicationUserId == giohang.ApplicationUserId);
            if (giohangdb == null)
            {
                _db.GioHang.Add(giohang);
            }
            else
            {
                giohangdb.Quantity += giohang.Quantity;
            }
            _db.SaveChanges();
            return Redirect("/Customer/GioHang/Index");
        }
        [Authorize]
        public IActionResult Order(string userId)
        {
            ViewBag.Active = 3;

            // Lay thong tin tk
            IEnumerable<HoaDon> hoadon = _db.HoaDon.Where(x => x.ApplicationUser.Email == userId).ToList();
            return View(hoadon);
        }
		public IActionResult Detele_Order(int id)
		{
			var hoadon = _db.HoaDon.FirstOrDefault(dh => dh.ID == id);
			if (hoadon == null)
			{
				return NotFound();
			}
			_db.HoaDon.Remove(hoadon);
			_db.SaveChanges();
			return RedirectToAction("Order");
		}
        public IActionResult Contact ()
        {
            ViewBag.Active = 4;

            return View(); 
        }
	}
}
