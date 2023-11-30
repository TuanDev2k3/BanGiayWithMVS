using BaiCuoiKy.Data;
using BaiCuoiKy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace BaiCuoiKy.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _db;
        public GioHangController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //Lấy thông tin tài khoản đăng nhập 
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            GioHangViewModel giohang = new GioHangViewModel()
            {
                DsGioHang = _db.GioHang
                .Include("Product")
                .Where(x => x.ApplicationUserId == claim.Value)
                .ToList(),
                HoaDon = new HoaDon()
            };
            foreach (var item in giohang.DsGioHang)
            {
                // tinh tien theo SL
                item.ProductPrice = item.Quantity * item.Product.Price;
                // Cong tien gio hang
                giohang.HoaDon.Total += item.ProductPrice;
            }
            return View(giohang);
        }

        public IActionResult Giam(int giohangId)
        {
            // lay thong tin gio hang
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
            giohang.Quantity -= 1;
            if (giohang.Quantity == 0)
            {
                _db.GioHang.Remove(giohang);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Tang(int giohangId)
        {
            // lay thong tin gio hang
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
            giohang.Quantity += 1;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Xoa(int giohangId)
        {
            // lay thong tin gio hang
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
            _db.GioHang.Remove(giohang);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ThanhToan()
        {
            // Lay TTin
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            //Lay Ds SanPham trong gio hang cua User
            GioHangViewModel giohang = new GioHangViewModel()
            {
                DsGioHang = _db.GioHang
                .Include("Product")
                .Where(x => x.ApplicationUserId == claim.Value)
                .ToList(),
                HoaDon = new HoaDon()
            };

            //Tim thong tin TK trong csdl de hien len trang thanh toan
            giohang.HoaDon.ApplicationUser = _db.ApplicationUser.FirstOrDefault(user => user.Id == claim.Value);
            // Gan Thong tin TK vao hoa don
            giohang.HoaDon.Name = giohang.HoaDon.ApplicationUser.Name;
            giohang.HoaDon.Address = giohang.HoaDon.ApplicationUser.Address;
            giohang.HoaDon.PhoneNumber = giohang.HoaDon.ApplicationUser.Phone;

            foreach (var item in giohang.DsGioHang)
            {
                // tinh tien theo SL
                item.ProductPrice = item.Quantity * item.Product.Price;
                // Cong tien gio hang
                giohang.HoaDon.Total += item.ProductPrice;
            }
            return View(giohang);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThanhToan(GioHangViewModel giohang)
        {
            // Lay Thong Tin
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            // Cap nhat TT gio hang
            giohang.DsGioHang = _db.GioHang.Include("Product")
                .Where(gh => gh.ApplicationUserId == claim.Value).ToList();
            giohang.HoaDon.ApplicationUserId = claim.Value;
            giohang.HoaDon.OrderDate = DateTime.Now;
            giohang.HoaDon.OrderStatus = "Đang xác nhận";
            foreach (var item in giohang.DsGioHang)
            {
                item.ProductPrice = item.Quantity * item.Product.Price;
                // Cong don gio hang
                giohang.HoaDon.Total += item.ProductPrice;
            }
            _db.HoaDon.Add(giohang.HoaDon);
            _db.SaveChanges();

            foreach (var item in giohang.DsGioHang)
            {
                ChiTietHoaDon chitiet = new ChiTietHoaDon()
                {
                    ProductId = item.ProductId,
                    HoaDonId = giohang.HoaDon.ID,
                    ProductPrice = item.ProductPrice,
                    Quantity = item.Quantity
                };
                _db.ChiTietHoaDon.Add(chitiet);
                _db.SaveChanges();
            }

            // Xoa thong tin trong gio hang
            _db.GioHang.RemoveRange(giohang.DsGioHang);
            _db.SaveChanges();
            return RedirectToAction("PaysSuccess");
        }

        public IActionResult PaysSuccess()
        {
            return View();
        }
    }
}
