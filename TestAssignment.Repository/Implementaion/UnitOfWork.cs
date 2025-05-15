using AutoMapper;
using TestAssignment.Entity.Data;
using TestAssignment.Entity.Models;
using TestAssignment.Repository.Interface;

namespace TestAssignment.Repository.Implementaion;

public class UnitOfWork : IUnitOfWork
{
    private readonly TestAssignmentContext _context;
    protected readonly IMapper _mapper;
    private IUserRepository? _userRepository;
    private IGenericRepository<Job>? _jobRepository;
    private IGenericRepository<JobUserMapping>? _jobUserMappingRepository;
    private IGenericRepository<JobStatus>? _jobStatusRepository;

    public UnitOfWork(TestAssignmentContext context,IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper;
    }

    public IUserRepository UserRepository =>
        _userRepository ??= new UserRepository(_context,_mapper);
    public IGenericRepository<Job> JobRepository =>
        _jobRepository ??= new GenericRepository<Job>(_context,_mapper);
    public IGenericRepository<JobUserMapping> JobUserMappingRepository =>
        _jobUserMappingRepository ??= new GenericRepository<JobUserMapping>(_context,_mapper);
    public IGenericRepository<JobStatus> JobStatusRepository =>
        _jobStatusRepository ??= new GenericRepository<JobStatus>(_context,_mapper);

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
