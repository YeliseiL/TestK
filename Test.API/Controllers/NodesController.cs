using Microsoft.AspNetCore.Mvc;
using Test.App.TreeNodes;

namespace Test.API.Controllers;

public class NodesController : MediatorBase
{
    /// <summary>
    /// Get node
    /// </summary>
    /// <response code="200"></response>
    [HttpGet("id")]
    public async Task<ActionResult<int>> Create(int id, CancellationToken token)
    {
        return Ok(await Mediator.Send(new GetTreeNodeQuery { TreeNodeId = id}, token));
    }
    /// <summary>
    /// Create node
    /// </summary>
    /// <response code="200"></response>
    [HttpPost()]
    public async Task<ActionResult<int>> Create (CreateNodeCommand r, CancellationToken token)
    {
        return Ok(await Mediator.Send(r, token));
    }
    /// <summary>
    /// Update node
    /// </summary>
    /// <response code="204"></response>
    [HttpPut("id")]
    public async Task<ActionResult> Update(int id, UpdateTreeNodeCommand r, CancellationToken token)
    {
        r.TreeNodeId = id;
        await Mediator.Send(r, token);
        return NoContent();
    }
    /// <summary>
    /// Delete node
    /// </summary>
    /// <response code="204"></response>
    [HttpDelete("id")]
    public async Task<ActionResult> Delete(int id, DeleteTreeNodeCommand r, CancellationToken token)
    {
        r.TreeNodeId = id;
        await Mediator.Send(r, token);
        return NoContent();
    }
}
