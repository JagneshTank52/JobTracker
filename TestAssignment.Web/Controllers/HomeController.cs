using Microsoft.AspNetCore.Mvc;
using TestAssignment.Repository.Helper;
using TestAssignment.Service.Interface;

namespace TestAssignment.Web.Controllers;

public class HomeController : Controller
{
    private readonly IAdminService _adminService;
    public HomeController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    public IActionResult Index()
    {
        return View();
    }

    // Get - Job List
    [HttpGet]
    public async Task<IActionResult> GetJobListAsync(QueryParameters queryParameters)
    {
        var jobList = await _adminService.JobListAsync(queryParameters);
        return PartialView("_JobList", jobList);
    }

    


}
