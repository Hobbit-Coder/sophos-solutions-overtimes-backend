using AutoMapper;
using MediatR;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Application.Users.Common;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Application.Users.Queries;

public class GetUsersQuery : IRequest<IEnumerable<GetUserResponse>>
{
    public Guid? AreaId { get; set; }
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<GetUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetUserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        List<User> users;

        if (request.AreaId is not null && request.AreaId != Guid.Empty)
        {
            users = (List<User>)await _unitOfWork.UserRepository.FindAsync(user => user.AreaId == request.AreaId);
        }
        else
        {
            users = (List<User>)await _unitOfWork.UserRepository.GetAllAsync();
        }

        List<GetUserResponse> usersResponse = new List<GetUserResponse>();

        foreach (var user in users)
        {
            var employee = _mapper.Map<GetUserResponse>(user);
            employee.Role = await _unitOfWork.UserRepository.GetRolesAsync(user);
            employee.Area = await _unitOfWork.AreaRepository.GetByIdAsync(user.AreaId);
            usersResponse.Add(employee);
        }

        return usersResponse;
    }
}
