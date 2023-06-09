using System.Linq.Expressions;

namespace SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync<TId>(TId id);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    bool Add(TEntity entity);
    void Add(IEnumerable<TEntity> entities);
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync<TId>(TId id);
}
