using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.App.Exceptions;

namespace Test.App.Nodes;

public class UpdateTreeNodeCommand : IRequest
{
    public int TreeNodeId { get; set; }
    public string Name { get; set; } = null!;
}
public class UpdateTreeNodeCommandValidator : AbstractValidator<UpdateTreeNodeCommand>
{
    public UpdateTreeNodeCommandValidator()
    {
        RuleFor(x => x.TreeNodeId).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
    }
}
public class UpdateTreeNodeCommandHandler(ITestDbContext ctx) : IRequestHandler<UpdateTreeNodeCommand>
{
    public async Task Handle(UpdateTreeNodeCommand r, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(r.Name))
            throw new SecureException("Name is empty");

        var node = await ctx.Nodes.FirstOrDefaultAsync(e => e.Id == r.TreeNodeId, ct)
            ?? throw new SecureException("Treenode not found");

        node.Name = r.Name;

        await ctx.SaveChangesAsync(ct);
    }
}