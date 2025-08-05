using DotNetTest.Models;
namespace DotNetTest.Services;

public interface IShiftService
{
    Shift CreateShift(Guid jobId, Guid userId, DateTime startTime, DateTime endTime, string comment);
    Shift UpdateShift(Guid id, DateTime? startTime, DateTime? endTime, string? comment, PaymentStatus? paymentStatus);
    void DeleteShift(Guid shiftId);
    Shift GetShift(Guid shiftId);
    IEnumerable<Shift> GetShifts(ShiftFilter? filter = null);
    IEnumerable<Shift> GetShiftsByJob(Guid jobId);
    IEnumerable<Shift> GetShiftsByUser(Guid userId);
}