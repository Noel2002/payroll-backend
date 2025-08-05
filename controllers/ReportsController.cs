using DotNetTest.Dtos;
using DotNetTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTest.Controllers;

[ApiController]
[Route("reports")]
public class ReportsController : ControllerBase
{
    private readonly IReportsService _reportsService;
    public ReportsController(IReportsService reportsService)
    {
        _reportsService = reportsService;
    }

    [HttpGet("{userId}/monthly-worked-hours")]
    public async Task<ActionResult<IEnumerable<MonthlyWorkingHoursDetails>>> GetMonthlyWorkingHoursDetails([FromRoute] Guid userId)
    {
        var report = await _reportsService.GetMonthlyWorkingHoursDetails(userId);
        return Ok(report);
    }

    [HttpGet("{userId}/monthly-earnings")]
    public async Task<ActionResult<IEnumerable<MonthlyEarnings>>> GetMonthlyEarnings([FromRoute] Guid userId)
    {
        var report = await _reportsService.GetMonthlyEarnings(userId);
        return Ok(report);
    }
}