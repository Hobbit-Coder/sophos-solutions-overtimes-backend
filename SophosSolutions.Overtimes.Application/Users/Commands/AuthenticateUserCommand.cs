using MediatR;
using SophosSolutions.Overtimes.Application.Common.Exceptions;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Services;
using SophosSolutions.Overtimes.Application.Common.Models;

namespace SophosSolutions.Overtimes.Application.Users.Commands;

public class AuthenticateUserCommand : IRequest<JwtDetails>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public AuthenticateUserCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}

internal class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, JwtDetails>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGeneratorService _jwtService;

    public AuthenticateUserCommandHandler(IUnitOfWork unitOfWork, IJwtTokenGeneratorService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<JwtDetails> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email!);

        if (user is null)
        {
            throw new NotFoundException($"User {request.Email} not found!");
        }

        var isValid = await _unitOfWork.UserRepository.ValidatePasswordAsync(user, request.Password!);

        if (!isValid)
        {
            throw new ForbiddenAccessException("Invalid password!");
        }

        var jwtDetails = await _jwtService.Generate(user);

        return jwtDetails;
    }
}

