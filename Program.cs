using DotNetTest.Services;
using DotNetTest.Config;
using Microsoft.EntityFrameworkCore;
using DotNetTest.Models;
using DotNetTest.Attributes;
using DotNetTest.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocalDatabaseURL")));

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionHandlerFilter>();
});
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IAuthTokenService<AuthorizedUser>, AuthTokenService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IShiftService, ShiftService>();
builder.Services.AddScoped<IReportsService, ReportsService>();

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();