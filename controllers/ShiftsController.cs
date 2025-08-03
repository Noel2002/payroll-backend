using DotNetTest.Models;
using DotNetTest.Services;
using Microsoft.AspNetCore.Mvc;
using DotNetTest.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DotNetTest.Models;

[ApiController]
[Route("shifts")]
public class ShiftsController : ControllerBase
{
    private IShiftService _shiftService;
    public ShiftsController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Shift>> GetShifts()
    {
        var shifts = _shiftService.GetShifts();
        return Ok(shifts);
    }

    [HttpGet("{id}")]
    public ActionResult<Shift> GetShift(Guid id)
    {
       
        var shift = _shiftService.GetShift(id);
        return Ok(shift);
        
    }

    [HttpPost]
    public ActionResult<Shift> CreateShift([FromBody] CreateShiftDto createShiftDto)
    {
        Shift newShift = _shiftService.CreateShift(
            jobId: createShiftDto.JobId,
            userId: createShiftDto.UserId,
            startTime: createShiftDto.StartTime,
            endTime: createShiftDto.EndTime,
            comment: createShiftDto.Comment
        );

        return CreatedAtAction(nameof(GetShift), new { id = newShift.Id }, newShift);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteShift(Guid id)
    {
        
        _shiftService.DeleteShift(id);
        return NoContent();
        
    }
}
