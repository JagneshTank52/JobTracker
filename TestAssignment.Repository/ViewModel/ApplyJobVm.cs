using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TestAssignment.Repository.ViewModel;

public class ApplyJobVm
{
    public int UserId { get; set; }
    public int JobId { get; set; }

    [Required(ErrorMessage = "Job Title Reqired")]
    [StringLength(250, ErrorMessage = "JobTitle cannot exceed 250 characters.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Only alphanumeric characters are allowed.")]
    public string JobTitle { get; set; } = string.Empty;
    public string JobDescription { get; set; } = string.Empty;
    [Required(ErrorMessage = "Job Location Reqired")]
    [StringLength(50, ErrorMessage = "JobTitle cannot exceed 50 characters.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Only alphanumeric characters are allowed.")]
    public string JobLocation { get; set; } = string.Empty;
    [Required(ErrorMessage = "Company Name Reqired")]
    [StringLength(50, ErrorMessage = "Company Name cannot exceed 50 characters.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Only alphanumeric characters are allowed.")]
    public string CompanyName { get; set; } = string.Empty;
    [Required(ErrorMessage = "NoOfApplicant Reqired")]
    public int NoOfApplicant { get; set; }
    public string ResumePath { get; set; } = string.Empty;
    [Required(ErrorMessage = "Resume Reqired")]
    public IFormFile? ResumeFile { get; set; }
}
