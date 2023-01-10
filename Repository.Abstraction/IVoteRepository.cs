using Domain.Models;

namespace Repository.Abstraction;

public interface IVoteRepository : IBaseCompositeRepository<Vote>
{
    Task<Vote?> GetVotesByRating(int rating, CancellationToken cancellationToken);
}