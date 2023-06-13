using AutoMapper;
using MediatR;
using SophosSolutions.Overtimes.Application.Common.Exceptions;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Application.Roles.Common;

namespace SophosSolutions.Overtimes.Application.Roles.Queries;

public class GetRoleByIdQuery : IRequest<GetRoleResponse>
{
    public Guid Id { get; set; }

    public GetRoleByIdQuery(Guid id)
    {
        Id = id;
    }
}

internal class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, GetRoleResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetRoleResponse> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(request.Id);

        if (role is null)
        {
            throw new NotFoundException(nameof(role), request.Id);
        }

        return _mapper.Map<GetRoleResponse>(role);
    }
}
