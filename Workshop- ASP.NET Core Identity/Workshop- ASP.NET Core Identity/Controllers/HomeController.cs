using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Workshop__ASP.NET_Core_Identity.Models;

namespace Workshop__ASP.NET_Core_Identity.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}