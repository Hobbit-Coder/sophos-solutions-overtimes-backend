using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SophosSolutions.Overtimes.Application.Common.Exceptions;
using SophosSolutions.Overtimes.Application.Users.Commands;
using SophosSolutions.Overtimes.Application.Users.Common;
using SophosSolutions.Overtimes.Application.Users.Queries;

namespace SophosSolutions.Overtimes.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly ISender _mediator;

    public EmployeeController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "GeneralManager,AreaManager")]
    public async Task<ActionResult<IEnumerable<GetUserResponse>>> GetAllAsync([FromQuery] Guid? areaId)
    {
        var request = new GetUsersQuery();

        if (HttpContext.User.IsInRole("AreaManager"))
        {
            if (areaId == null || areaId == Guid.Empty)
            {
                throw new ForbiddenAccessException("No tienes el rol necesario para obtener todos los usuarios!");
            }

            request.AreaId = areaId;
        }

        var users = await _mediator.Send(request);

        return Ok(users);
    }

    [HttpGet("{id:Guid}")]
    [Authorize]
    public async Task<ActionResult<GetUserResponse>> GetByAreaIdAsync([FromRoute] Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(user);
    }

    [HttpPut("{id:Guid}")]
    [Authorize]
    public async Task<ActionResult> PutAsync([FromRoute] Guid id, [FromBody] UpdateUserCommand request)
    {
        if (id != request.Id) return BadRequest("¡Los ids no coinciden!");
        await _mediator.Send(request);
        return NoContent();
    }

}
