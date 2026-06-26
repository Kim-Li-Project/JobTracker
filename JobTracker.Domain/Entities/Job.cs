using JobTracker.Domain.Enums;

namespace JobTracker.Domain.Entities;

public class Job
{
    public Guid Id { get; private set; }
    public string Company { get; private set; }
    public string Position { get; private set; }
    public JobStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }


    public Job(string company, string position)
    {
        Id = Guid.NewGuid();
        Company = company;
        Position = position;
        Status = JobStatus.Applied;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string company, string position)
    {
        Company = company;
        Position = position;
    }   
    
    public void ChangeStatus(JobStatus newStatus)
    {
        if (!Enum.IsDefined(typeof(JobStatus), newStatus))
        {
            throw new ArgumentException($"Invalid job status value: {(int)newStatus}.");
        }

        if (!IsValidStatusTransition(Status, newStatus))
        {
            throw new ArgumentException(
                $"Cannot change job status from {Status} to {newStatus}.",
                nameof(newStatus));
        }

        Status = newStatus;
    }
    
    private static bool IsValidStatusTransition(JobStatus currentStatus, JobStatus newStatus)
    {
        return (currentStatus, newStatus) switch
        {
            (JobStatus.Applied, JobStatus.Interview) => true,
            (JobStatus.Applied, JobStatus.Rejected) => true,
            (JobStatus.Interview, JobStatus.Offer) => true,
            (JobStatus.Interview, JobStatus.Rejected) => true,
            _ => false
        };
    }
}