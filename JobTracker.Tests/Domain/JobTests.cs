using JobTracker.Domain.Entities;
using JobTracker.Domain.Enums;

namespace JobTracker.Tests.Domain;

public class JobTests
{
    [Fact]
    public void Constructor_ShouldSetStatusToApplied()
    {
        // Arrange
        var job = new Job("OpenAI", "Backend Engineer");

        // Assert
        Assert.Equal(JobStatus.Applied, job.Status);
        
    }
    
    [Fact]
    public void ChangeStatus_FromAppliedToInterview_ShouldUpdateStatus()
    {
        // Arrange
        var job = new Job("OpenAI", "Backend Engineer");

        // Act
        job.ChangeStatus(JobStatus.Interview);

        // Assert
        Assert.Equal(JobStatus.Interview, job.Status);
    }
    
    [Fact]
    public void ChangeStatus_FromRejectedToOffer_ShouldThrowException()
    {
        // Arrange
        var job = new Job("OpenAI", "Backend Engineer");
        job.ChangeStatus(JobStatus.Rejected);

        // Act
        var exception = Assert.Throws<ArgumentException>(
            () => job.ChangeStatus(JobStatus.Offer));

        // Assert
        Assert.Contains(
            "Cannot change job status from Rejected to Offer.",
            exception.Message);
    }
    
    [Fact]
    public void ChangeStatus_FromInterviewToOffer_ShouldUpdateStatus()
    {
        // Arrange
        var job = new Job("OpenAI", "Backend Engineer");
        job.ChangeStatus(JobStatus.Interview);

        // Act
        job.ChangeStatus(JobStatus.Offer);

        // Assert
        Assert.Equal(JobStatus.Offer, job.Status);
    }
    
    [Fact]
    public void ChangeStatus_WithUndefinedStatus_ShouldThrowException()
    {
        // Arrange
        var job = new Job("OpenAI", "Backend Engineer");

        // Act
        var exception = Assert.Throws<ArgumentException>(
            () => job.ChangeStatus((JobStatus)999));

        // Assert
        Assert.Contains(
            "Invalid job status value: 999.",
            exception.Message);
    }
}