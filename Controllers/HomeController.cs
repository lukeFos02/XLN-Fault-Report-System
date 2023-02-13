using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XLN_Fault_Report_System.Models;
using XLN_Fault_Report_System.Services;
using Microsoft.AspNetCore.Http;

namespace XLN_Fault_Report_System.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogin _loginUser;
        private readonly IHttpContextAccessor _contextAccessor;
        public HomeController(ILogin loginUser, IHttpContextAccessor contextAccessor)
        {
            _loginUser = loginUser;
            _contextAccessor = contextAccessor;	
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
				_contextAccessor.HttpContext.Session.SetString("Username", username);
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