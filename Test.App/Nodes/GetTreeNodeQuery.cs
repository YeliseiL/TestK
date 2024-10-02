using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Domain;

namespace Test.App.TreeNodes;

public class GetTreeNodeQuery : IRequest<Node>
{
    public int TreeNodeId { get; set; }
}
public class GetTreeNodeQueryHandler(ITestDbContext ctx) : IRequestHandler<GetTreeNodeQuery, Node>
{
    public async Task<Node> Handle(GetTreeNodeQuery r, CancellationToken ct)
    {
        return await ctx.Nodes.FirstOrDefaultAsync(e => e.Id == r.TreeNodeId, ct) ?? new Node();
    }
}