using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using XLN_Fault_Report_System.Models;


namespace xlnWork.Controllers;

public class ErrorForm : Controller
{


    public ErrorForm()
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

    //[HttpPost]
    public IActionResult Diagnostics()
    {
        return View();
    }

}

