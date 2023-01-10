using Domain.Models;

namespace Repository.Abstraction;

public interface IDirectionRepository : IBaseRepository<Direction>
{
    Task<Direction?> GetComposite(int movieId, int participantId, CancellationToken cancellationToken);
}