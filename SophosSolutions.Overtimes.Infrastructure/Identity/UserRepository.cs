using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Infrastructure.Persistence.Contexts;
using SophosSolutions.Overtimes.Models.Entities;
using SophosSolutions.Overtimes.Models.Enums;

namespace SophosSolutions.Overtimes.Infrastructure.Identity;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(OvertimesDbContext dbContext, ILoggerFactory loggerFactory, UserManager<User> userManager)
        : base(dbContext, loggerFactory)
    {
        _userManager = userManager;
    }

    public override async Task<bool> UpdateAsync(User user)
    {
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> CreateAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        return result.Succeeded;
    }

    public Task<bool> ValidatePasswordAsync(User user, string password)
    {
        return _userManager.CheckPasswordAsync(user, password);
    }

    public Task<User> GetByEmailAsync(string email)
    {
        return _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> AddRoleAsync(User user, Roles role)
    {
        var result = await _userManager.AddToRoleAsync(user, role.ToString());
        return result.Succeeded;
    }

    public Task<IList<string>> GetRolesAsync(User user)
    {
        return _userManager.GetRolesAsync(user);
    }
}
