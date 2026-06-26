using JobTracker.Application.DTOs.Jobs;

namespace JobTracker.Application.Interfaces;

public interface IJobService
{
    Task<PagedResult<JobResponse>> GetPagedAsync(GetJobsRequest request);

    Task<JobResponse?> GetByIdAsync(Guid id);

    Task<JobResponse> CreateAsync(CreateJobRequest request);

    Task<bool> UpdateAsync(Guid id, UpdateJobRequest request);

    Task<bool> ChangeStatusAsync(Guid id, ChangeJobStatusRequest request);
    Task<bool> DeleteAsync(Guid id);
}