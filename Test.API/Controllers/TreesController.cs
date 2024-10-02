using Microsoft.AspNetCore.Mvc;
using Test.App.Trees;

namespace Test.API.Controllers;

public class TreesController : MediatorBase
{
    /// <summary>
    /// Create tree
    /// </summary>
    /// <response code="200"></response>
    [HttpPost()]
    public async Task<ActionResult<int>> Create(CreateTreeCommand r, CancellationToken token)
    {
        return Ok(await Mediator.Send(r, token));
    }
}
