using Domain.Models;

namespace Repository.Repositories.Abstract;

public interface IVoteRepository : IBaseCompositeRepository<Vote>
{
    Task<Vote?> GetVotesByRating(int rating, CancellationToken cancellationToken);
}