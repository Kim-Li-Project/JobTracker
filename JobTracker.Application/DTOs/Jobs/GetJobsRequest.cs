using System.ComponentModel.DataAnnotations;
using JobTracker.Domain.Enums;

namespace JobTracker.Application.DTOs.Jobs;

public class GetJobsRequest
{
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;
    
    [Range(1, 100)]
    public int PageSize { get; set; } = 10;
    
    [EnumDataType(
        typeof(JobStatus),
        ErrorMessage = "Status must be one of: Applied, Interview, Offer, Rejected.")]
    public JobStatus? Status { get; set; }

    
    [StringLength(100)]
    public string? Keyword { get; set; }
}