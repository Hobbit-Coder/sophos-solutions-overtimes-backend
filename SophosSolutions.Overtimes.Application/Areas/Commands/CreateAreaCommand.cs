using MediatR;

namespace SophosSolutions.Overtimes.Application.Areas.Commands;

public class CreateAreaCommand : IRequest<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
