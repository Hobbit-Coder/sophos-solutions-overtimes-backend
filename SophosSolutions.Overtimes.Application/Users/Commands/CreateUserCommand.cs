using AutoMapper;
using MediatR;
using SophosSolutions.Overtimes.Application.Common.Exceptions;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Application.Users.Common;
using SophosSolutions.Overtimes.Models.Entities;
using SophosSolutions.Overtimes.Models.ValueObjects;

namespace SophosSolutions.Overtimes.Application.Users.Commands;

public class CreateUserCommand : IRequest<CreateUserReponse>
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

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
        // 1) Validate the user doesn't exit
        var existedUser = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email!);

        if (existedUser is not null)
        {
            throw new ForbiddenAccessException($"Email {existedUser.Email} is already taken!");
        }

        // 2) Create user
        var user = _mapper.Map<User>(request);
        var result = await _unitOfWork.UserRepository.CreateAsync(user, request.Password!);

        // 3) Add default role
        if (result.Succeeded)
        {
            await _unitOfWork.UserRepository.AddRoleAsync(user, RoleName.Collaborator);
        }
        else
        {
            throw new ForbiddenAccessException(result.Errors.First());
        }

        return _mapper.Map<CreateUserReponse>(user);
    }
}