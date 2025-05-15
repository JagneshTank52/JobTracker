using Microsoft.IdentityModel.Tokens;
using TestAssignment.Entity.Models;
using TestAssignment.Repository.Interface;
using TestAssignment.Repository.ViewModel;
using TestAssignment.Service.Interface;

namespace TestAssignment.Service.Implementaion;

public class AuthenticationService : IAuthenticationService
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly ITokenService _tokenService;

    public AuthenticationService(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<(bool success, string? token, string message, User? user)> LoginUser(LoginVm model)
    {
        try
        {
            User? user = await _unitOfWork.UserRepository.GetUserByEmailAsync(model.Email);

            string token = string.Empty;

            if (user == null || user.Password != model.Password)
            {
                return (false, token, "Invalid Email or Password", null);
            }

            var timeSpan = TimeSpan.FromHours(24);
            token = _tokenService.GenerateAuthToken(user, timeSpan);

            if (token.IsNullOrEmpty())
            {
                return (false, null, "Token not generated", null);
            }

            return (true, token, "Login Successfully", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return(false,null,"Exception Occur for more information check console.",null);
        }
    }
}
