using Microsoft.EntityFrameworkCore;
using DotNetTest.Models;

namespace DotNetTest.Config;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<User> Users { set; get; }
    public DbSet<Job> Jobs { set; get; }
    public DbSet<Shift> Shifts { set; get; }
}

