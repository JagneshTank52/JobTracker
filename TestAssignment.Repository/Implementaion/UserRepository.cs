using Microsoft.EntityFrameworkCore;
using TestAssignment.Entity.Data;
using TestAssignment.Entity.Models;
using TestAssignment.Repository.Interface;

namespace TestAssignment.Repository.Implementaion;

public class UserRepository : IUserRepository
{
    public readonly TestAssignmentContext _context;

    public UserRepository(TestAssignmentContext context)
    {
        _context = context;
    }
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.Include
            ("UserRole").Where(w => !w.IsDeleted).FirstOrDefaultAsync(u => u.Email == email);
    }
}
