using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestAssignment.Web.Controllers;

public class HomeController : Controller
{
    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Roles = "User")]
    public IActionResult Privacy()
    {
        return View();
    }
}
