using MediatR;
using SophosSolutions.Overtimes.Application.Common.Exceptions;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;

namespace SophosSolutions.Overtimes.Application.Areas.Commands;

public class UpdateAreaCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

internal class UpdateAreaCommandHandler : IRequestHandler<UpdateAreaCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAreaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateAreaCommand request, CancellationToken cancellationToken)
    {
        var area = await _unitOfWork.AreaRepository.GetByIdAsync(request.Id);

        if (area is null)
        {
            throw new NotFoundException(nameof(area), request.Id);
        }

        area.Description = request.Description;
        area.Name = request.Name!;

        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}
