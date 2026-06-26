using JobTracker.Application.DTOs.Jobs;
using JobTracker.Domain.Entities;

namespace JobTracker.Application.Interfaces;

public interface IJobRepository
{
    Task<PagedResult<Job>> GetPagedAsync(GetJobsRequest request);
    Task<Job?> GetByIdAsync(Guid id);
    Task AddAsync(Job job);
    Task UpdateAsync(Job job);
    Task DeleteAsync(Job job);  
    
}