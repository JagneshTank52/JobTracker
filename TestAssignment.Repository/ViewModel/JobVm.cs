using System.ComponentModel.DataAnnotations;

namespace TestAssignment.Repository.ViewModel;

public class JobVm
{
    public int Id { get; set; }
    [Required]
    public string JobTitle { get; set; } = string.Empty;
    public string JobDescription { get; set; } = string.Empty;
    [Required]
    public string JobLocation { get; set; } = string.Empty;
    [Required]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    public int NoOfApplicant { get; set; }
}
