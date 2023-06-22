using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Services;
using SophosSolutions.Overtimes.Models.Common;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Infrastructure.Persistence.Contexts;

public class OvertimesDbContext : IdentityDbContext<User, Role, Guid>
{
    public DbSet<Overtime> TblOvertimes { get; set; }
    public DbSet<Area> TblAreas { get; set; }

    private readonly ICurrentUserService _currentUserService;

    public OvertimesDbContext(DbContextOptions<OvertimesDbContext> options, ICurrentUserService currentUserService)
        : base(options)
    {
        TblOvertimes = Set<Overtime>();
        TblAreas = Set<Area>();

        _currentUserService = currentUserService;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = _currentUserService.UserId;
                    entry.Entity.ModifiedOn = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
