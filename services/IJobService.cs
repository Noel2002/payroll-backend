using DotNetTest.Models;
namespace DotNetTest.Services;

public interface IJobService
{
    Job CreateJob(string title, string description, decimal rate, DateTime startDate, DateTime endDate, Guid userId);
    Job? UpdateJob(Guid jobId, string title, string description, decimal rate, DateTime startDate, DateTime endDate);
    void DeleteJob(Guid jobId);
    Job? GetJob(Guid jobId);
    IEnumerable<Job> GetJobs();
    IEnumerable<Job> GetJobsByUser(Guid userId);
}