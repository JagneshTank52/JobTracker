using TestAssignment.Entity.Models;

namespace TestAssignment.Repository.Interface;

public interface IUserRepository
{
    public Task<User?> GetUserByEmailAsync(string email);
}
