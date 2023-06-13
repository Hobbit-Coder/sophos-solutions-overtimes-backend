using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SophosSolutions.Overtimes.Application.Overtimes.Commands;
using SophosSolutions.Overtimes.Application.Overtimes.Queries;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OvertimeController : ControllerBase
{
    private readonly ISender _mediator;

    public OvertimeController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Overtime>>> GetAsync()
    {
        var overtimes = await _mediator.Send(new GetOvertimesQuery());
        return Ok(overtimes);
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<Overtime>> GetByIdAsync([FromRoute] Guid id)
    {
        var overtime = await _mediator.Send(new GetOvertimeByIdQuery(id));
        return Ok(overtime);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> PostAsync([FromBody] CreateOvertimeCommand overtime)
    {
        var id = await _mediator.Send(overtime);
        return Ok(id);
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult> PutAsync([FromRoute] Guid id, [FromBody] UpdateOvertimeCommand overtime)
    {
        if (id != overtime.Id)
        {
            return BadRequest("Id's do not match");
        }
        await _mediator.Send(overtime);

        return NoContent();
    }
}
