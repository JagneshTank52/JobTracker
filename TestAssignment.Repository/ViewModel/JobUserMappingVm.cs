using TestAssignment.Entity.Models;

namespace TestAssignment.Repository.ViewModel;

public class JobUserMappingVm
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public int UserId { get; set; }
    public int StatusId { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public string UserResume { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public UserVm User {get;set;} = null!;
    public JobVm Job {get;set;} = null!;
}
