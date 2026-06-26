using System.ComponentModel.DataAnnotations;
using JobTracker.Domain.Enums;

namespace JobTracker.Application.DTOs.Jobs;

public class ChangeJobStatusRequest
{
    [Required]
    public JobStatus? Status { set; get; }
}