using Microsoft.AspNetCore.Mvc;
using XLN_Fault_Report_System.Services;
using XLN_Fault_Report_System.Models;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;

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
            _contextAccessor.HttpContext.Session.SetInt32("UsersID", User.UserId);
            List<Asset> Assets = _services.GetUsersAssets(Username, Password);
            List<Fault> Faults = _services.GetUsersFaults(Username, Password);
            _services.UpdateFaultStatus(Faults);
            foreach (Asset a in Assets)
            {
                foreach (Fault f in Faults)
                {
                    if (a.AssetId == f.AssetId)
                    {
                        a.Faults.Add(f);
                    }
                }
            }
            TempData["User"] = User;
            TempData["UsersAssets"] = Assets;
            return View();
        }
        [HttpPost]
        public IActionResult NumbersPage(Asset asset)
        {
            Asset chosenAsset = _services.GetAsset(asset.AssetId);
            _contextAccessor.HttpContext.Session.SetInt32("ChosenAssetId", chosenAsset.AssetId);
            return RedirectToAction("ErrorForm1", "ErrorForm");
        }
        public IActionResult CancelFault(int fault) 
        {
            Fault chosenFault = _services.GetFault(fault);
            _services.CancelFault(chosenFault);
            return RedirectToAction("NumbersPage", "LandingPage");
        }
    }
}
