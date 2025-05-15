using TestAssignment.Entity.Data;
using TestAssignment.Repository.Interface;

namespace TestAssignment.Repository.Implementaion;

public class UnitOfWork : IUnitOfWork
{
    private readonly TestAssignmentContext _context;
    private IUserRepository? _userRepository;

    public UnitOfWork(TestAssignmentContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUserRepository UserRepository =>
        _userRepository ??= new UserRepository(_context);

    public async Task<bool> SaveAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}
