using SophosSolutions.Overtimes.Models.Entities;
using SophosSolutions.Overtimes.Models.Enums;

namespace SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> CreateAsync(User user, string password);
    Task<bool> ValidatePasswordAsync(User user, string password);
    Task<User> GetByEmailAsync(string email);
    Task<bool> AddRoleAsync(User user, Roles role);
    Task<IList<string>> GetRolesAsync(User user);
}
