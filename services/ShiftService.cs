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

    public IEnumerable<Shift> GetShifts(ShiftFilter? filter)
    {
        IQueryable<Shift> query = _context.Shifts.Include(shift => shift.Job).Include(shift => shift.User);
        if (filter != null)
        {
            if (filter.From.HasValue)
            {
                query = query.Where(shift => shift.StartTime >= filter.From.Value);
            }
            if (filter.To.HasValue)
            {
                query = query.Where(shift => shift.EndTime <= filter.To.Value);
            }
            if (filter.PaymentStatus.HasValue)
            {
                query = query.Where(shift => shift.PaymentStatus == filter.PaymentStatus.Value);
            }
        }
        return query;
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

    public Shift UpdateShift(Guid id, DateTime? startTime, DateTime? endTime, string? comment, PaymentStatus? paymentStatus)
    {
        Shift? shift = _context.Shifts.Find(id) ?? throw new KeyNotFoundException($"Shift with ID {id} not found.");

        shift.StartTime = startTime ?? shift.StartTime;
        shift.EndTime = endTime ?? shift.EndTime;
        shift.Comment = comment ?? shift.Comment;
        shift.PaymentStatus = paymentStatus ?? shift.PaymentStatus;

        _context.Shifts.Update(shift);
        _context.SaveChanges();

        return shift;
    }
   
}