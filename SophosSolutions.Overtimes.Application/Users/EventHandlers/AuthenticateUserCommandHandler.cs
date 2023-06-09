using MediatR;
using SophosSolutions.Overtimes.Application.Common.Exceptions;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Services;
using SophosSolutions.Overtimes.Application.Common.Models;
using SophosSolutions.Overtimes.Application.Users.Commands;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Application.Users.EventHandlers;

public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, JwtDetails>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public AuthenticateUserCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<JwtDetails> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email!);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.Email!);
        }

        var isValid = await _unitOfWork.UserRepository.ValidatePasswordAsync(user, request.Password!);

        if (!isValid)
        {
            throw new ForbiddenAccessException();
        }

        var jwtDetails = await _jwtService.GenerateJWT(user);

        return jwtDetails;
    }
}
