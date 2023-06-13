using MediatR;
using SophosSolutions.Overtimes.Application.Common.Exceptions;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Models.Enums;

namespace SophosSolutions.Overtimes.Application.Overtimes.Commands;

public class UpdateOvertimeCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public int Hours { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
}

internal class UpdateOvertimeCommandHandler : IRequestHandler<UpdateOvertimeCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOvertimeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateOvertimeCommand request, CancellationToken cancellationToken)
    {
        var overtime = await _unitOfWork.OvertimeRepository.GetByIdAsync(request.Id);

        if (overtime is null)
        {
            throw new NotFoundException(nameof(overtime), request.Id);
        }

        await _unitOfWork.CompleteAsync();
    }
}
