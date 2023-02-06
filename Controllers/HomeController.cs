using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XLN_Fault_Report_System.Models;
using XLN_Fault_Report_System.Services;

namespace XLN_Fault_Report_System.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogin _loginUser;

		public HomeController(ILogin loginUser)
		{
			_loginUser= loginUser;
		}

		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(string username, string passcode)
		{
            bool issuccess = _loginUser.AuthenticateUser(username, passcode);

			if (issuccess == true)
			{
				ViewBag.username = string.Format(username);
				return RedirectToAction("Index", "LandingPage");
			}
			else
			{
				ViewBag.username = string.Format("Login Failed");
				return View();
			}
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}