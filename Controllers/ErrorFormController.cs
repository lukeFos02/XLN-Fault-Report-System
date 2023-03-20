using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using XLN_Fault_Report_System.Models;
using XLN_Fault_Report_System.Services;

namespace XLN_Fault_Report_System.Controllers
{
    public class ErrorFormController : Controller
    {
        private readonly IServices _service;
        private readonly IHttpContextAccessor _contextAccessor;
        public ErrorFormController(IServices loginUser, IHttpContextAccessor contextAccessor)
        {
            _service = loginUser;
            _contextAccessor = contextAccessor;
        }
        public IActionResult WarningPage()
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
            var ServiceType = "";
            if (cli != false)
            {
                ServiceType = ServiceType + ",Client";
            }
            if (bb != false)
            {
                ServiceType = ServiceType + ",Broadband";
            }
            _contextAccessor.HttpContext.Session.SetString("ServiceType", ServiceType);
            return RedirectToAction("ErrorForm2", "ErrorForm");
        }
        public IActionResult ErrorForm2()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ErrorForm2(bool bellnotring, bool contdialtone, bool crossedlines, 
            bool cutsoff, bool nodialtone, bool noisy, bool damage, bool intermittent, bool earlylife, 
            bool broadbandfault, bool landlinefault, bool firmware, bool webpages, string otherErrors, string errordescription)
        {
            var IncidentType = "";
            if (bellnotring != false) { IncidentType += ",Bell not ringing"; }
            if (contdialtone != false) { IncidentType += ",Continuois dial tone"; }
            if (crossedlines != false) { IncidentType += "Crossed lines"; }
            if (cutsoff != false) { IncidentType += ",Cuts off"; }
            if (nodialtone != false) { IncidentType += ",No dial tone"; }
            if (noisy != false) { IncidentType += ",Noisy line"; }
            if (damage != false) { IncidentType += ",Damage"; }

            if (intermittent != false) { IncidentType += ",Intermittent conection"; }
            if (earlylife != false) { IncidentType += ",Early life failure"; }
            if (broadbandfault != false) { IncidentType += ",Broadband Fault"; }
            if (landlinefault != false) { IncidentType += ",Landline fault"; }
            if (firmware != false) { IncidentType += ",Firmware upgraded"; }
            if (webpages != false) { IncidentType += ",No web pages loading"; }
            if (otherErrors == "yes") { IncidentType += ",Yes"; } else { IncidentType += ",No"; }
            if (errordescription != null) { _contextAccessor.HttpContext.Session.SetString("ErrorDescription", errordescription); }
            _contextAccessor.HttpContext.Session.SetString("IncidentType", IncidentType);

            return RedirectToAction("ErrorForm3", "ErrorForm");
        }
        public IActionResult ErrorForm3()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ErrorForm3(string intermittent, string intermittentdescription)
        {
            var IntermittentStatus = "";
            if (intermittent == "yes") { IntermittentStatus += "Yes"; } else { IntermittentStatus += "No"; }
            if (intermittentdescription != null) { _contextAccessor.HttpContext.Session.SetString("IntermittentDescription", intermittentdescription); }
            _contextAccessor.HttpContext.Session.SetString("IntermittentStatus", IntermittentStatus);
            return RedirectToAction("Diagnostics", "ErrorForm");
        }
        public IActionResult CustomerDetails()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CustomerDetails(string contactname, string contactnumber, string contacthoursfrom, string contacthoursto)
        {
            _contextAccessor.HttpContext.Session.SetString("ContactName", contactname);
            contactnumber = contactnumber.Replace(" ", "");
            if (contactnumber[0] == '0' && contactnumber[1] == '7' && contactnumber.Length == 11)
            {
                foreach(char c in contactnumber)
                {
                    if (c < '0' || c > '9')
                    {
                        ViewBag.ContactNumber = String.Format("Please enter a valid phone number");
                        return View();
                    }
                }
                _contextAccessor.HttpContext.Session.SetString("ContactNumber", contactnumber);
            }
            else
            {
                ViewBag.ContactNumber = String.Format("Please enter a valid phone number");
                return View();
            }
            if (contacthoursfrom == null || contacthoursto == null)
            {
                ViewBag.ContactHours = String.Format("Please Insert A Time");
                return View();
            }

            string firstTwoNumbers = contacthoursfrom.Substring(0, 2);
            string lastTwoNumbers = contacthoursfrom.Substring(3);
            string hoursToFirstTwo = contacthoursto.Substring(0, 2);
            string hoursToLastTwo = contacthoursto.Substring(3);

            if (int.Parse(hoursToFirstTwo) < int.Parse(firstTwoNumbers))
            {
                ViewBag.ContactHours = String.Format("Please ensure that the value of the hours is from lower to higher");
                return View();
            }
            else if (int.Parse(hoursToFirstTwo) == int.Parse(firstTwoNumbers))
            {
                if (int.Parse(hoursToLastTwo) <= int.Parse(lastTwoNumbers))
                {
                    ViewBag.ContactHours = String.Format("Please ensure that the value of the hours is from lower to higher");
                    return View();
                }
            }

            _contextAccessor.HttpContext.Session.SetString("ContactHoursFrom", contacthoursfrom);
            _contextAccessor.HttpContext.Session.SetString("ContactHoursTo", contacthoursto);
            return RedirectToAction("FaultSubmitted", "ErrorForm");
        }
        public IActionResult FaultSubmitted()
        {
            Fault fault = new Fault();

            fault.UserId                        =             (int)_contextAccessor.HttpContext.Session.GetInt32("UsersID");
            fault.AssetId                       =       (int)_contextAccessor.HttpContext.Session.GetInt32("ChosenAssetId");
            fault.ContactName                   =             _contextAccessor.HttpContext.Session.GetString("ContactName");
            fault.ContactNumber                 =           _contextAccessor.HttpContext.Session.GetString("ContactNumber");
            fault.ContactHoursFrom              =        _contextAccessor.HttpContext.Session.GetString("ContactHoursFrom");
            fault.ContactHoursTo                =          _contextAccessor.HttpContext.Session.GetString("ContactHoursTo");
            fault.ServiceType                   =             _contextAccessor.HttpContext.Session.GetString("ServiceType");
            fault.IncidentType                  =            _contextAccessor.HttpContext.Session.GetString("IncidentType");
            fault.ErrorDescription              =        _contextAccessor.HttpContext.Session.GetString("ErrorDescription");
            fault.IntermittentStatus            =      _contextAccessor.HttpContext.Session.GetString("IntermittentStatus");
            fault.IntermittentStatusDescription = _contextAccessor.HttpContext.Session.GetString("IntermittentDescription");
            fault.DiagnosticResult              =        _contextAccessor.HttpContext.Session.GetString("DiagnosticResult");
            fault.Time = DateTime.Now.ToString();
            fault.Status = "Fault report pending";

            string to = "hmssos385@gmail.com";
            string from = "hmssos385@gmail.com";
            MailMessage message = new MailMessage(from, to);

            _service.SaveFault(fault);

            Fault newFault = _service.GetNewFault(fault.AssetId);
            _contextAccessor.HttpContext.Session.SetInt32("NewFaultID", newFault.FaultId);

            string mailbody = "Your error has been successfully logged into the system\nThis is the ID for your error: " + newFault.FaultId +
                " You can talk with a member of our team using this phone number: 077730330";
            message.Subject = "Error Confirmation";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("hmssos385@gmail.com", "ufvnbchfpsdbsjxl");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception)
            {
                throw;
            }

            return View();
        }
        public IActionResult Diagnostics()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Diagnostics(IFormCollection form)
        {
            string result = form["diagnostictest"];
            _contextAccessor.HttpContext.Session.SetString("DiagnosticResult", result);
            return RedirectToAction("WarningPage", "ErrorForm");
        }
        public IActionResult SelfCheckQuestions()
        {
            return View();
        }

    }
}
