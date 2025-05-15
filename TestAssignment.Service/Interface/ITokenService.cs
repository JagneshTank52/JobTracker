using TestAssignment.Entity.Models;

namespace TestAssignment.Service.Interface;

public interface ITokenService
{
    public string GenerateAuthToken(User user, TimeSpan expiration);
}
