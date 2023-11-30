using BaiCuoiKy.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaiCuoiKy.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]


	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
