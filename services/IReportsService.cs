using DotNetTest.Dtos;

namespace DotNetTest.Services;

public interface IReportsService
{
    public Task<IEnumerable<MonthlyWorkingHoursDetails>> GetMonthlyWorkingHoursDetails(Guid userId);
    public Task<IEnumerable<MonthlyEarnings>> GetMonthlyEarnings(Guid userId);
}