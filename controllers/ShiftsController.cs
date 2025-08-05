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
    public ActionResult<IEnumerable<Shift>> GetShifts([FromQuery] ShiftFilter? filter)
    {
        var shifts = _shiftService.GetShifts(filter);
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

    [HttpPatch("{id}")]
    public ActionResult<Shift> UpdateShift(Guid id, UpdateShiftDto updateShiftDto)
    {
        Shift updatedShift = _shiftService.UpdateShift(
            id: id,
            comment: updateShiftDto.Comment,
            startTime: updateShiftDto.StartTime,
            endTime: updateShiftDto.EndTime,
            paymentStatus: updateShiftDto.PaymentStatus
        );

        return Ok(updatedShift);
    }
}
