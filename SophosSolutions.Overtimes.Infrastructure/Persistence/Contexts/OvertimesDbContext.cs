using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SophosSolutions.Overtimes.Models.Common;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Infrastructure.Persistence.Contexts;

public class OvertimesDbContext : IdentityDbContext<User, Role, Guid>
{
    public DbSet<Overtime> TblOvertimes { get; set; }
    public DbSet<Area> TblAreas { get; set; }

    public OvertimesDbContext(DbContextOptions<OvertimesDbContext> options)
        : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = Guid.Empty;
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = Guid.Empty;
                    entry.Entity.ModifiedOn = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
