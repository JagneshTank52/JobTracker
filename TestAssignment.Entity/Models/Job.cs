namespace TestAssignment.Entity.Models;

public class Job
{
    public int Id { get; set; }

    public string JobTitle { get; set; } = string.Empty;

    public string JobDescription { get; set; } = string.Empty;

    public string JobLocation { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public int NoOfApplicant { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? ModifiedAt { get; set; }
    public virtual ICollection<JobUserMapping> JobUserMappings { get; } = new List<JobUserMapping>();
}
