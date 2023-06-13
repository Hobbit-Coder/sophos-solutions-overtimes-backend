using MediatR;

namespace SophosSolutions.Overtimes.Application.Users.Commands;

public class AddAreaToUserCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid AreaId { get; set; }
}
