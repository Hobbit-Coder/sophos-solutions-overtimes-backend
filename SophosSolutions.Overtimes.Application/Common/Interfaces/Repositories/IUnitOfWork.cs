namespace SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    // Repositories
    IAreaRepository AreaRepository { get; }
    IOvertimeRepository OvertimeRepository { get; }
    IRoleRepository RoleRepository { get; }
    IUserRepository UserRepository { get; }

    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}
