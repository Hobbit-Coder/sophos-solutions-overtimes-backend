using MediatR;
using SophosSolutions.Overtimes.Application.Common.Exceptions;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Application.Overtimes.Queries;

public class GetOvertimeByIdQuery : IRequest<Overtime>
{
    public Guid Id { get; set; }

    public GetOvertimeByIdQuery(Guid id)
    {
        Id = id;
    }
}

internal class GetOvertimeByIdQueryHandler : IRequestHandler<GetOvertimeByIdQuery, Overtime>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOvertimeByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Overtime> Handle(GetOvertimeByIdQuery request, CancellationToken cancellationToken)
    {
        var overtime = await _unitOfWork.OvertimeRepository.GetByIdAsync(request.Id);

        if (overtime is null)
        {
            throw new NotFoundException(nameof(overtime), request.Id);
        }

        return overtime;
    }
}
