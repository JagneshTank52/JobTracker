namespace TestAssignment.Entity.Models;

public class JobStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<JobUserMapping> JobUserMappings { get; } = new List<JobUserMapping>();
}
