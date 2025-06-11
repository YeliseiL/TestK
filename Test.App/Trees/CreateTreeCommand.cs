using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.App.Exceptions;
using Test.Domain;

namespace Test.App.Trees;

public class CreateTreeCommand : IRequest<int>
{
    public string Name { get; set; } = null!;

}
public class CreateTreeCommandValidator : AbstractValidator<CreateTreeCommand>
{
    public CreateTreeCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
public class CreateTreeCommandHandler(ITestDbContext ctx, IMapper mapper) : IRequestHandler<CreateTreeCommand, int>
{
    public async Task<int> Handle(CreateTreeCommand request, CancellationToken cancellationToken)
    {
        if (await ctx.Trees.AnyAsync(e => e.Name == request.Name))
            throw new SecureException("Tree with this name exists");

        var tree = mapper.Map<Tree>(request);

        ctx.Trees.Add(tree);

        await ctx.SaveChangesAsync(CancellationToken.None);

        return tree.Id;
    }
}