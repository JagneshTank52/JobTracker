using AutoMapper;
using TestAssignment.Entity.Models;
using TestAssignment.Repository.Interface;
using TestAssignment.Repository.ViewModel;
using TestAssignment.Service.Interface;

namespace TestAssignment.Service.Implementaion;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<(bool status, string message)> ApplyJobAsync(ApplyJobVm applyJobModel, int userId)
    {
        try
        {
            Job? appliedJob = await _unitOfWork.JobRepository.GetByIdAsync(applyJobModel.JobId);

            if (appliedJob == null)
            {
                return (false, "Job does not exist");
            }
            string resumePath = string.Empty;

            if (applyJobModel.ResumeFile != null && applyJobModel.ResumeFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "resume");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(applyJobModel.ResumeFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await applyJobModel.ResumeFile.CopyToAsync(stream);
                }

                resumePath = "/uploads/resume/" + uniqueFileName;
                applyJobModel.ResumePath = resumePath;
            }

            JobUserMappingVm userJob = new JobUserMappingVm
            {
                JobId = applyJobModel.JobId,
                UserId = userId,
                StatusId = 1,
                UserResume = applyJobModel.ResumePath,
                Comment = string.Empty
            };

            JobUserMapping userJobMapping = _mapper.Map<JobUserMapping>(userJob);

            await _unitOfWork.JobUserMappingRepository.AddAsync(userJobMapping);

            bool isAdded = await _unitOfWork.SaveAsync();

            if (!isAdded)
            {
                return (false, "Job not Appllied");
            }

            return (true, "Job applied succcessfully");
        }
        catch (Exception ex)
        {

            Console.Write(ex.Message);
            return (false, "Exception Occure");
        }
    }

}