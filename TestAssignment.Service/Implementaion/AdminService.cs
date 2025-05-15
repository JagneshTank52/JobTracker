using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestAssignment.Entity.Models;
using TestAssignment.Repository.Helper;
using TestAssignment.Repository.Interface;
using TestAssignment.Repository.ViewModel;
using TestAssignment.Service.Helper;
using TestAssignment.Service.Interface;

namespace TestAssignment.Service.Implementaion;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET JOB LIST 
    public async Task<PaginatedList<JobVm>> JobListAsync(QueryParameters queryParameters)
    {
        // 1 DEFAULT FLTER
        Expression<Func<Job, bool>> jobFilter = f => !f.IsDeleted;

        Func<IQueryable<Job>, IOrderedQueryable<Job>> jobOrderBy = queryParameters.SortColumn switch
        {
            _ => q => q.OrderBy(o => o.Id)
        };

        var queryResult = await _unitOfWork.JobRepository.GetPagedRecordsDtoAsync<JobVm>(queryParameters.PageSize, queryParameters.PageNumber, jobOrderBy, jobFilter);

        PaginatedList<JobVm> customerList = new PaginatedList<JobVm>(queryResult.records, queryResult.TotalRecords, queryParameters);

        return customerList;
    }

    // Get Candidate list for particular job
    public async Task<List<JobUserMappingVm>> GetJobCandidateList(int jobId)
    {
        // 1 DEFAULT FLTER
        Expression<Func<JobUserMapping, bool>> candidateListFilter = f => !f.IsDeleted;

        // 2 Get according to Job Id
        candidateListFilter = candidateListFilter.AndAlso(f => f.JobId == jobId);

        //4 order by
        Expression<Func<JobUserMapping, object>> candidateListOrderBy = o => o.Id;

        Func<IQueryable<JobUserMapping>, IQueryable<JobUserMapping>>? candidateListInclude = i => i.Include(o => o.User).Include(a => a.Job).Include(c => c.JobStatus);


        var queryResult = await _unitOfWork.JobUserMappingRepository.GetAllAsync(false, candidateListFilter, candidateListOrderBy, candidateListInclude);

        var result = queryResult.Select(s => new JobUserMappingVm
        {
            Id = s.Id,
            JobId = s.JobId,
            Job = _mapper.Map<JobVm>(s.Job),
            UserId = s.UserId,
            User = _mapper.Map<UserVm>(s.User),
            StatusId = s.StatusId,
            StatusName = s.JobStatus.Name,
            UserResume = s.UserResume,
            Comment = s.Comment
        }).ToList();

        return result;
    }

    // Get Job 
    public async Task<(bool status, string message, JobVm? jobModel)> GetJobAsync(int id)
    {
        try
        {
            JobVm? job = await _unitOfWork.JobRepository.GetByIdDtoAsync<JobVm>(id);

            if (job == null)
            {
                return (false, "Job Does not found", job);
            }

            return (true, "Job found", job);
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
            return (false, "Exception Occure", null);
        }
    }

    // ADD JOB 
    public async Task<(bool status, string message)> AddJobAsync(JobVm jobModel)
    {
        try
        {
            Job newJob = _mapper.Map<Job>(jobModel);

            await _unitOfWork.JobRepository.AddAsync(newJob);

            bool isAdded = await _unitOfWork.SaveAsync();

            if (!isAdded)
            {
                return (isAdded, "New job not created.");
            }

            return (isAdded, "New job created.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return (false, "Exception Occur");
        }
    }
    // Edit Job 
    public async Task<(bool status, string message)> EditJobAsync(JobVm jobModel)
    {
        try
        {
            Job editedJob = _mapper.Map<Job>(jobModel);
            editedJob.ModifiedAt = DateTime.Now;

            _unitOfWork.JobRepository.Update(editedJob);

            bool IsEdited = await _unitOfWork.SaveAsync();

            if (!IsEdited)
            {
                return (IsEdited, $"job {jobModel.Id} not edited.");
            }

            return (IsEdited, $"job {jobModel.Id} edited.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return (false, "Exception Occur");
        }
    }

    // Get Job 
    public async Task<(bool status, string message)> DeleteJobAsync(int id)
    {
        try
        {
            Job? job = await _unitOfWork.JobRepository.GetByIdAsync(id);

            if (job == null)
            {
                return (false, "Job Does not found");
            }

            job.IsDeleted = true;
            job.ModifiedAt = DateTime.Now;
            _unitOfWork.JobRepository.Update(job);

            bool isDeleted = await _unitOfWork.SaveAsync();

            if (!isDeleted)
            {
                return (false, "Job not Deleted");
            }

            return (true, "Job deleted successfully");
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
            return (false, "Exception Occure");
        }
    }

    // ADD User 


    // public async Task<ReviewModal> GetReviewModal(int jobId)
    // {
    //     JobUserMapping? jobUserMapping = await _unitOfWork.JobUserMappingRepository.GetAllAsync(false, f =>f.IsDeleted && f => f.userId==userId && f => f.jobId==jobId);
    //     ReviewModal reviewModal = new ReviewModal
    //     {
    //         StatusList = (await _unitOfWork.JobStatusRepository.GetAllAsync()).Select(s => new SelectListItem
    //         {
    //             Value = s.Id.ToString(),
    //             Text = s.Name
    //         }).ToList(),
    //         JobUserMappingId = 
    //     };


    // }

}
