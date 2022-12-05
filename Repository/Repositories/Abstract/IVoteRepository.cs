using Domain.Models;

namespace Repository.Repositories.Abstract
{
    public interface IVoteRepository : IBaseRepository<Vote>
    {
        Task<Vote?> GetVotesByRating(int rating, CancellationToken cancellationToken);
    }
}
