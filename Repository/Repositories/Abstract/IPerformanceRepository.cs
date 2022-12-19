using Domain.Models;

namespace Repository.Repositories.Abstract
{
    public interface IPerformanceRepository : IBaseRepository<Performance>
    {
        Task<Performance?> GetComposite(int movieId, int participantId, CancellationToken cancellationToken);
    }
}
