using Microsoft.AspNetCore.Mvc;

namespace TestAssignment.Web.Controllers;

public class ErrorController : Controller
{
    public IActionResult Index(int statusCode)
    {
        if (statusCode == 401)
        {
            ViewData["ErrorMessage"] = "Unauthorized access.";
        }
        else if (statusCode == 403)
        {
            ViewData["ErrorMessage"] = "Forbidden access.";
        }
        else if (statusCode == 500)
        {
            ViewData["ErrorMessage"] = "Internal server error.";
        }
        else if (statusCode == 404)
        {
            ViewData["ErrorMessage"] = "Page you are looking for not found.";
        }
        else
        {
            ViewData["ErrorMessage"] = "An unknown error occurred.";
        }

        ViewData["StatusCode"] = statusCode;
        return View();
    }
}
