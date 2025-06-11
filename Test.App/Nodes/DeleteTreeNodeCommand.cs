using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.App.Exceptions;

namespace Test.App.Nodes;

public class DeleteTreeNodeCommand : IRequest
{
    public int TreeNodeId { get; set; }
}
public class DeleteTreeNodeCommandValidator : AbstractValidator<DeleteTreeNodeCommand>
{
    public DeleteTreeNodeCommandValidator()
    {
        RuleFor(e => e.TreeNodeId).GreaterThan(0);
    }
}

public class DeleteTreeNodeCommandHandler(ITestDbContext ctx) : IRequestHandler<DeleteTreeNodeCommand>
{
    public async Task Handle(DeleteTreeNodeCommand r, CancellationToken ct)
    {
        var node = await ctx.Nodes.FirstOrDefaultAsync(e => e.Id == r.TreeNodeId)
            ?? throw new SecureException("Treenode not found");

        ctx.Nodes.Remove(node);

        await ctx.SaveChangesAsync(ct);
    }
}