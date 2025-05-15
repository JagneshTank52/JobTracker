using System.ComponentModel.DataAnnotations;

namespace TestAssignment.Repository.ViewModel;

public class RegisterVm
{
    [Required(ErrorMessage = "First Name Reqired")]
    [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only alphabetic characters are allowed.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Last Name Reqired")]
    [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only alphabetic characters are allowed.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "User Name is required.")]
    [StringLength(50, ErrorMessage = "User Name cannot exceed 50 characters.")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "User Name can only contain letters, digits, and underscores.")]
    public string? UserName { get; set; }
    
    [Required(ErrorMessage = "Email is required.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(12, MinimumLength = 6, ErrorMessage = "Password shoud be in between 6 and 12 digit")]
    public required string Password { get; set; }
}
