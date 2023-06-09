using MediatR;

namespace SophosSolutions.Overtimes.Application.Users.Commands;

public class CreateUserCommand : IRequest<CreateUserReponse>
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
