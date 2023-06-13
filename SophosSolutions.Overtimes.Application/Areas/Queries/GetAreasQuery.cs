using MediatR;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Application.Areas.Queries;

public class GetAreasQuery : IRequest<IEnumerable<Area>>
{
}

internal class GetAreasQueryHandler : IRequestHandler<GetAreasQuery, IEnumerable<Area>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAreasQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<Area>> Handle(GetAreasQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.AreaRepository.GetAllAsync();
    }
}
