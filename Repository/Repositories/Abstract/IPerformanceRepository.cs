using Domain.Models;
using System.Threading.Tasks;

namespace Repository.Repositories.Abstract
{
    public interface IPerformanceRepository : IBaseRepository<Performance>
    {
        Task<Performance?> GetComposite(int movieId, int participantId, CancellationToken cancellationToken);
        IEnumerable<Performance?> GetPerformancesByMovie(int movieId, CancellationToken cancellationToken);
        IEnumerable<Performance?> GetPerformancesByParticipant(int participantId, CancellationToken cancellationToken);
        IEnumerable<Performance?> GetByCharacterName(string title, CancellationToken cancellationToken);
    }
}
