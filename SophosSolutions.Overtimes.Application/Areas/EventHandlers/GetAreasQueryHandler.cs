using AutoMapper;
using MediatR;
using SophosSolutions.Overtimes.Application.Areas.Queries;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Application.Areas.EventHandlers;

public class GetAreasQueryHandler : IRequestHandler<GetAreasQuery, IEnumerable<Area>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAreasQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public Task<IEnumerable<Area>> Handle(GetAreasQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.AreaRepository.GetAllAsync();
    }
}
