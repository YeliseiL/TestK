using Microsoft.EntityFrameworkCore;
using Test.Domain;

namespace Test.App;

public interface ITestDbContext
{
    DbSet<ExceptionLog> ExceptionLogs { get; set; }
    DbSet<Node> Nodes { get; set; }
    DbSet<Tree> Trees { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
