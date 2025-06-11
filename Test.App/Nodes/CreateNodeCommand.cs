using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.App.Exceptions;
using Test.Domain;

namespace Test.App.Nodes;

public class CreateNodeCommand : IRequest<int>
{
    public int TreeId { get; set; }
    public int? ParentId { get; set; }
    public string Name { get; set; } = null!;
}
public class CreateNodeCommandValidator : AbstractValidator<CreateNodeCommand>
{
    public CreateNodeCommandValidator()
    {
        RuleFor(x => x.TreeId).GreaterThan(0);
        RuleFor(x => x.ParentId).GreaterThan(0).When(e => e.ParentId.HasValue);
        RuleFor(x => x.Name).NotEmpty();
    }
}
public class CreateTreeNodeCommandHandler(ITestDbContext ctx) : IRequestHandler<CreateNodeCommand, int>
{
    public async Task<int> Handle(CreateNodeCommand r, CancellationToken ct)
    {
        if (!await ctx.Trees.AnyAsync(e => e.Id == r.TreeId, ct))
            throw new SecureException("Tree not found");

        if (string.IsNullOrEmpty(r.Name))
            throw new SecureException("Name is empty");

        var node = new Node(r.TreeId, r.Name, r.ParentId);

        if (r.ParentId.HasValue && !await ctx.Nodes.AnyAsync(e => e.Id == r.ParentId))
            throw new SecureException("Parent node not found");

        await ctx.Nodes.AddAsync(node);

        await ctx.SaveChangesAsync(ct);

        return node.Id;
    }
}
