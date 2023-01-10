using Domain.Models;

namespace Repository.Abstraction;

public interface IPerformanceRepository : IBaseRepository<Performance>
{
    Task<Performance?> GetComposite(int movieId, int participantId, CancellationToken cancellationToken);
}