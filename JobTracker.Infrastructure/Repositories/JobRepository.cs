using JobTracker.Application.Interfaces;
using JobTracker.Domain.Entities;
using JobTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using JobTracker.Application.DTOs.Jobs;

namespace JobTracker.Infrastructure.Repositories;

public class JobRepository : IJobRepository
{
    private readonly JobTrackerDbContext _dbContext;

    public JobRepository(JobTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<Job>> GetPagedAsync(GetJobsRequest request)
    {
        IQueryable<Job> query = _dbContext.Jobs.AsNoTracking();

        if (request.Status.HasValue)
        {
            var status = request.Status.Value;
            
            query = query.Where(job => job.Status == status);
        }
        

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var keyword = request.Keyword.Trim();

            query = query.Where(job => job.Company.Contains(keyword) || job.Position.Contains(keyword));
        }

        var totalCount = await query.CountAsync();

        var jobs = await query.OrderByDescending(job => job.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResult<Job>
        {
            Items = jobs,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<Job?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Jobs.AsNoTracking().FirstOrDefaultAsync(job => job.Id == id);
    }


    public async Task AddAsync(Job job)
    {
        await _dbContext.Jobs.AddAsync(job);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Job job)
    {
        _dbContext.Jobs.Update(job);
        await _dbContext.SaveChangesAsync();
    }

    public Task DeleteAsync(Job job)
    {
        _dbContext.Jobs.Remove(job);
        return _dbContext.SaveChangesAsync();
    }
}