using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.App.Exceptions;
using Test.Domain;

namespace Test.App.Trees;

public class CreateTreeCommand : IRequest<int>
{
    public string? Name { get; set; } 
}
public class CreateTreeCommandHandler(ITestDbContext ctx) : IRequestHandler<CreateTreeCommand, int>
{
    public async Task<int> Handle(CreateTreeCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name))
            throw new SecureException("Name is empty");

        if (await ctx.Trees.AnyAsync(e => e.Name == request.Name))
            throw new SecureException("Tree with this name exists");

        var tree = new Tree { Name = request.Name };

        ctx.Trees.Add(tree);

        await ctx.SaveChangesAsync();

        return tree.Id;
    }
}