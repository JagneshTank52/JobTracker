using Microsoft.AspNetCore.Http;

namespace TestAssignment.Repository.ViewModel;

public class ApplyJobVm
{
    public int UserId { get; set; }
    public int JobId { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string JobDescription { get; set; } = string.Empty;
    public string JobLocation { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public int NoOfApplicant { get; set; }
    public string ResumePath { get; set; } = string.Empty;
    public IFormFile? ResumeFile { get; set; }

}
