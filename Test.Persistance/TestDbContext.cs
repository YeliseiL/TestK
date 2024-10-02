using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Test.App;
using Test.Domain;

namespace Test.Persistence;
//Add-Migration Init -StartupProject Test.API -Project Test.Persistence -Verbose
public class TestDbContext : DbContext, ITestDbContext
{
    public TestDbContext()
    {
    }
    public TestDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Node> Nodes { get; set; }
    public DbSet<Tree> Trees { get; set; }
    public DbSet<ExceptionLog> ExceptionLogs { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
