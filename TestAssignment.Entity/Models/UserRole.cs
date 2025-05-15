using System.ComponentModel.DataAnnotations;

namespace TestAssignment.Entity.Models;

public class UserRole
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; } = null!;
    public virtual ICollection<User> Users { get; } = new List<User>();
}
