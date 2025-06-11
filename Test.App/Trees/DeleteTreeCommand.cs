using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.App.Exceptions;

namespace Test.App.Trees;

public class DeleteTreeCommand : IRequest
{
    public int TreeId { get; set; }
}
public class DeleteTreeCommandHandler(ITestDbContext ctx) : IRequestHandler<DeleteTreeCommand>
{
    public async Task Handle(DeleteTreeCommand r, CancellationToken ct)
    {
        var tree = await ctx.Trees.FirstOrDefaultAsync(e => e.Id == r.TreeId)
            ?? throw new SecureException("Not found");

        ctx.Trees.Remove(tree);

        await ctx.SaveChangesAsync(ct);
    }
}