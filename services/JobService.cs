using DotNetTest.Models;
using DotNetTest.Config;
using Microsoft.EntityFrameworkCore;

namespace DotNetTest.Services;

public class JobService : IJobService
{
    private readonly DatabaseContext _context;
    public JobService(DatabaseContext context)
    {
        _context = context;
    }
    public Job CreateJob(string title, string description, decimal rate, DateTime startDate, DateTime endDate, Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new MissingFieldException("User ID cannot be empty.");
        }

        User? user = _context.Users.Find(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        Job job = new Job
        {
            Title = title,
            Description = description,
            Rate = rate,
            StartDate = startDate,
            EndDate = endDate,
            User = user
        };

        _context.Jobs.Add(job);
        _context.SaveChanges();

        return job;
    }

    public void DeleteJob(Guid jobId)
    {
        Job? job = _context.Jobs.Find(jobId);
        if (job == null)
        {
            throw new KeyNotFoundException($"Job with ID {jobId} not found.");
        }

        _context.Jobs.Remove(job);
        _context.SaveChanges();
    }

    public Job? GetJob(Guid jobId)
    {
        Job? job = _context.Jobs.Find(jobId);
        if (job == null)
        {
            throw new KeyNotFoundException($"Job with ID {jobId} not found.");
        }
        return job;
    }

    public IEnumerable<Job> GetJobs()
    {
        IEnumerable<Job> jobs = _context.Jobs;
        return jobs;
    }

    public IEnumerable<Job> GetJobsByUser(Guid userId)
    {
        IEnumerable<Job> jobs = _context.Jobs.Include("User").Where(job => job.User.Id == userId);
        return jobs;
    }

    public Job? UpdateJob(Guid jobId, string title, string description, decimal rate, DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }
}