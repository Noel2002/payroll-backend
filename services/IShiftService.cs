using DotNetTest.Models;
namespace DotNetTest.Services;

public interface IShiftService
{
    Shift CreateShift(Guid jobId, Guid userId, DateTime startTime, DateTime endTime, string comment);
    Shift UpdateShift(Guid shiftId, DateTime startTime, DateTime endTime);
    void DeleteShift(Guid shiftId);
    Shift GetShift(Guid shiftId);
    IEnumerable<Shift> GetShifts();
    IEnumerable<Shift> GetShiftsByJob(Guid jobId);
    IEnumerable<Shift> GetShiftsByUser(Guid userId);
}