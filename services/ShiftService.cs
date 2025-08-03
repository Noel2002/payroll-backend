using DotNetTest.Models;
using DotNetTest.Config;
using Microsoft.EntityFrameworkCore;

namespace DotNetTest.Services;

public class ShiftService : IShiftService
{
    private readonly DatabaseContext _context;
    public ShiftService(DatabaseContext context)
    {
        _context = context;
    }
    public Shift CreateShift(Guid jobId, Guid userId, DateTime startTime, DateTime endTime, string comment)
    {
        Job? job = _context.Jobs.Find(jobId) ?? throw new KeyNotFoundException($"Job with ID {jobId} not found.");

        User? user = _context.Users.Find(userId) ?? throw new KeyNotFoundException($"User with ID {userId} not found.");

        Shift shift = new Shift
        {
            Job = job,
            User = user,
            StartTime = startTime,
            EndTime = endTime,
            PaymentStatus = PaymentStatus.PENDING,
            Comment = comment
        };

        _context.Shifts.Add(shift);
        _context.SaveChanges();
        return shift;
    }

    public void DeleteShift(Guid shiftId)
    {
        Shift? shift = _context.Shifts.Find(shiftId) ?? throw new KeyNotFoundException("Shift not found.");
        _context.Shifts.Remove(shift);
        _context.SaveChanges();
    }

    public Shift GetShift(Guid shiftId)
    {
        Shift? shift = _context.Shifts.Find(shiftId) ?? throw new KeyNotFoundException($"Shift with ID {shiftId} not found.");

        return shift;
    }

    public IEnumerable<Shift> GetShifts()
    {
        return _context.Shifts.Include(shift => shift.Job).Include(shift => shift.User);
    }

    public IEnumerable<Shift> GetShiftsByJob(Guid jobId)
    {
        return _context.Shifts
            .Include(shift => shift.Job)
            .Where(shift => shift.Job.Id == jobId);
    }

    public IEnumerable<Shift> GetShiftsByUser(Guid userId)
    {
        return _context.Shifts
            .Include(shift => shift.User)
            .Where(shift => shift.User.Id == userId);
    }

    public Shift UpdateShift(Guid shiftId, DateTime startTime, DateTime endTime)
    {
        throw new NotImplementedException();
    }
}