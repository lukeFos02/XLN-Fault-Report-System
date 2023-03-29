
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XLN_Fault_Report_System.Models;
using XLN_Fault_Report_System.Services;
using Microsoft.AspNetCore.Http;

namespace XLN_Fault_Report_System.Controllers
{
	public class HomeController : Controller
	{
		private readonly IServices _service;
        private readonly IHttpContextAccessor _contextAccessor;
        public HomeController(IServices loginUser, IHttpContextAccessor contextAccessor)
        {
            _service = loginUser;
            _contextAccessor = contextAccessor;	
        }

        public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(string username, string passcode)
		{
            bool issuccess = _service.AuthenticateUser(username, passcode);

			if (issuccess == true)
			{
				_contextAccessor.HttpContext.Session.SetString("Username", username);
				_contextAccessor.HttpContext.Session.SetString("Password", passcode);
				return RedirectToAction("Index", "LandingPage");
			}
			else
			{
				ViewBag.username = string.Format("Login Failed");
				return View();
			}
		}
		public IActionResult Logout()
		{
			_contextAccessor.HttpContext.Session.Remove("Username");
			_contextAccessor.HttpContext.Session.Remove("Password");
			return RedirectToAction("Index");
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