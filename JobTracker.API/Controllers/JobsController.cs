using JobTracker.Application.DTOs.Jobs;
using JobTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [ProducesResponseType(typeof(PagedResult<JobResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpGet]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetJobsRequest request)
    {
        var jobs = await _jobService.GetPagedAsync(request);
        return Ok(jobs);
    }

    [ProducesResponseType(typeof(JobResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var job = await _jobService.GetByIdAsync(id);

        if (job is null)
        {
            return NotFound();
        }

        return Ok(job);
    }

    [ProducesResponseType(typeof(JobResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateJobRequest request)
    {
        var job = await _jobService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateJobRequest request)
    {
        var updated = await _jobService.UpdateAsync(id,request);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, ChangeJobStatusRequest request)
    {
        var changed = await _jobService.ChangeStatusAsync(id,request);

        if (!changed)
        {
            return NotFound();
        }
        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var delete = await _jobService.DeleteAsync(id);

        if (!delete)
        {
            return NotFound();
        }
        return NoContent();
        
    }
}