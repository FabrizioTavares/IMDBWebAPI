using Domain.Models;
using System.Threading.Tasks;

namespace Repository.Repositories.Abstract
{
    public interface IPerformanceRepository : IBaseRepository<Performance>
    {
        Task<Performance?> GetComposite(int movieId, int participantId, CancellationToken cancellationToken);
    }
}
