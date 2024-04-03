using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using COMP2139_labs.Models;

namespace COMP2139_labs.Controllers;

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

    public IActionResult About()
    {
        return View();
    }

    /*public IActionResult GeneralSearch(string searchType, string searchString)
    {
        if (searchType == "Projects")
        {
            return RedirectToAction("Search", "Project", new { searchString });
        }
        else if (searchType == " Tasks")
        {
            return RedirectToAction("Search", "Task", new { searchString });
        }

        return RedirectToAction("Index", "Home");
    }
    */


    //[HttpGet]
    //public IActionResult GeneralSearch(string searchType, string searchString)
    //{
    //    // Assuming "Task" controller is correctly named. Adjust if it has a different name.
    //    var controllerName = searchType == "Projects" ? "Project" : searchType == "Tasks" ? "Tasks" : "Home";
    //    var actionName = "Search";

    //    return RedirectToAction(actionName, controllerName, new { area = "ProjectManagement", searchString });
    //}
    //public IActionResult NotFound(int statusCode)
    //{
    //    if(statusCode == 404)
    //    {
    //        return View("NotFound");
    //    }
    //    return View("Error");
    //}

    [HttpGet]
    public IActionResult GeneralSearch(string searchType, string searchString)
    {
        // Assuming "Task" controller is correctly named. Adjust if it has a different name.
        var controllerName = searchType == "Projects" ? "Project" : searchType == "Task" ? "Task" : "Home";
        var actionName = "Search";

        return RedirectToAction(actionName, controllerName, new { area = "ProjectManagement", searchString });
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}

