using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestAssignment.Entity.Data;
using TestAssignment.Entity.Models;
using TestAssignment.Repository.Interface;

namespace TestAssignment.Repository.Implementaion;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(TestAssignmentContext context,IMapper mapper) : base(context,mapper) { }
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.Include
            ("UserRole").Where(w => !w.IsDeleted).FirstOrDefaultAsync(u => u.Email == email);
    }
}
