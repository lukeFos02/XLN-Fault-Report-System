using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using XLN_Fault_Report_System.Models;


namespace XLN_Fault_Report_System.Controllers
{
    public class ErrorFormController : Controller
    {
        private Fault currentFault;

        public ErrorFormController()
        {

        }
        public IActionResult WarningPage()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ErrorForm1()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ErrorForm1(bool cli, bool bb)
        {
            currentFault = new Fault();
            var ServiceType = "";
            if (cli != false)
            {
                ServiceType = ServiceType + ",Client";
            }
            if (bb != false)
            {
                ServiceType = ServiceType + ",Broadband";
            }
            currentFault.ServiceType = ServiceType;
            return RedirectToAction("ErrorForm2", "ErrorForm");
        }
        public IActionResult ErrorForm2()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ErrorForm2(bool bellnotring, bool contdialtone, bool crossedlines, 
            bool cutsoff, bool nodialtone, bool noisy, bool damage, bool intermittent, bool earlylife, 
            bool broadbandfault, bool landlinefault, bool firmware, bool webpages, bool yes, bool no)
        {
            var IncidentType = "";

            return View();
        }

        //[HttpPost]
        public IActionResult Diagnostics()
        {
            return View();
        }

    }
}
