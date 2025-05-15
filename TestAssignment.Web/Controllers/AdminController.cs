using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAssignment.Repository.Helper;
using TestAssignment.Repository.ViewModel;
using TestAssignment.Service.Interface;

namespace TestAssignment.Web.Controllers;

public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult CreateJob()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateJobAsync(JobVm jobModel)
    {
        if (!ModelState.IsValid)
        {
            return View(jobModel);
        }

        var (status, message) = await _adminService.AddJobAsync(jobModel);

        if (!status)
        {
            TempData["error"] = message;
            return View(jobModel);
        }

        TempData["success"] = message;
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EditJobAsync(int jobId)
    {
        if (jobId <= 0)
        {
            return RedirectToAction("Index", "Home");
        }

        var result = await _adminService.GetJobAsync(jobId);

        return View(result.jobModel);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EditJobAsync(int jobId, JobVm jobModel)
    {
        if (jobId != jobModel.Id)
        {
            TempData["error"] = "You can not edit other job";
            return View(jobModel);
        }

        if (!ModelState.IsValid)
        {
            return View(jobModel);
        }

        var (status, message) = await _adminService.EditJobAsync(jobModel);

        if (!status)
        {
            TempData["error"] = message;
            return View(jobModel);
        }

        TempData["success"] = message;
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteJobAsync(int jobId)
    {
        if (jobId <= 0)
        {
            TempData["error"] = "Job does not exist";
            return RedirectToAction("Index", "Home");
        }

        var (status, message) = await _adminService.DeleteJobAsync(jobId);

        if (!status)
        {
            TempData["error"] = message;
            return RedirectToAction("Index", "Home");
        }

        TempData["success"] = message;
        return RedirectToAction("Index", "Home");

    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApplicationList(int jobId)
    {
        if (jobId <= 0)
        {
            return RedirectToAction("Index", "Home");
        }

        var candidateList = await _adminService.GetJobCandidateList(jobId);

        return PartialView("_CandidateList", candidateList);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult JobCandidateList(int jobId)
    {
        if (jobId <= 0)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetReviewModal(int jobId, int userId)
    {
        if (jobId <= 0)
        {
            return RedirectToAction("Index", "Home");
        }

        ReviewModal reviewModal = await _adminService.GetReviewModalAsync(jobId, userId);

        return PartialView("_Review", reviewModal);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateReview(ReviewModal reviewModal)
    {
        var (status, message) = await _adminService.UpdateJobStatus(reviewModal);

        return Json(new { success = status, msg = message });
    }

}
