using DotNetTest.Config;
using DotNetTest.Dtos;
using DotNetTest.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Npgsql;

public class ReportsService : IReportsService
{
    private readonly DatabaseContext _context;
    public ReportsService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MonthlyEarnings>> GetMonthlyEarnings(Guid userId)
    {
        string query = @"SELECT EXTRACT(MONTH FROM ""EndTime"") as ""Month"", SUM((EXTRACT(EPOCH FROM(""EndTime"" - ""StartTime"")/3600))*j.""Rate"") as ""Earnings""
                        FROM ""Shifts"" s JOIN ""Jobs"" j on j.""Id""= s.""JobId""
                        WHERE s.""UserId""=@userId
                        GROUP BY EXTRACT(MONTH FROM ""EndTime"")";
        IEnumerable<MonthlyEarnings> monthlyEarnings = await _context.Database.SqlQueryRaw<MonthlyEarnings>(query, new NpgsqlParameter("userId", userId)).ToListAsync();
        return monthlyEarnings;
    }

    public async Task<IEnumerable<MonthlyWorkingHoursDetails>> GetMonthlyWorkingHoursDetails(Guid userId)
    {
        List<MonthlyWorkingHoursDetails> monthlyDetails = await _context.Database.SqlQueryRaw<MonthlyWorkingHoursDetails>(
            @$"select EXTRACT(MONTH FROM ""EndTime"") as ""Month"", SUM(EXTRACT(EPOCH FROM(""EndTime"" - ""StartTime"")/3600)) as ""TotalWorkingHours"" 
            FROM ""Shifts"" 
            WHERE ""UserId""=@userId
            GROUP BY EXTRACT(MONTH FROM ""EndTime"")", new NpgsqlParameter("userId", userId)).ToListAsync();

            return monthlyDetails;
    }
}