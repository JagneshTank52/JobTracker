
using TestAssignment.Repository.Helper;
using TestAssignment.Repository.ViewModel;

namespace TestAssignment.Service.Interface;

public interface IAdminService
{
    public Task<PaginatedList<JobVm>> JobListAsync(QueryParameters queryParameters);
    public Task<List<JobUserMappingVm>> GetJobCandidateList(int jobId);
    public Task<(bool status, string message, JobVm? jobModel)> GetJobAsync(int id);
    public Task<(bool status, string message)> AddJobAsync(JobVm jobModel);
    public Task<(bool status, string message)> EditJobAsync(JobVm jobModel);
    public Task<(bool status, string message)> DeleteJobAsync(int id);

}
