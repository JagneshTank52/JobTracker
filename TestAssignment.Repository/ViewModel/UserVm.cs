using System.ComponentModel.DataAnnotations;

namespace TestAssignment.Repository.ViewModel;

public class UserVm
{
    public int Id { get; set; }
    [Required(ErrorMessage = "First Name Reqired")]
    [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only alphabetic characters are allowed.")]
    public string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Last Name Reqired")]
    [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only alphabetic characters are allowed.")]
    public string LastName { get; set; } = string.Empty;
    [Required(ErrorMessage = "User Name is required.")]
    [StringLength(50, ErrorMessage = "User Name cannot exceed 50 characters.")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "User Name can only contain letters, digits, and underscores.")]
    public string UserName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email is required.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;
}
