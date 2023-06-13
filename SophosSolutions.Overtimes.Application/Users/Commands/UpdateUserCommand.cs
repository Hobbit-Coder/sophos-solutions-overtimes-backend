using MediatR;
using SophosSolutions.Overtimes.Application.Common.Exceptions;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Models.ValueObjects;

namespace SophosSolutions.Overtimes.Application.Users.Commands;

public class UpdateUserCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public Guid ManagerId { get; set; }
    public Guid AreaId { get; set; }
    public Guid RoleId { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
            throw new NotFoundException(nameof(user), request.Id);
        }

        if (!string.IsNullOrEmpty(request.Name))
            user.Name = request.Name;

        if (!string.IsNullOrEmpty(request.LastName))
            user.LastName = request.LastName;

        if (request.ManagerId != Guid.Empty)
            user.ManagerId = request.ManagerId;

        if (request.AreaId != Guid.Empty)
            user.AreaId = request.AreaId;

        if (!string.IsNullOrEmpty(request.Email))
            user.Email = request.Email;

        if (!string.IsNullOrEmpty(request.UserName))
            user.UserName = request.UserName;

        if (request.RoleId != Guid.Empty)
        {
            var role = await _unitOfWork.RoleRepository.FindAsync(r => r.Id == request.RoleId);
            var roleName = RoleName.From(role.First().Name);
            await _unitOfWork.UserRepository.AddRoleAsync(user, roleName);
        }

        await _unitOfWork.UserRepository.UpdateUserAsync(user);
    }
}
