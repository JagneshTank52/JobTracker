using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TestAssignment.Repository.ViewModel;
using TestAssignment.Service.Interface;

namespace TestAssignment.Web.Controllers;

public class AccountController : Controller
{
    protected readonly IAuthenticationService _authenticationService;

    public AccountController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    // GET - LOGIN
    [HttpGet]
    public IActionResult Login()
    {
        if (!HttpContext.Request.Cookies.ContainsKey("AuthToken"))
        {
            return View();
        }

        string userRole = User.FindFirst(ClaimTypes.Role)!.Value;

        return userRole switch
        {
            "Admin" => RedirectToAction("Index", "Home"),
            "User" => RedirectToAction("Index", "Home"),
            _ => View(),
        };
    }

    // POST - LOGIN
    [HttpPost]
    public async Task<IActionResult> Login(LoginVm loginModel)
    {
        if (!ModelState.IsValid)
        {
            return View(loginModel);
        }

        var (success, token, message, user) = await _authenticationService.LoginUser(loginModel);

        if (!success || user == null)
        {
            TempData["error"] = message;
            return View();
        }

        if (token != null)
        {
            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(24)
            }
            );
        }

        TempData["success"] = message;
        return user.UserRole.Name switch
        {
            "Admin" => RedirectToAction("Index", "Home"),
            "User" => RedirectToAction("Index", "Home"),
            _ => View(),
        };
    }

    // GET - LOGOUT
    [HttpGet]
    public IActionResult LogOut()
    {
        Response.Cookies.Delete("AuthToken");
        return RedirectToAction("Login", "Account");
    }

    // GET - Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // Post - Register
    [HttpPost]
    public async Task<IActionResult> RegisterAsync(RegisterVm registerModal)
    {
        if (!ModelState.IsValid)
        {
            return View(registerModal);
        }

        var (status, message) = await _authenticationService.AddUserAsync(registerModal);

        if (!status)
        {
            TempData["error"] = message;
            return View(registerModal);
        }

        TempData["success"] = message;
        return RedirectToAction("Login", "Account");
    }
}
