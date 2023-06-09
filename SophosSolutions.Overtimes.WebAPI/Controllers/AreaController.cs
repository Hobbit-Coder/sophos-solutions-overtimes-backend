using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SophosSolutions.Overtimes.Application.Areas.Queries;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AreaController : ControllerBase
{
    private readonly ISender _mediator;

    public AreaController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Area>>> GetAllAsync()
    {
        var areas = await _mediator.Send(new GetAreasQuery());
        return Ok(areas);
    }
}
