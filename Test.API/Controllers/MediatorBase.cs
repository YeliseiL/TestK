using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class MediatorBase : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}
