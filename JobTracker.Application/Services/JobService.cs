using JobTracker.Application.DTOs.Jobs;
using JobTracker.Application.Interfaces;
using JobTracker.Domain.Entities;

namespace JobTracker.Application.Services;

public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;
    
    public JobService(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }
    
    public async Task<PagedResult<JobResponse>> GetPagedAsync(
        GetJobsRequest request)
    {
        var result = await _jobRepository.GetPagedAsync(request);

        return new PagedResult<JobResponse>
        {
            Items = result.Items.Select(MapToResponse),
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }

    public async Task<JobResponse?> GetByIdAsync(Guid id)
    {
        var job =  await _jobRepository.GetByIdAsync(id);
        
        return job is null ? null : MapToResponse(job);
    }

    public async Task<JobResponse> CreateAsync(CreateJobRequest request)
    {
        var job = new Job(request.Company.Trim(), request.Position.Trim());
        
        await _jobRepository.AddAsync(job);
        
        return MapToResponse(job);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateJobRequest request)
    {
        var existingJob = await _jobRepository.GetByIdAsync(id);

        if (existingJob is null)
        {
            return false;
        }
        
        existingJob.UpdateDetails(request.Company.Trim(), request.Position.Trim());
        
        await _jobRepository.UpdateAsync(existingJob);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingJob = await _jobRepository.GetByIdAsync(id);

        if (existingJob is null)
        {
            return false;
        }
        
        await _jobRepository.DeleteAsync(existingJob);
        return true;
    }

    public async Task<bool> ChangeStatusAsync(Guid id, ChangeJobStatusRequest request)
    {
        var existingJob = await _jobRepository.GetByIdAsync(id);

        if (existingJob is null)
        {
            return false;
        }

        if (request.Status is null)
        {
            throw new ArgumentException("Invalid job status.", nameof(request.Status));
        }

        existingJob.ChangeStatus(request.Status.Value);
        await _jobRepository.UpdateAsync(existingJob);
        return true;
    }
    
    private static JobResponse MapToResponse(Job job)
    {
        return new JobResponse
        {
            Id = job.Id,
            Company = job.Company.Trim(),
            Position = job.Position.Trim(),
            Status = job.Status.ToString(),
            CreatedAt = job.CreatedAt,
        };
    }
}