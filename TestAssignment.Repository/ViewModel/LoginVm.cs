using System.ComponentModel.DataAnnotations;

namespace TestAssignment.Repository.ViewModel;

public class LoginVm
{
    [Required(ErrorMessage = "Email is required.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(12, MinimumLength = 6, ErrorMessage = "Password shoud be in between 6 and 12 digit")]
    public required string Password { get; set; }
}
