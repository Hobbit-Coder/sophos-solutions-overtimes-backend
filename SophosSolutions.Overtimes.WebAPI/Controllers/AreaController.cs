using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SophosSolutions.Overtimes.Application.Areas.Commands;
using SophosSolutions.Overtimes.Application.Areas.Queries;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "GeneralManager,AreaManager")]
public class AreaController : ControllerBase
{
    private readonly ISender _mediator;

    public AreaController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "GeneralManager")]
    public async Task<ActionResult<IEnumerable<Area>>> GetAsync()
    {
        var areas = await _mediator.Send(new GetAreasQuery());
        return Ok(areas);
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<Area>> GetByIdAsync([FromRoute] Guid id)
    {
        var area = await _mediator.Send(new GetAreaByIdQuery(id));
        return Ok(area);
    }

    [HttpPost]
    [Authorize(Roles = "GeneralManager")]
    public async Task<ActionResult<Guid>> PostAsync([FromBody] CreateAreaCommand area)
    {
        var id = await _mediator.Send(area);
        return Ok(id);
    }

    [HttpPut("{id:Guid}")]
    [Authorize(Roles = "GeneralManager")]
    public async Task<ActionResult> PutAsync([FromRoute] Guid id, [FromBody] UpdateAreaCommand area)
    {
        await _mediator.Send(area);
        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteAreaCommand(id));
        return NoContent();
    }
}
