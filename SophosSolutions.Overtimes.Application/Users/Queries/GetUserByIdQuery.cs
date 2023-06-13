using AutoMapper;
using MediatR;
using SophosSolutions.Overtimes.Application.Common.Exceptions;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Application.Users.Common;

namespace SophosSolutions.Overtimes.Application.Users.Queries;

public class GetUserByIdQuery : IRequest<GetUserResponse>
{
    public Guid Id { get; set; }

    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }
}

internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetUserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
            throw new NotFoundException(nameof(user), request.Id);
        }

        var response = _mapper.Map<GetUserResponse>(user);
        response.Role = await _unitOfWork.UserRepository.GetRolesAsync(user);
        response.Area = await _unitOfWork.AreaRepository.GetByIdAsync(user.AreaId);

        return response;
    }
}
