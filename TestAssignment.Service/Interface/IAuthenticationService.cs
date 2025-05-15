using TestAssignment.Entity.Models;
using TestAssignment.Repository.ViewModel;

namespace TestAssignment.Service.Interface;

public interface IAuthenticationService
{
    public Task<(bool success, string? token, string message, User? user)> LoginUser(LoginVm model);
    public Task<(bool status, string message)> AddUserAsync(RegisterVm userModal);
}
