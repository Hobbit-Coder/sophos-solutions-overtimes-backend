using Microsoft.Extensions.Logging;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Infrastructure.Persistence.Contexts;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Infrastructure.Identity;

public class AreaRepository : Repository<Area>, IAreaRepository
{
    public AreaRepository(OvertimesDbContext dbContext, ILoggerFactory loggerFactory)
        : base(dbContext, loggerFactory)
    {
    }

    public override async Task<bool> UpdateAsync(Area area)
    {
        var entity = await GetByIdAsync(area.Id);

        entity.Description = area.Description;
        entity.Name = area.Name;

        return true;
    }
}
