using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using Repository.Data;

namespace Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{

    protected readonly ApplicationDbContext _applicationDbContext;
    protected readonly DbSet<T> _entities;

    public BaseRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _entities = _applicationDbContext.Set<T>();
    }

    public virtual async Task<T?> Get(int id, CancellationToken cancellationToken)
    {
        return await _entities.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public virtual IEnumerable<T> GetAll()
    {
        return _entities.AsEnumerable<T>();
    }

    public virtual async Task<T> Insert(T entity, CancellationToken cancellationToken)
    {
        await _entities.AddAsync(entity, cancellationToken);
        await SaveChanges(cancellationToken);
        return entity;
    }

    public virtual async Task Update(T entity, CancellationToken cancellationToken)
    {
        _entities.Update(entity);
        await SaveChanges(cancellationToken);
    }

    public virtual async Task Remove(T entity, CancellationToken cancellationToken)
    {
        _entities.Remove(entity);
        await SaveChanges(cancellationToken);
    }

    public virtual async Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        return await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}