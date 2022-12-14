using Domain.Models;

namespace Repository.Repositories.Abstract
{
    public interface IDirectionRepository : IBaseRepository<Direction>
    {
        Task<Direction?> GetComposite(int movieId, int participantId, CancellationToken cancellationToken);
        IEnumerable<Direction?> GetDirectionsByMovie(int movieId, CancellationToken cancellationToken);
        IEnumerable<Direction?> GetDirectionsByParticipant(int participantId, CancellationToken cancellationToken);
    }
}
