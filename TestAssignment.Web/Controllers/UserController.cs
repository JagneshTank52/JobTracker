using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAssignment.Repository.ViewModel;
using TestAssignment.Service.Interface;

namespace TestAssignment.Web.Controllers;

public class UserController : Controller
{
    private readonly IAdminService _adminService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IAdminService adminService, IMapper mapper, IUserService userService)
    {
        _adminService = adminService;
        _mapper = mapper;
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Roles = "User")]
    [HttpGet]
    public async Task<IActionResult> ApplyJobAsync(int jobId)
    {
        if (jobId <= 0)
        {
            return RedirectToAction("Index", "Home");
        }

        var result = await _adminService.GetJobAsync(jobId);

        ApplyJobVm applyJobModel = _mapper.Map<ApplyJobVm>(result.jobModel);
        applyJobModel.JobId = jobId;

        return View(applyJobModel);
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> ApplyJobAsync(int jobId, ApplyJobVm applyJobModel)
    {
        if (jobId <= 0)
        {
            return RedirectToAction("Index", "Home");
        }

        if (jobId != applyJobModel.JobId)
        {
            TempData["error"] = "You can not apply other job";
            return View(applyJobModel);
        }

        int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var (status, message) = await _userService.ApplyJobAsync(applyJobModel, userId);

        if (!status)
        {
            TempData["error"] = message;
            return View(applyJobModel);
        }

        TempData["success"] = message;
        return RedirectToAction("Index", "Home");
    }
}

