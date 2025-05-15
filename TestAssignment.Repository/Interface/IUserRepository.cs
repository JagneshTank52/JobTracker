using TestAssignment.Entity.Models;

namespace TestAssignment.Repository.Interface;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<User?> GetUserByEmailAsync(string email);
}
