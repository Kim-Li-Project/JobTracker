using System.ComponentModel.DataAnnotations;

namespace JobTracker.Application.DTOs.Jobs;

public class CreateJobRequest
{
    [Required] [StringLength(100)] public string Company { get; set; } = string.Empty;

    [Required] [StringLength(100)] public string Position { get; set; } = string.Empty;
}