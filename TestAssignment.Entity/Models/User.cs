using System.ComponentModel.DataAnnotations;

namespace TestAssignment.Entity.Models;

public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string HasPassword { get; set; } = string.Empty;
    public bool IsDeleted {get; set;} = false;
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime? ModifiedAt {get; set;}
    public int UserRoleId { get; set; }

    public UserRole UserRole {get; set;} = null!;
}
