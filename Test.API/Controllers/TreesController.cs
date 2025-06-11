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
    /// <summary>
    /// Delete tree
    /// </summary>
    /// <response code="200"></response>
    [HttpDelete("id")]
    public async Task<ActionResult<int>> Delete(int id, DeleteTreeCommand r, CancellationToken token)
    {
        r.TreeId = id;
        await Mediator.Send(r, token);
        return NoContent();
    }
}
