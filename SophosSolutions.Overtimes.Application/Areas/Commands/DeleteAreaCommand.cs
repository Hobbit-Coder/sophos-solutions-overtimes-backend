using MediatR;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;

namespace SophosSolutions.Overtimes.Application.Areas.Commands;

public class DeleteAreaCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteAreaCommand(Guid id)
    {
        Id = id;
    }
}

internal class DeleteAreaCommandHandler : IRequestHandler<DeleteAreaCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAreaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteAreaCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.AreaRepository.DeleteAsync(request.Id);

        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}
