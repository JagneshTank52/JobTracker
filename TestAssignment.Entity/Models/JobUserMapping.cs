namespace TestAssignment.Entity.Models;

public class JobUserMapping
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public int UserId { get; set; }
    public int StatusId { get; set; }
    public string UserResume { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? ModifiedAt { get; set; }
    public virtual JobStatus JobStatus { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public virtual Job Job { get; set; } = null!;
}
