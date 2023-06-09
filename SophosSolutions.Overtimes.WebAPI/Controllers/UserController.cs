using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SophosSolutions.Overtimes.Application.Common.Models;
using SophosSolutions.Overtimes.Application.Users.Commands;
using SophosSolutions.Overtimes.Application.Users.Queries;
using SophosSolutions.Overtimes.Models.Entities;
using SophosSolutions.Overtimes.Models.Enums;

namespace SophosSolutions.Overtimes.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ISender _mediator;
    public UserController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "GeneralManager")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    public async Task<ActionResult<CreateUserReponse>> RegisterAsync([FromBody] CreateUserCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<ActionResult<JwtDetails>> AuthenticateAsync([FromBody] AuthenticateUserCommand request)
    {
        var jwt = await _mediator.Send(request);
        return Ok(jwt);
    }

    [HttpGet("me")]
    public ActionResult Me()
    {
        var user = HttpContext.User;

        return Ok(new
        {
            Claims = user.Claims.Select(s => new
            {
                s.Type,
                s.Value
            }).ToList(),
            user.Identity?.Name,
            user.Identity?.IsAuthenticated,
            user.Identity?.AuthenticationType
        });
    }
}
