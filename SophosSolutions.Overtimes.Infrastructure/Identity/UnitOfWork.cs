using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Infrastructure.Persistence.Contexts;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Infrastructure.Identity;

public class UnitOfWork : IUnitOfWork
{
    private readonly OvertimesDbContext _dbContext;
    private readonly ILogger<UnitOfWork> _logger;

    public IAreaRepository AreaRepository { get; }
    public IOvertimeRepository OvertimeRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public IUserRepository UserRepository { get; }

    public UnitOfWork(OvertimesDbContext dbContext, ILoggerFactory loggerFactory, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _logger = loggerFactory.CreateLogger<UnitOfWork>();

        AreaRepository = new AreaRepository(_dbContext, loggerFactory);
        OvertimeRepository = new OvertimeRepository(_dbContext, loggerFactory);
        RoleRepository = new RoleRepository(_dbContext, loggerFactory);
        UserRepository = new UserRepository(_dbContext, loggerFactory, userManager);
    }

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Saving changes in db");
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error to save changes. Details: {0}", ex.Message);
            throw;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
