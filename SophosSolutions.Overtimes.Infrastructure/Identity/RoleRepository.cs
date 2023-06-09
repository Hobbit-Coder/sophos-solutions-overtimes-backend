using Microsoft.Extensions.Logging;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Infrastructure.Persistence.Contexts;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Infrastructure.Identity;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(OvertimesDbContext dbContext, ILoggerFactory loggerFactory)
        : base(dbContext, loggerFactory)
    {
    }
}
