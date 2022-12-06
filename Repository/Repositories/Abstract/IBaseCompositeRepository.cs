using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Abstract
{
    public interface IBaseCompositeRepository<T> : IBaseRepository<T>
    {
        Task<T?> GetComposite(int firstId, int secondId, CancellationToken cancellationToken);
    }
}
