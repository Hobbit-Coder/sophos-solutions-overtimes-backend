using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SophosSolutions.Overtimes.Models.Entities;
using SophosSolutions.Overtimes.Models.Enums;
using System.Linq;

namespace SophosSolutions.Overtimes.Infrastructure.Persistence.Contexts;

public class OvertimesDbContextSeed
{
    private readonly OvertimesDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ILogger<OvertimesDbContextSeed> _logger;

    public OvertimesDbContextSeed(OvertimesDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager, ILogger<OvertimesDbContextSeed> logger)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_dbContext.Database.IsSqlServer())
            {
                await _dbContext.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        // Default roles
        var GeneralManagerRole = new Role();
        GeneralManagerRole.Name = Roles.GeneralManager.ToString();

        var HumanTalentManagerRole = new Role();
        HumanTalentManagerRole.Name = Roles.HumanTalentManager.ToString();

        var AreaManagerRole = new Role();
        AreaManagerRole.Name = Roles.AreaManager.ToString();

        var CollaboratorRole = new Role();
        CollaboratorRole.Name = Roles.Collaborator.ToString();

        var DefaultRoles = new List<Role>
        {
            GeneralManagerRole,
            HumanTalentManagerRole,
            AreaManagerRole,
            CollaboratorRole
        };

        foreach (var role in DefaultRoles.Where(role => _roleManager.Roles.All(defaultRole => defaultRole.Name != role.Name)))
        {
            await _roleManager.CreateAsync(role);
        }

        // Default users
        var administrator = new User("Test", "User");
        administrator.UserName = "administrator@test.com";
        administrator.Email = "administrator@test.com";

        if (_userManager.Users.All(user => user.Email != administrator.Email))
        {
            await _userManager.CreateAsync(administrator, "Admin1234*");
            await _userManager.AddToRoleAsync(administrator, GeneralManagerRole.Name);
        }
    }
}
