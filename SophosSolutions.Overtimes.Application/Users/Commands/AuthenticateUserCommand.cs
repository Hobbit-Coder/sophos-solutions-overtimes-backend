using MediatR;
using SophosSolutions.Overtimes.Application.Common.Models;

namespace SophosSolutions.Overtimes.Application.Users.Commands;

public class AuthenticateUserCommand : IRequest<JwtDetails>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
