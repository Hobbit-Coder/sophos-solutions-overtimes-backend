using AutoMapper;
using MediatR;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Application.Roles.Common;

namespace SophosSolutions.Overtimes.Application.Roles.Queries;

public class GetRolesQuery : IRequest<IEnumerable<GetRoleResponse>>
{
}

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<GetRoleResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRolesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<GetRoleResponse>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _unitOfWork.RoleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetRoleResponse>>(roles);
    }
}
