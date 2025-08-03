using DotNetTest.Dtos.Jobs;
using DotNetTest.Models;
using DotNetTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTest.Controllers;

[ApiController]
[Route("jobs")]
public class JobsController : Controller
{
    private readonly IJobService _jobService;
    private readonly ILogger<JobsController> _logger;
    public JobsController(IJobService jobService, ILogger<JobsController> logger)
    {
        _jobService = jobService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Job>> GetJobs()
    {
        var jobs = _jobService.GetJobs();
        return Ok(jobs);
    }

    [HttpPost]
    public ActionResult<Job> CreateJob([FromBody] CreateJobDto createJobDto)
    {
        var createdJob = _jobService.CreateJob(
            createJobDto.Title,
            createJobDto.Description,
            createJobDto.Rate,
            createJobDto.StartDate,
            createJobDto.EndDate,
            createJobDto.UserId
        );
        return CreatedAtAction(nameof(GetJobs), new { id = createdJob.Id }, createdJob);

    }

    [HttpGet("{id}")]
    public ActionResult<Job> GetJob(Guid id)
    {
    
        Job job = _jobService.GetJob(id);
        return Ok(job);
       

    }

    [HttpGet("user/{userId}")]
    public ActionResult<IEnumerable<Job>> GetJobsByUser(Guid userId)
    {
        var jobs = _jobService.GetJobsByUser(userId);
        return Ok(jobs);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteJob(Guid id)
    {
        
        _jobService.DeleteJob(id);
        return Ok("Job deleted successfully");
        
    }
}