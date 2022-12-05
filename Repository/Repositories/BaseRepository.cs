using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        private readonly ApplicationDbContext _applicationDbContext;
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

        public virtual async Task<T?> GetComposite(int firstId, int secondId, CancellationToken cancellationToken)
        {
            return await _entities.FindAsync(new object?[] { firstId, secondId }, cancellationToken);
        }
        public virtual ICollection<T> GetAll()
        {
            return _entities.ToList();
        }

        public virtual async Task Insert(T entity, CancellationToken cancellationToken)
        {
            await _entities.AddAsync(entity, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
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
}
