using AutoMapper;
using MediatR;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Services;
using SophosSolutions.Overtimes.Application.Users.Commands;
using SophosSolutions.Overtimes.Models.Entities;
using SophosSolutions.Overtimes.Models.Enums;
using System.Net.Mail;

namespace SophosSolutions.Overtimes.Application.Users.EventHandlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserReponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateUserReponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        var isCreated = await _unitOfWork.UserRepository.CreateAsync(user, request.Password!);

        if (isCreated)
        {
            await _unitOfWork.UserRepository.AddRoleAsync(user, Roles.Collaborator);
        }

        return _mapper.Map<CreateUserReponse>(user);
    }
}
