using Microsoft.AspNetCore.Mvc;

namespace XLN_Fault_Report_System.Controllers
{
    public class LandingPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
