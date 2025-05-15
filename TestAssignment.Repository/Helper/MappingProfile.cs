using AutoMapper;
using TestAssignment.Entity.Models;
using TestAssignment.Repository.ViewModel;

namespace TestAssignment.Repository.Helper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Book mappings
        CreateMap<JobVm, Job>();
        CreateMap<Job, JobVm>();
        CreateMap<JobVm, ApplyJobVm>();
        CreateMap<User, UserVm>();
        CreateMap<RegisterVm, User>();
        CreateMap<JobUserMappingVm, JobUserMapping>();
        CreateMap<JobUserMapping, JobUserMappingVm>();

    }
}
