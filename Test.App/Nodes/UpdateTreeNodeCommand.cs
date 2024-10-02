using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Test.App.TreeNodes;

public class UpdateTreeNodeCommand : IRequest
{
    public int TreeNodeId { get; set; }
    public string Name { get; set; } = null!;
}
public class UpdateTreeNodeCommandHandler(ITestDbContext ctx) : IRequestHandler<UpdateTreeNodeCommand>
{
    public async Task Handle(UpdateTreeNodeCommand r, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(r.Name))
            throw new Exception("Name is empty");

        var node = await ctx.Nodes.FirstOrDefaultAsync(e => e.Id == r.TreeNodeId, ct)
            ?? throw new Exception("Not found");

        node.Name = r.Name;

        await ctx.SaveChangesAsync(ct);   
    }
}