using TestAssignment.Repository.ViewModel;

namespace TestAssignment.Service.Interface;

public interface IUserService
{
    public Task<(bool status, string message)> ApplyJobAsync(ApplyJobVm applyJobModel, int UserId);
}
