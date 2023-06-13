using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SophosSolutions.Overtimes.Application.Common.Models;
using SophosSolutions.Overtimes.Application.Users.Commands;
using SophosSolutions.Overtimes.Application.Users.Common;

namespace SophosSolutions.Overtimes.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthController(ISender mediator)
    {
        _mediator = mediator;
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
