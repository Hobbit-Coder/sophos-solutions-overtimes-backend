using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SophosSolutions.Overtimes.Models.Entities;
using SophosSolutions.Overtimes.Models.ValueObjects;

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
        GeneralManagerRole.Name = RoleName.GeneralManager;

        var HumanTalentManagerRole = new Role();
        HumanTalentManagerRole.Name = RoleName.HumanTalentManager;

        var AreaManagerRole = new Role();
        AreaManagerRole.Name = RoleName.AreaManager;

        var CollaboratorRole = new Role();
        CollaboratorRole.Name = RoleName.Collaborator;

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

        // Default areas
        var management = new Area("Management");
        if (_dbContext.TblAreas.All(area => area.Name != management.Name))
        {
            _dbContext.TblAreas.Add(management);
        }

        var it = new Area("Computing and technology");
        if (_dbContext.TblAreas.All(area => area.Name != management.Name))
        {
            _dbContext.TblAreas.Add(it);
        }

        await _dbContext.SaveChangesAsync();

        // Default users
        var administrator = new User("Admin", "User");
        administrator.UserName = "1234567890";
        administrator.Email = "admin@test.com";
        administrator.AreaId = management.Id;

        if (_userManager.Users.All(user => user.Email != administrator.Email))
        {
            await _userManager.CreateAsync(administrator, "Admin1234*");
            await _userManager.AddToRoleAsync(administrator, RoleName.GeneralManager);
        }

        var collaborator = new User("Collaborator", "User");
        collaborator.UserName = "0987654321";
        collaborator.Email = "collaborator@test.com";
        collaborator.AreaId = it.Id;

        if (_userManager.Users.All(user => user.Email != collaborator.Email))
        {
            await _userManager.CreateAsync(collaborator, "Admin1234*");
            await _userManager.AddToRoleAsync(collaborator, RoleName.Collaborator);
        }


    }
}
