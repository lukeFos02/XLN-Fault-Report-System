using Microsoft.AspNetCore.Mvc;
using XLN_Fault_Report_System.Services;
using XLN_Fault_Report_System.Models;

namespace XLN_Fault_Report_System.Controllers
{
    public class LandingPageController : Controller
    {
        private readonly IServices _services;
        private readonly IHttpContextAccessor _contextAccessor;
        public LandingPageController(IServices services, IHttpContextAccessor contextAccessor)
        {
            _services = services;
            _contextAccessor = contextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NumbersPage()
        {
            string Username = _contextAccessor.HttpContext.Session.GetString("Username");
            string Password = _contextAccessor.HttpContext.Session.GetString("Password");
            User User = _services.GetUser(Username, Password);
            List<Asset> Assets = _services.GetUsersAssets(Username, Password);  
            TempData["User"] = User;
            TempData["UsersAssets"] = Assets;
            return View();
        }
        [HttpPost]
        public IActionResult NumbersPage(Asset asset)
        {
            Asset chosenAsset = _services.GetAsset(asset.AssetId);
            _contextAccessor.HttpContext.Session.SetInt32("ChosenAssetId", chosenAsset.AssetId);
            return RedirectToAction("CustomerDetails", "ErrorForm");
        }
    }
}
