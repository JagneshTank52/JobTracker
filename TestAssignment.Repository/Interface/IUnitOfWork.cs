namespace TestAssignment.Repository.Interface;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }

    public Task<bool> SaveAsync();
}

