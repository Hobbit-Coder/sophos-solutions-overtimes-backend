using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Infrastructure.Persistence.Contexts;
using System.Linq.Expressions;

namespace SophosSolutions.Overtimes.Infrastructure.Identity;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected OvertimesDbContext _dbContext;
    internal DbSet<TEntity> dbSet;
    private readonly ILogger<Repository<TEntity>> _logger;

    protected Repository(OvertimesDbContext dbContext, ILoggerFactory loggerFactory)
    {
        _dbContext = dbContext;
        dbSet = _dbContext.Set<TEntity>();
        _logger = loggerFactory.CreateLogger<Repository<TEntity>>();
    }

    public virtual async Task<TEntity> GetByIdAsync<TId>(TId id)
    {
        try
        {
            _logger.LogInformation(@$"{typeof(Repository<TEntity>)} GetByIdAsync, consultando información.");
            var entity = await dbSet.FindAsync(id);
            return entity!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, @$"{typeof(Repository<TEntity>)} GetByAsync, funciona con errores.");
            throw;
        }

    }

    public virtual bool Add(TEntity entity)
    {
        var result = dbSet.Add(entity);
        return result is not null;
    }

    public virtual void Add(IEnumerable<TEntity> entities)
    {
        dbSet.AddRange(entities);
    }

    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await dbSet.Where(predicate).ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public virtual async Task<bool> DeleteAsync<TId>(TId id)
    {
        var entity = await dbSet.FindAsync(id);
        if (entity is null) return false;
        dbSet.Remove(entity);
        return true;
    }

    public virtual Task<bool> UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
