using SophosSolutions.Overtimes.Application.Common.Models;
using SophosSolutions.Overtimes.Models.Entities;
using SophosSolutions.Overtimes.Models.ValueObjects;

namespace SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<Result> CreateAsync(User user, string password);
    Task<bool> ValidatePasswordAsync(User user, string password);
    Task<User> GetByEmailAsync(string email);
    Task<Result> AddRoleAsync(User user, RoleName role);
    Task<IList<string>> GetRolesAsync(User user);
    Task<Result> UpdateUserAsync(User user);
}
