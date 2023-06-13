using MediatR;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Application.Areas.Queries;

public class GetAreaByIdQuery : IRequest<Area>
{
    public Guid Id { get; set; }

    public GetAreaByIdQuery(Guid id)
    {
        Id = id;
    }
}

internal class GetAreaByIdQueryHandler : IRequestHandler<GetAreaByIdQuery, Area>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAreaByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Area> Handle(GetAreaByIdQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.AreaRepository.GetByIdAsync(request.Id);
    }
}
