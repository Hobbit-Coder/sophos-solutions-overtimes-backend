using MediatR;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Application.Overtimes.Queries;

public class GetOvertimesQuery : IRequest<IEnumerable<Overtime>>
{
    public Guid? AreaId { get; set; }
    public Guid? UserId { get; set; }
}

internal class GetOvertimesQueryHandler : IRequestHandler<GetOvertimesQuery, IEnumerable<Overtime>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOvertimesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<Overtime>> Handle(GetOvertimesQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.OvertimeRepository.GetAllAsync();
    }
}