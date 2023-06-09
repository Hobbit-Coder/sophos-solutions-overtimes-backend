using Microsoft.Extensions.Logging;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Infrastructure.Persistence.Contexts;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Infrastructure.Identity;

public class OvertimeRepository : Repository<Overtime>, IOvertimeRepository
{
    public OvertimeRepository(OvertimesDbContext dbContext, ILoggerFactory loggerFactory)
        : base(dbContext, loggerFactory)
    {
    }

    public override async Task<bool> UpdateAsync(Overtime overtime)
    {
        var entity = await GetByIdAsync(overtime.Id);

        entity.Date = overtime.Date;
        entity.Description = overtime.Description;
        entity.Hours = overtime.Hours;
        entity.Status = overtime.Status;

        return true;
    }
}
