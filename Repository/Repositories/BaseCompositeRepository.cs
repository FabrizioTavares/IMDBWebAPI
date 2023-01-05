using Repository.Data;
using Repository.Repositories.Abstract;

namespace Repository.Repositories;

public class BaseCompositeRepository<T> : BaseRepository<T>, IBaseCompositeRepository<T> where T : class
{
    public BaseCompositeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

    public virtual async Task<T?> GetComposite(int firstId, int secondId, CancellationToken cancellationToken)
    {
        return await _entities.FindAsync(new object?[] { firstId, secondId }, cancellationToken);
    }

}