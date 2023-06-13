using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Application.Common.Models;
using SophosSolutions.Overtimes.Infrastructure.Common;
using SophosSolutions.Overtimes.Infrastructure.Persistence.Contexts;
using SophosSolutions.Overtimes.Models.Entities;
using SophosSolutions.Overtimes.Models.ValueObjects;

namespace SophosSolutions.Overtimes.Infrastructure.Identity;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(OvertimesDbContext dbContext, ILoggerFactory loggerFactory, UserManager<User> userManager)
        : base(dbContext, loggerFactory)
    {
        _userManager = userManager;
    }

    public async Task<Result> UpdateUserAsync(User user)
    {
        var result = await _userManager.UpdateAsync(user);
        return result.ToApplicationResult();
    }

    public async Task<Result> CreateAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        return result.ToApplicationResult();
    }

    public Task<bool> ValidatePasswordAsync(User user, string password)
    {
        return _userManager.CheckPasswordAsync(user, password);
    }

    public Task<User> GetByEmailAsync(string email)
    {
        return _userManager.FindByEmailAsync(email);
    }

    public async Task<Result> AddRoleAsync(User user, RoleName role)
    {
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);
        var roleResult = await _userManager.AddToRoleAsync(user, role);
        return roleResult.ToApplicationResult();
    }

    public Task<IList<string>> GetRolesAsync(User user)
    {
        return _userManager.GetRolesAsync(user);
    }
}
