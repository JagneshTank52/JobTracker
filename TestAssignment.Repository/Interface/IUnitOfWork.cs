using TestAssignment.Entity.Models;

namespace TestAssignment.Repository.Interface;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IGenericRepository<Job> JobRepository { get; }
    IGenericRepository<JobUserMapping> JobUserMappingRepository { get; }
    IGenericRepository<JobStatus> JobStatusRepository { get; }

    public Task<bool> SaveAsync();
}

