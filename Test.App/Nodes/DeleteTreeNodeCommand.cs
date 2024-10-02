using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Test.App.TreeNodes;

public class DeleteTreeNodeCommand : IRequest
{
    public int TreeNodeId { get; set; }
}
public class DeleteTreeNodeCommandHandler(ITestDbContext ctx) : IRequestHandler<DeleteTreeNodeCommand>
{
    public async Task Handle(DeleteTreeNodeCommand r, CancellationToken ct)
    {
        var node = await ctx.Nodes.FirstOrDefaultAsync(e => e.Id == r.TreeNodeId)
            ?? throw new Exception("Not found");

        ctx.Nodes.Remove(node);

        await ctx.SaveChangesAsync(ct);
    }
}