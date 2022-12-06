using Repository.Data;
using Repository.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BaseCompositeRepository<T> : BaseRepository<T>, IBaseCompositeRepository<T> where T : class
    {
        public BaseCompositeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public virtual async Task<T?> GetComposite(int firstId, int secondId, CancellationToken cancellationToken)
        {
            return await _entities.FindAsync(new object?[] { firstId, secondId }, cancellationToken);
        }

    }
}
